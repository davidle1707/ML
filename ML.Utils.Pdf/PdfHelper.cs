using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using ML.Common;

namespace ML.Utils.Pdf
{
    public class PdfHelper
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(PdfHelper));

        private GenerateResponse _response;
        
        public GenerateResponse Merge(List<byte[]> pdfs, GenerateRequest request)
        {
            _response = new GenerateResponse();

            try
            {
                var readers = new List<PdfReader>();
                pdfs.ForEach(pdf => readers.Add(new PdfReader(pdf)));

                var mergeBuffer = MergeByPdfReaders(readers, request);

                _response.Contents = mergeBuffer.ToByteArray();
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _response.Message = ex.Message;
            }

            return _response;
        }

        public GenerateResponse Generate(byte[] pdf, GenerateRequest request)
        {
            _response = new GenerateResponse();

            try
            {
                var buffer = GenerateByPdfReader(new PdfReader(pdf), request);

                _response.Contents = buffer.ToByteArray();
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _response.Message = ex.Message;
            }

            return _response;
        }
        
        private ByteBuffer MergeByPdfReaders(List<PdfReader> readers, GenerateRequest request)
        {
            var mergeBuffer = new ByteBuffer();

            try
            {
                if (readers.Count == 1)
                {
                    mergeBuffer = GenerateByPdfReader(readers[0], request);
                }
                else if (readers.Count > 1)
                {
                    var mergeSetting = GetMergeRequest(request);

                    var pdfCopy = new PdfCopyFields(mergeBuffer);

                    foreach (var reader in readers)
                    {
                        var pdfBuffer = GenerateByPdfReader(reader, mergeSetting);
                        pdfCopy.AddDocument(new PdfReader(pdfBuffer.ToByteArray()));
                    }

                    if (request.ShowPrintDialog)
                    {
                        AddPrintAction(pdfCopy.Writer);
                    }

                    pdfCopy.Close();

                    AfterMerge(mergeBuffer, request);
                }
            }
            catch (Exception ex)
            {
                _log.Error("DocGenerator - MergePdfs", ex);
            }

            return mergeBuffer;
        }

        private ByteBuffer GenerateByPdfReader(PdfReader reader, GenerateRequest request)
        {
            var pdfBuffer = new ByteBuffer();
            var stamper = new PdfStamper(reader, pdfBuffer);

            if (request.ShowPrintDialog)
            {
                AddPrintAction(ref stamper);
            }

            if (request.Mapper != null)
            {
                MapFields(stamper, request);
            }

            if (request.ShowPageNumber)
            {
                AddPageNumbers(ref stamper, reader);
            }

            if (request.RetrieveSignaturePositions)
            {
                RetreivePositionSignatures(reader, request);
            }

            if (request.RetrieveTotalPages)
            {
                RetreiveTotalPages(reader);
            }

            stamper.Close();
            reader.Close();

            return pdfBuffer;
        }

        private void AfterMerge(ByteBuffer mergedBuffer, GenerateRequest request)
        {
            if (request.RetrieveSignaturePositions || request.RetrieveTotalPages || request.ShowPageNumber)
            {
                var tempPdfReader = new PdfReader(mergedBuffer.ToByteArray());

                if (request.RetrieveSignaturePositions)
                {
                    RetreivePositionSignatures(tempPdfReader, request);
                }

                if (request.RetrieveTotalPages)
                {
                    RetreiveTotalPages(tempPdfReader);
                }

                if (request.ShowPageNumber)
                {
                    var tmpPdfStamper = new PdfStamper(tempPdfReader, mergedBuffer);

                    AddPageNumbers(ref tmpPdfStamper, tempPdfReader);

                    tmpPdfStamper.Close();
                }

                tempPdfReader.Close();
            }
        }

        private void AddPageNumbers(ref PdfStamper stamper, PdfReader reader, float subtractRight = 30, float additionalBottom = 10)
        {
            var totalPages = reader.NumberOfPages;

            var font = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            for (var page = 1; page <= totalPages; page++)
            {
                var chunk = new Chunk($"Page {page} of {_response.TotalPages}");
                var pageSize = reader.GetPageSize(page);

                var pageContent = stamper.GetOverContent(page);
                pageContent.BeginText();
                pageContent.SetFontAndSize(font, 8);
                pageContent.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, chunk.Content, pageSize.Right - subtractRight, pageSize.Bottom + additionalBottom, 0);
                pageContent.EndText();
            }
        }
        
        private void MapFields(PdfStamper stamper, GenerateRequest request)
        {
            if (stamper.AcroFields?.Fields == null || stamper.AcroFields.Fields.Count == 0)
            {
                return;
            }
            
            foreach (string fieldKey in stamper.AcroFields.Fields.Keys)
            {
                var map = request.Mapper(new MapRequest { FieldKey = fieldKey });

                if (map.Success)
                {
                    if (map.Value is bool && stamper.AcroFields.GetFieldType(fieldKey) == AcroFields.FIELD_TYPE_CHECKBOX)
                    {
                        map.Value = (bool)map.Value ? "Yes" : "No";
                    }

                    stamper.AcroFields.SetField(fieldKey, map.Value.ToStr());
                }

                if (request.LockMappingField)
                {
                    stamper.AcroFields.SetFieldProperty(fieldKey, "setfflags", PdfFormField.FF_READ_ONLY, null);
                }
            }
        }

        private void RetreivePositionSignatures(PdfReader reader, GenerateRequest request)
        {
            if (request.SignatureFieldKeys.Count == 0 || reader.AcroFields?.Fields == null || reader.AcroFields.Fields.Count == 0)
            {
                return;
            }

            _response.SignaturePositions.Clear();

            foreach (string fieldKey in reader.AcroFields.Fields.Keys)
            {
                if (!request.SignatureFieldKeys.Contains(fieldKey))
                {
                    continue;
                }

                var fieldPositions = reader.AcroFields.GetFieldPositions(fieldKey);
                var pageNumber = (int)fieldPositions[0];

                _response.SignaturePositions.Add(new SignaturePosition
                {
                    FieldKey = fieldKey,
                    LowerLeftX = fieldPositions[1],
                    LowerLeftY = fieldPositions[2],
                    UpperRightX = fieldPositions[3],
                    UpperRightY = fieldPositions[4],
                    PageNumber = pageNumber,
                    PageSize = reader.GetPageSize(pageNumber),
                });
            }
        }

        private void RetreiveTotalPages(PdfReader reader)
        {
            _response.TotalPages = reader.NumberOfPages;
        }

        private void AddPrintAction(ref PdfStamper stamper)
        {
            AddPrintAction(stamper.Writer);
        }

        private void AddPrintAction(PdfWriter writer)
        {
            var printAction = PdfAction.JavaScript("this.print(true);", writer);
            writer.AddJavaScript(printAction);
        }

        private GenerateRequest GetMergeRequest(GenerateRequest request)
        {
            var merge = request.Clone();
            merge.ShowPrintDialog = false;
            merge.ShowPageNumber = false;
            merge.RetrieveSignaturePositions = false;
            merge.RetrieveTotalPages = false;
            
            return merge;
        }
    }
}
