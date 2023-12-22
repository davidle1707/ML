using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Configuration;
using ML.Utils.Payment.PrismPayAPI;
using log4net;

namespace ML.Utils.Payment.PrismPay
{
    public class PrismPayProcessor 
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(PrismPayProcessor));

        private readonly PrismPaySetting _prismPaySetting;
        private readonly ServiceModelSectionGroup _serviceModelSectionGroup;

        public PrismPayProcessor(PrismPaySetting prismPaySetting)
        {
            _prismPaySetting = prismPaySetting;

            _serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(
            HttpRuntime.AppDomainAppId == null
                ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                : WebConfigurationManager.OpenWebConfiguration("~")
            );
        }

        public ProcessResult ProcessPayment(CreditCardInfo request)
        {
            try
            {
                request.acctid = _prismPaySetting.AccountId;
                using (var service = CreateService())
                {
                    return service.processCCSale(request);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error call ProcessPayment", ex);
                return null;
            }
        }

        private TransactionSOAPBindingImplClient CreateService()
        {
            var binding = ResolveBinding(bindingConfiguration: _prismPaySetting.BindingConfigurationName);
            var remoteAddress = new EndpointAddress(new Uri(_prismPaySetting.ApiUrl));

            var client = new TransactionSOAPBindingImplClient(binding, remoteAddress);
           
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
