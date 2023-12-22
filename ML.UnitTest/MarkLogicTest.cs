using ML.Utils.MarkLogic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;
using ML.Common;

namespace ML.UnitTest
{
    [TestFixture]
    public class MarkLogicTest
    {
        [Test]
        public void Test()
        {
            var update = XBuilder<User>.UpdateField.Set(a => a.FirstName, "huy") & XBuilder<User>.UpdateField.Set(a => a.FirstName, "ngo");
            //var mng = new MarkLogicManager<User>("xcc://super:asd123@CMSMDB:3939", "CMSM");
            //var query = mng.UpdateField(Guid.Empty, update);

            //var query = XBuilder<Organization>.FromElem("CMSM", user => user.Id, new Guid(), user => user.LosIntegrationOrgs).ToList(b => b.Empty);

            //var db = new XDataContext("xcc://super:asd123@CMSMDB:3939", "CMSM");
            //var user = db.Entity<User, Guid>();

            //var entity = user.FindOne(new Guid("475c36ea-7c98-dbcf-589a-08d1e24440b5"));

        }

        private XJoin<T> From<T>()
        {
            return XBuilder<T>.From("CMSM");
        }

        #region Entity

        [Serializable]
        [XEntity("Role", "r", "http://www.cmsm.com")]
        public class Role : BaseEntity
        {
            public string RoleName { get; set; }

            public string Description { get; set; }

            public Guid OrganizationId { get; set; }

            public Guid? BranchId { get; set; }

            //public List<RoleStage> RoleStages { get; set; }

            //public List<RoleContent> RoleContents { get; set; }
        }

        [Serializable]
        [XEntity("Branch", "b", "http://www.cmsm.com")]
        public class Branch : BaseEntity
        {
            public Guid OrganizationId { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string Zip { get; set; }

            public string ContactName { get; set; }

            public string ContactPhone { get; set; }

            public string ContactMail { get; set; }

            public bool IsActive { get; set; }

            public DateTime? CreatedDate { get; set; }

            public Guid? CreatedBy { get; set; }

            public DateTime? ModifiedDate { get; set; }

            public Guid? ModifiedBy { get; set; }
        }

        [Serializable]
        [XEntity("BusinessFile", "bf", "http://www.cmsm.com")]
        public class BusinessFile : BaseEntity
        {
            public BusinessFile()
            {
                Generic = new BusinessFileGeneric();

                FileRelations = new List<BusinessFileRelation>();
            }

            public Guid SynchronizeId { get; set; }

            public Guid OrganizationId { get; set; }

            public short BusinessType { get; set; }

            public int FileNumber { get; set; }

            public DateTime CreatedDate { get; set; }

            public Guid? CreatedBy { get; set; }

            public BusinessFileGeneric Generic { get; set; }

            public Guid PrimaryBorrowerId { get; set; }

            public bool IsSubFile { get; set; }

            public int SubFileNumber { get; set; }

            public List<BusinessFileRelation> FileRelations { get; set; }

        }

        [Serializable]
        [XEntity("bf")]
        public class BusinessFileGeneric
        {
            public Guid? BranchId { get; set; }

            public Guid? CampaignId { get; set; }

            public Guid? DBAId { get; set; }

            public string ColorCode { get; set; }

            public string MarketingReferenceNumber { get; set; }

            public Guid? LastOpenedBy { get; set; }

            public DateTime? LastOpenedDate { get; set; }

            public DateTime? LastSavedDate { get; set; }

            public Guid? LastSavedBy { get; set; }

            #region Delete

            public Guid? DeletedOrganizationId { get; set; }

            public DateTime? DeletedDate { get; set; }

            public Guid? DeletedBy { get; set; }

            public DateTime? RestoredDate { get; set; }

            public Guid? RestoredBy { get; set; }

            #endregion

            #region Sync

            public Guid? SyncOrganizationId { get; set; }

            public Guid? SyncBusinessFileId { get; set; }

            public short? SyncStatus { get; set; }

            #endregion
        }

        [Serializable]
        [XEntity("bf")]
        public class BusinessFileRelation
        {
            public short Relation { get; set; }

            public Guid BusinessFileId { get; set; }

            public Guid OrganizationId { get; set; }

        }

        [XEntity("DynamicField", "df", "http://www.cmsm.com")]
        public class DynamicField : BaseEntity
        {
            public string Name { get; set; }

            public string DisplayName { get; set; }

            public short Type { get; set; }

            public short DataType { get; set; }

            public short ControlType { get; set; }

            public short BusinessType { get; set; }

            public Guid OrganizationId { get; set; }

            public bool IsRequired { get; set; }

            public string DocumentParamName { get; set; }

            public string QuestionaireParamName { get; set; }

            public bool DisplayInPipeline { get; set; }

            public bool DisplayInReport { get; set; }

            public bool UseInCgiParam { get; set; }

            public Guid? ParentId { get; set; }

            public bool? IsHighlightApp { get; set; }

            public bool IsConditionalQuestion { get; set; }

            public List<DynamicFieldReferenceDC> DynamicFieldReferences { get; set; }
        }

        [Serializable]
        [XEntity("Team", "t", "http://www.cmsm.com")]
        public class Team : BaseEntity
        {
            public string Name { get; set; }

            public Guid OrganizationId { get; set; }

            public Guid? BranchId { get; set; }

            public Guid? LeaderId { get; set; }

            public TeamUserDC TeamUsers { get; set; }
        }

        [XEntity("FaxLog", "fl", "http://www.cmsm.com")]
        public class FaxLog : BaseEntity
        {
            public Guid? BusinessFileId { get; set; }

            public Guid? SynchronizeFileId { get; set; }

            public Guid? LoggedBy { get; set; }

            public DateTime LoggedDate { get; set; }

            public string Description { get; set; }

            public int FaxTotalPages { get; set; }

            public string FaxStatus { get; set; }

            public string FaxExternalId { get; set; }

            public string ReceiveName { get; set; }

            public string Notes { get; set; }

            public string FaxSource { get; set; }

            public string FaxDestination { get; set; }

            public Guid OrganizationId { get; set; }

            public Guid? BranchId { get; set; }

            public string Status { get; set; }

            public bool IsSent { get; set; }

            public string FaxId { get; set; }

            public DateTime? FaxReceivedDate { get; set; }
        }

        [Serializable]
        public class BaseEntity : XEntity<Guid>
        {
            public override Guid Id { get; set; }
        }

        [XEntity("EmailServer", "es", "http://www.cmsm.com")]
        public class EmailServer : BaseEntity
        {
            public Guid OrganizationId { get; set; }

            public Guid? BranchId { get; set; }

            public string ServerName { get; set; }

            public string IncomingServer { get; set; }

            public string OutgoingServer { get; set; }

            public bool OutgoingServerEnableSsl { get; set; }

            public int OutgoingServerPort { get; set; }

            public bool OutgoingRequireAnthen { get; set; }
        }

        [Serializable]
        [XEntity("SupportRequest", "sr", "http://www.cmsm.com")]
        public class SupportRequest : BaseEntity
        {
            public Guid OrganizationId { get; set; }

            public short RequestType { get; set; }

            public int TicketNumber { get; set; }

            public string Email { get; set; }

            public Guid SubmittedBy { get; set; }

            public DateTime SubmittedDate { get; set; }

            public string Phone { get; set; }

            public string Message { get; set; }

            public string BusinessFileNumber { get; set; }

            public short RequestStatus { get; set; }

            public Guid? AssignedTo { get; set; }

            public DateTime? AssignedDate { get; set; }

            public Guid? ResolvedBy { get; set; }

            public DateTime? ResolvedDate { get; set; }

            public DateTime? LastResponseDate { get; set; }
        }

        [XEntity("Organization", "o", "http://www.cmsm.com")]
        public class Organization : BaseEntity
        {
            public Organization()
            {
                BusinessTypeOrgs = new List<BusinessTypeOrg>();
                Reports = new List<ReportOrg>();
                ProductAndPricingOrgs = new List<ProductAndPricingOrg>();
                LosIntegrationOrgs = new List<LOSIntegrationOrg>();
            }

            public Guid? ParentId { get; set; }

            public short? OrgType { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string Zip { get; set; }

            public string ContactName { get; set; }

            public string ContactPhone { get; set; }

            public string ContactEmail { get; set; }

            public bool IsActive { get; set; }

            public DateTime CreatedDate { get; set; }

            public Guid? CreatedBy { get; set; }

            public Guid? CreatedByOrgId { get; set; }

            public DateTime? ModifiedDate { get; set; }

            public Guid? ModifiedBy { get; set; }

            public Guid? EmailServerId { get; set; }

            public string EmailLogin { get; set; }

            public string EmailPassword { get; set; }

            public int CurrentAppNumber { get; set; }

            public string DocuSignEmail { get; set; }

            public string DocuSignPassword { get; set; }

            public string DocuSignAccountId { get; set; }

            public string DocuSignIntegratorKey { get; set; }

            public short DistributeAppType { get; set; }

            public List<BusinessTypeOrg> BusinessTypeOrgs { get; set; }

            public List<ReportOrg> Reports { get; set; }

            public bool IsBackEnd { get; set; }

            public bool HasCallCenterMode { get; set; }

            public bool EnableChat { get; set; }

            public bool EnableEmail { get; set; }

            public bool EnableSupport { get; set; }

            public bool EnablePhone { get; set; }

            public bool EnableTaskAlert { get; set; }

            public bool EnableSMS { get; set; }

            public bool EnableNotification { get; set; }

            public Guid? UseWhiteLabelId { get; set; }

            public bool DisableCustomStage { get; set; }

            public bool DisableCustomTrigger { get; set; }

            public bool HasFax { get; set; }

            public decimal FaxBalance { get; set; }

            public decimal FaxPriceOrderNumber { get; set; }

            public string VitelityFaxUserName { get; set; }

            public string VitelityFaxPassword { get; set; }

            public string PrefixAppNumber { get; set; }

            public int? MaxUsers { get; set; }

            public bool CanManageFrontEndUsers { get; set; }

            public bool EnableAuthenWhiteLabel { get; set; }

            public bool EnableAuthenIpRestriction { get; set; }

            public bool EnableCheckOpenedFile { get; set; }

            public string ContactFax { get; set; }

            public bool EnableAppDataAsCAPS { get; set; }

            public bool? EnableSetupProductAndPricing { get; set; }

            public List<ProductAndPricingOrg> ProductAndPricingOrgs { get; set; }

            public List<LOSIntegrationOrg> LosIntegrationOrgs { get; set; }
        }

        [Serializable]
        [XEntity("o")]
        public class BusinessTypeOrg
        {
            public short BusinessType { get; set; }

        }

        [Serializable]
        [XEntity("o")]
        public class ReportOrg
        {
            public Guid Id { get; set; }
        }

        [Serializable]
        [XEntity("o")]
        public class ProductAndPricingOrg
        {
            public Guid Id { get; set; }
        }

        [Serializable]
        [XEntity("o")]
        public class LOSIntegrationOrg
        {
            public short LOSCompany { get; set; }
        }

        [Serializable]
        [XEntity("User", "u", "http://www.cmsm.com")]
        public class User : BaseEntity
        {
            public Guid? BranchId { get; set; }

            public short AccessArea { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

            public int LoginFailed { get; set; }

            public bool IsLocked { get; set; }

            public DateTime CreatedDate { get; set; }

            public Guid? CreatedBy { get; set; }

            public DateTime? ModifiedDate { get; set; }

            public Guid? ModifiedBy { get; set; }

            public Guid OrganizationId { get; set; }

            public string TimeZoneId { get; set; }

            public Guid? EmailServerId { get; set; }

            public string EmailLogin { get; set; }

            public string EmailPassword { get; set; }

            public Guid RoleId { get; set; }

            public DateTime? LastLoginDate { get; set; }

            public short? CurrentBusinessType { get; set; }

            public bool CanReceiveApps { get; set; }

            public int MaxReceiveAppsPerDay { get; set; }

            public int PriorityReceiveApps { get; set; }

            public bool IsCallAgent { get; set; }

            public string WebPhoneExtension { get; set; }

            public string WebPhonePassword { get; set; }

            public string PhoneExtension { get; set; }

            public string AppPhoneExtension { get; set; }

            public string AppPhonePassword { get; set; }

            public string FaxNumber { get; set; }

            public string FaxNotifyEmail { get; set; }

            public bool HasFax { get; set; }

            public string ThemeName { get; set; }

            public int? AutoSaveAppInterval { get; set; }

            public Guid? ReleaseAnnouncementId { get; set; }

            public string SignalRConnectionId { get; set; }

            public DateTime? LockedDate { get; set; }

            public Guid? LockedBy { get; set; }

            public string Fax { get; set; }
        }

        #endregion

        #region DC

        [Serializable]
        public class BaseDC
        {
            public Guid Id { get; set; }
        }

        [Serializable]
        public sealed class DynamicFieldReferenceDC : BaseDC
        {
            public string Value { get; set; }
        }

        [Serializable]
        public class TeamUserDC
        {
            public TeamUserDC()
            {
                Users = new List<Guid>();
            }

            [XmlArray("users")]
            [XmlArrayItem(ElementName = "user", Type = typeof(Guid))]
            public List<Guid> Users { get; set; }
        }

        [Serializable]
        public class ReferenceDC
        {
            public ReferenceDC()
            {
            }

            public ReferenceDC(Guid id, string name) : this()
            {
                Id = id;
                Name = name;
            }

            public Guid Id { get; set; }

            public string Name { get; set; }

            public bool IsSelected { get; set; }

        }

        [Serializable]
        public class SupportRequestDC
        {
            public SupportRequestDC()
            {
                Id = CombGuid.New;
            }


            public Guid Id { get; set; }


            public Guid OrganizationId { get; set; }


            public string OrganizationName { get; set; }


            public short RequestType { get; set; }


            public int TicketNumber { get; set; }


            public string Email { get; set; }


            public Guid SubmittedBy { get; set; }


            public string SubmittedByName { get; set; }


            public DateTime SubmittedDate { get; set; }


            public string Phone { get; set; }


            public string Message { get; set; }


            public string BusinessFileNumber { get; set; }


            public short RequestStatus { get; set; }


            public Guid? AssignedTo { get; set; }


            public string AssignedToName { get; set; }


            public DateTime? AssignedDate { get; set; }


            public Guid? ResolvedBy { get; set; }


            public string ResolvedByName { get; set; }


            public DateTime? ResolvedDate { get; set; }


            public DateTime? LastResponseDate { get; set; }
        }

        #endregion
    }
}
