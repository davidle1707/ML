using ML.Utils.DocuSign.API;
using System.Collections.Generic;

namespace ML.Utils.DocuSign
{
	public enum DocuSignApplicantType : short
	{
		Applicant = 1,

		CoApplicant
	}

	public class DocuSignEnvelope
	{
		public DocuSignEnvelope()
		{
			Recipients = new List<DocuSignRecipient>();
			Tabs = new List<DocuSignTab>();
		}

		public string Subject { get; set; }

		public string EmailBlurb { get; set; }

		public List<DocuSignRecipient> Recipients { get; set; }

		public List<DocuSignTab> Tabs { get; set; }
	}

	public class DocuSignTab
	{
		public TabTypeCode TypeCode { get; set; }

		public int XPosition { get; set; }

		public int YPosition { get; set; }

		public int PageNumber { get; set; }

		public DocuSignApplicantType ApplicantType { get; set; }
	}

	public class DocuSignRecipient
	{
		public DocuSignRecipient()
		{
			TypeCode = RecipientTypeCode.Signer;
			ApplicantType = DocuSignApplicantType.CoApplicant;
		}

		public int Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		/// <summary>
		/// Default value: Signer
		/// </summary>
		public RecipientTypeCode TypeCode { get; set; }

		/// <summary>
		/// Default value: CoApplicant
		/// </summary>
		public DocuSignApplicantType ApplicantType { get; set; }
	}
}
