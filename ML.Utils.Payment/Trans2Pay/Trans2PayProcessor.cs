using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using ML.Utils.Payment.Trans2PayAPI;
using ML.Utils.Payment.Trans2Pay.Info;
using ML.Utils.Trans2Pay;
using System.Collections.Generic;

namespace ML.Utils.Payment.Trans2Pay
{
    public class Trans2PayProcessor : BaseClass
    {
        private int _pageSize = 20;
        private int _pageNumber = 1;
        private int? _rowCount = 20;
        private int? _pageCount = 1;
        //Properties
        private readonly Trans2PaySetting _trans2PaySetting;
        private readonly ServiceModelSectionGroup _serviceModelSectionGroup;
        //Contructor
        public Trans2PayProcessor(Trans2PaySetting trans2PaySetting)
        {
            _trans2PaySetting = trans2PaySetting;

            _serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(
             HttpRuntime.AppDomainAppId == null
                 ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                 : WebConfigurationManager.OpenWebConfiguration("~")
             );
        }

        public bool SetClient(ClientInfo clientInfo, ref string accountId, ref string errorMessage)
        {
            try
            {
                using (var service = CreateService())
                {
                    var existsClient = service.ClientsListGetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord,
                                                                accountId, "", "", "");

                    var clientsWsdsObject = new ClientsWSDS();
                    DataRow clientRow = clientsWsdsObject.Tables[0].NewRow();
                    if (string.IsNullOrEmpty(accountId) || (existsClient.CLIENTS.Count == 0 && existsClient.ERRORS.Count == 0))
                    {
                        //Get Next Available Account
                        int numAccountRemaining = 0;
                        var nextAccount = service.NextAccountGetADO(_trans2PaySetting.UserName,
                                                                    _trans2PaySetting.PassWord, 1,
                                                                    ref numAccountRemaining);

                        if (nextAccount.ACCOUNTS.Count > 0)
                        {
                            clientRow["ACCOUNT_ID"] = nextAccount.ACCOUNTS[0].ACCOUNT_ID;
                        }
                        else
                        {
                            errorMessage = "No more accounts are available..!";
                            return false;
                        }
                    }
                    else
                    {
                        clientRow["ACCOUNT_ID"] = existsClient.CLIENTS[0].ACCOUNT_ID;
                    }

                    clientRow["CLIENT_ID"] = clientInfo.ClientId;
                    clientRow["LAST_NAME"] = clientInfo.LastName;
                    clientRow["FIRST_NAME"] = clientInfo.FirstName;
                    clientRow["SOC_SEC_NUM"] = clientInfo.SocSecNum;
                    clientRow["DATE_OF_BIRTH"] = clientInfo.DateOfBirth;
                    clientRow["ADDRESS1"] = clientInfo.Address1;
                    clientRow["CITY"] = clientInfo.City;
                    clientRow["STATE"] = clientInfo.State;
                    clientRow["COUNTRY"] = clientInfo.Country;
                    clientRow["ZIPCODE"] = clientInfo.ZipCode;
                    clientRow["BANK_ROUTING_NUM"] = clientInfo.BankRoutingNum;
                    clientRow["BANK_ACCOUNT_NUM"] = clientInfo.BankAccountNum;
                    clientRow["BANK_ACCOUNT_TYPE"] = clientInfo.BankAccountType;
                    clientRow["ACTIVE_FLAG"] = clientInfo.ActiveFlag;
                    clientRow["PHONE_NUM"] = clientInfo.PhoneNumber;
                    clientRow["POLICY_GROUP_ID"] = clientInfo.PolicyGroupId;
                    clientsWsdsObject.Tables["CLIENTS"].Rows.Add(clientRow);
                    clientRow.AcceptChanges();

                    if (string.IsNullOrEmpty(accountId) || (existsClient.CLIENTS.Count == 0 && existsClient.ERRORS.Count == 0))
                        clientRow.SetAdded();
                    else
                        clientRow.SetModified();

                    var clientsSetAdo = service.ClientsSetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord,
                                                              clientsWsdsObject);

                    //Remove warning 
                    var drWarning = clientsSetAdo.ERRORS.Select("ERROR_TYPE='W'");
                    foreach (var warningRow in drWarning)
                        clientsSetAdo.ERRORS.Rows.Remove(warningRow);

                    if (drWarning.Any())
                        clientsSetAdo.ERRORS.AcceptChanges();

                    if (clientsSetAdo.ERRORS.Count > 0)
                    {
                        errorMessage = getStrError(clientsSetAdo.ERRORS[0].ERROR_CODE, clientsSetAdo.ERRORS[0].ERROR_TEXT);
                        clientsSetAdo.RejectChanges();
                        return false;
                    }

                    accountId = clientsSetAdo.CLIENTS[0].ACCOUNT_ID;
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return false;
            }
        }
        public bool SetDraft(string accountId, DebitInfo debitInfo, ref string errorMessage)
        {
            try
            {
                using (var service = CreateService())
                {
                    var client = service.ClientsListGetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord,
                                                           accountId, "", "", "");
                    if (!client.CLIENTS.Any())
                    {
                        errorMessage = "T2P account doesn't available..!";
                        return false;
                    }

                    var existDraftGet = service.DraftsGetADO(_trans2PaySetting.UserName,
                                                             _trans2PaySetting.PassWord, null,
                                                             client.CLIENTS[0].ACCOUNT_ID, "",
                                                             "", null,
                                                             debitInfo.DebitType, null, null, "", "", "",
                                                             "", "", "", debitInfo.DrcTransactionId, "", "", null, null,
                                                             "", "", "",
                                                             _pageSize, _pageNumber, ref _rowCount, ref _pageCount);

                    var debitsWsdsObject = new DebitsWSDS();
                    DataRow debitsRow = debitsWsdsObject.Tables["DEBITS"].NewRow();
                    debitsRow["DEBIT_ID"] = (existDraftGet.DEBITS.Count == 0 && existDraftGet.ERRORS.Count == 0)
                                                ? -1
                                                : existDraftGet.DEBITS[0].DEBIT_ID;
                    debitsRow["ACCOUNT_ID"] = client.CLIENTS[0].ACCOUNT_ID;
                    debitsRow["EFFECTIVE_DATE"] = AdjustDateTimeValue(debitInfo.EffectiveDate);
                    debitsRow["DAY_OF_MONTH"] = debitInfo.EffectiveDate.Day;
                    debitsRow["DEBIT_TYPE"] = debitInfo.DebitType;
                    debitsRow["OCCURS_NUM"] = 1;
                    debitsRow["DEBIT_AMOUNT"] = debitInfo.Amount;
                    debitsRow["MEMO"] = debitInfo.Memo;
                    debitsRow["ACTIVE_FLAG"] = debitInfo.ActiveFlag;
                    debitsRow["DRC_TRANSACTION_ID"] = debitInfo.DrcTransactionId;
                    debitsWsdsObject.Tables["DEBITS"].Rows.Add(debitsRow);
                    debitsRow.AcceptChanges();

                    if (existDraftGet.DEBITS.Count == 0 && existDraftGet.ERRORS.Count == 0)
                        debitsRow.SetAdded();
                    else
                        debitsRow.SetModified();

                    var draftDebitsSet = service.DraftsSetADO(_trans2PaySetting.UserName,
                                                              _trans2PaySetting.PassWord, debitsWsdsObject);
                    if (draftDebitsSet.ERRORS.Count > 0)
                    {
                        errorMessage = getStrError(draftDebitsSet.ERRORS[0].ERROR_CODE, draftDebitsSet.ERRORS[0].ERROR_TEXT);
                        existDraftGet.RejectChanges();
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return false;
            }
        }
        public bool SetFee(string accountId, DebitInfo debitInfo, ref string errorMessage)
        {
            try
            {
                using (var service = CreateService())
                {
                    var client = service.ClientsListGetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord,
                                                           accountId, "", "", "");

                    if (!client.CLIENTS.Any())
                    {
                        errorMessage = "T2P account doesn't available..!";
                        return false;
                    }

                    var existFeeGet = service.FeesGetADO(_trans2PaySetting.UserName,
                                                             _trans2PaySetting.PassWord, null,
                                                             client.CLIENTS[0].ACCOUNT_ID, "",
                                                             "", null,
                                                             "", null, null, "", "", "",
                                                             "", "", "", debitInfo.DrcTransactionId, "", "", null, null,
                                                             "", "", "",
                                                             _pageSize, _pageNumber, ref _rowCount, ref _pageCount);

                    var debitsWsdsObject = new DebitsWSDS();
                    DataRow debitsRow = debitsWsdsObject.Tables["DEBITS"].NewRow();
                    debitsRow["DEBIT_ID"] = (existFeeGet.DEBITS.Count == 0 && existFeeGet.ERRORS.Count == 0)
                                                ? -1
                                                : existFeeGet.DEBITS[0].DEBIT_ID;
                    debitsRow["ACCOUNT_ID"] = client.CLIENTS[0].ACCOUNT_ID;
                    debitsRow["EFFECTIVE_DATE"] = AdjustDateTimeValue(debitInfo.EffectiveDate);
                    debitsRow["DAY_OF_MONTH"] = debitInfo.EffectiveDate.Day;
                    debitsRow["DEBIT_TYPE"] = debitInfo.DebitType;
                    debitsRow["OCCURS_NUM"] = 1;
                    debitsRow["DEBIT_AMOUNT"] = debitInfo.Amount;
                    debitsRow["MEMO"] = debitInfo.Memo;
                    debitsRow["ACTIVE_FLAG"] = debitInfo.ActiveFlag;
                    debitsRow["DRC_TRANSACTION_ID"] = debitInfo.DrcTransactionId;
                    debitsWsdsObject.Tables["DEBITS"].Rows.Add(debitsRow);
                    debitsRow.AcceptChanges();

                    if (existFeeGet.DEBITS.Count == 0 && existFeeGet.ERRORS.Count == 0)
                        debitsRow.SetAdded();
                    else
                        debitsRow.SetModified();

                    var feeDebitsSet = service.FeesSetADO(_trans2PaySetting.UserName,
                                                              _trans2PaySetting.PassWord, debitsWsdsObject);
                    if (feeDebitsSet.ERRORS.Count > 0)
                    {
                        errorMessage = getStrError(feeDebitsSet.ERRORS[0].ERROR_CODE, feeDebitsSet.ERRORS[0].ERROR_TEXT);
                        existFeeGet.RejectChanges();
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return false;
            }
        }

        public string GetPolicyId(string policyGroupName)
        {
            using (var service = CreateService())
            {
                var policies = service.PolicyGroupsGetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord, "");
                string expression = "DESCRIPTION = '" + policyGroupName + "'";
                var policy = policies.POLICY_GROUPS.Select(expression);
                if (policy.Any())
                {
                    return policy[0]["POLICY_GROUP_ID"].ToString();
                }
            }
            return string.Empty;
        }
        public DataTable GetFeeInfos(string policyId, ref string errorMessage)
        {
            var dt = new DataTable();
            using (var service = CreateService())
            {
                var fees = service.FeeInfoGetADO(_trans2PaySetting.UserName, _trans2PaySetting.PassWord, policyId, "");
                if (fees.ERRORS.Count > 0)
                {
                    errorMessage = fees.ERRORS[0].ERROR_TEXT;
                    return dt;
                }

                dt = fees.DEBIT_TYPES;
            }
            return dt;
        }
        public DataTable GetPolicies(ref string errorMessage)
        {
            var dt = new DataTable();
            using (var service = CreateService())
            {
                var draftsGet = service.PolicyGroupsGetADO(_trans2PaySetting.UserName,
                                                     _trans2PaySetting.PassWord, "Y");
                if (draftsGet.ERRORS.Count > 0)
                {
                    errorMessage = draftsGet.ERRORS[0].ERROR_TEXT;
                    return dt;
                }

                dt = draftsGet.POLICY_GROUPS;
            }

            return dt;
        }
        public DataTable GetDrafts(DateTime fromDate, DateTime toDate, DebitType type)
        {
            var finish = false;
            var dt = new DataTable();
            using (var service = CreateService())
            {
                while (!finish)
                {
                    var draftsGet = service.DraftsGetADO(_trans2PaySetting.UserName,
                                                         _trans2PaySetting.PassWord,
                                                         null, "",
                                                         fromDate.ToShortDateString(), toDate.ToShortDateString(),
                                                         null, type.ToString(), null, null, "",
                                                         "", "", "", "", "", "", "", "", null, null, "",
                                                         "", "", _pageSize, _pageNumber, ref _rowCount,
                                                         ref _pageCount);
                    if (draftsGet.DEBITS.Count == 0)
                    {
                        finish = true;
                    }
                    else
                    {
                        dt.Merge(draftsGet.DEBITS);
                        _pageNumber++;
                    }
                }
            }

            return dt;
        }
        public DataTable GetTransactions(DateTime fromDate, DateTime toDate, DebitType type)
        {
            var finish = false;
            var dt = new DataTable();
            using (var service = CreateService())
            {
                while (!finish)
                {
                    var transactionsGet = service.TransactionsGetADO(_trans2PaySetting.UserName,
                                                                     _trans2PaySetting.PassWord,
                                                                     null, "", "", "",
                                                                     fromDate.ToShortDateString(),
                                                                     toDate.ToShortDateString(),
                                                                     "", "", null,
                                                                     "", type.ToString(), "", null, null, null,
                                                                     "", "", "", "", "", "", "",
                                                                     null, null, _pageSize, _pageNumber, ref _rowCount,
                                                                     ref _pageCount);
                    if (transactionsGet.TRANSACTIONS.Count == 0)
                    {
                        finish = true;
                    }
                    else
                    {
                        dt.Merge(transactionsGet.TRANSACTIONS);
                        _pageNumber++;
                    }
                }
            }

            return dt;
        }
        public TransactionsWSDS.TRANSACTIONSRow GetDraftTransactionByExternalId(string externalId)
        {
            if (string.IsNullOrEmpty(externalId))
                return null;
            using (var service = CreateService())
            {
                var draftsGet = service.DraftsGetADO(_trans2PaySetting.UserName,
                                                     _trans2PaySetting.PassWord,
                                                     null,
                                                     "", "",
                                                     "", null, "", null, null, "",
                                                     "", "", "", "", "", externalId, "", "", null, null, "",
                                                     "", "", _pageSize, _pageNumber, ref _rowCount,
                                                     ref _pageCount);
                if (draftsGet.DEBITS.Count > 0)
                {
                    var transactionsGet = service.TransactionsGetADO(_trans2PaySetting.UserName,
                                                                     _trans2PaySetting.PassWord,
                                                                     null, "", "", "", "", "", "", "", null,
                                                                     "", "", "", null, null,
                                                                     draftsGet.DEBITS[0].DEBIT_ID,
                                                                     "", "", "", "", "", "", "",
                                                                     null, null, _pageSize, _pageNumber, ref _rowCount,
                                                                     ref _pageCount);

                    if (transactionsGet.TRANSACTIONS.Count > 0)
                        return transactionsGet.TRANSACTIONS[0];
                }
            }
            return null;
        }
        public TransactionsWSDS.TRANSACTIONSRow GetFeeTransactionByExternalId(string externalId)
        {
            if (string.IsNullOrEmpty(externalId))
                return null;
            using (var service = CreateService())
            {
                var feesGet = service.FeesGetADO(_trans2PaySetting.UserName,
                                                     _trans2PaySetting.PassWord,
                                                     null,
                                                     "", "",
                                                     "", null, "", null, null, "",
                                                     "", "", "", "", "", externalId, "", "", null, null, "",
                                                     "", "", _pageSize, _pageNumber, ref _rowCount,
                                                     ref _pageCount);
                if (feesGet.DEBITS.Count > 0)
                {
                    var transactionsGet = service.TransactionsGetADO(_trans2PaySetting.UserName,
                                                                     _trans2PaySetting.PassWord,
                                                                     null, "", "", "", "", "", "", "", null,
                                                                     "", "", "", null, null,
                                                                     feesGet.DEBITS[0].DEBIT_ID,
                                                                     "", "", "", "", "", "", "",
                                                                     null, null, _pageSize, _pageNumber, ref _rowCount,
                                                                     ref _pageCount);

                    if (transactionsGet.TRANSACTIONS.Count > 0)
                        return transactionsGet.TRANSACTIONS[0];
                }
            }
            return null;
        }
        public TransactionsWSDS.TRANSACTIONSRow GetDepositByCheckNumber(string checkNumber)
        {
            if (string.IsNullOrEmpty(checkNumber))
                return null;

            using (var service = CreateService())
            {
                var depositsGet = service.DepositsGetADO(_trans2PaySetting.UserName,
                                                                 _trans2PaySetting.PassWord, "",
                                                                 "", null, "", "", checkNumber, "",
                                                                 "", "", "", "", "", "", "", "", "", "", "", "",
                                                                 "", "", _pageSize, _pageNumber,
                                                                 ref _rowCount, ref _pageCount);


                if (depositsGet.DEPOSITS.Count > 0)
                {
                    var transactionsGet = service.TransactionsGetADO(_trans2PaySetting.UserName,
                                                                    _trans2PaySetting.PassWord,
                                                                    null, "", "", "", "", "", "", "", null,
                                                                    "", "", "", null, depositsGet.DEPOSITS[0].DEPOSIT_ID,
                                                                    null,
                                                                    "", "", "", "", "", "", "",
                                                                    null, null, _pageSize, _pageNumber, ref _rowCount,
                                                                    ref _pageCount);

                    if (transactionsGet.TRANSACTIONS.Count > 0)
                        return transactionsGet.TRANSACTIONS[0];
                }
            }
            return null;
        }

        //Utilities
        private DateTime AdjustDateTimeValue(DateTime localDate)
        {
            int cstOffset = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time").BaseUtcOffset.Hours;
            return localDate.AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours - cstOffset);
        }
        private WebServicesSoapClient CreateService()
        {
            var binding = ResolveBinding(bindingConfiguration: _trans2PaySetting.BindingConfigurationName);
            var remoteAddress = new EndpointAddress(new Uri(_trans2PaySetting.ApiUrl));

            var client = new WebServicesSoapClient(binding, remoteAddress);

            return client;
        }
        private Binding ResolveBinding(string binding = "basicHttpBinding", string bindingConfiguration = "")
        {
            Binding resolveBinding;

            var bindingElement = _serviceModelSectionGroup.Bindings.BindingCollections.Single(s => s.BindingName == binding);

            var config = bindingElement.ConfiguredBindings.FirstOrDefault(x => x.Name == bindingConfiguration);

            if (config != null)
            {
                resolveBinding = (Binding)Activator.CreateInstance(bindingElement.BindingType, config.Name);
                config.ApplyConfiguration(resolveBinding);
            }
            else
            {
                resolveBinding = (Binding)Activator.CreateInstance(bindingElement.BindingType);
            }

            return resolveBinding;
        }
    }

}
