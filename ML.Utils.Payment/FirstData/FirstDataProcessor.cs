using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Configuration;
using ML.Utils.Payment.FirstDataAPI;
using log4net;

namespace ML.Utils.Payment.FirstData
{
    public class FirstDataProcessor : IFirstDataProcessor
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(FirstDataProcessor));

        private readonly FirstDataSetting _firstDataSetting;
        private readonly ServiceModelSectionGroup _serviceModelSectionGroup;

        public FirstDataProcessor(FirstDataSetting firstDataSetting)
        {
            _firstDataSetting = firstDataSetting;

            _serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(
            HttpRuntime.AppDomainAppId == null
                ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                : WebConfigurationManager.OpenWebConfiguration("~")
            );
        }

        public TransactionResult ProcessPayment(Transaction request)
        {
            try
            {
                request.ExactID = _firstDataSetting.GatewayId;
                request.Password = _firstDataSetting.PassWord;
                using (var service = CreateService())
                {
            
                    return service.SendAndCommit(request);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error call SendAndCommit", ex);
                return null;
            }
        }

        public TransactionResult GetStatus(Transaction request)
        {
             try
            {
                request.ExactID = _firstDataSetting.GatewayId;
                request.Password = _firstDataSetting.PassWord;
                using (var service = CreateService())
                {
                    return service.TransactionInfo(request);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error call Get Status", ex);
                return null;
            }      
        }

        private ServiceSoapClient CreateService()
        {
            var binding = ResolveBinding(bindingConfiguration: _firstDataSetting.BindingConfigurationName);
            var remoteAddress = new EndpointAddress(new Uri(_firstDataSetting.ApiUrl));

            var client = new ServiceSoapClient(binding, remoteAddress);
            //Add Headers 
            client.ChannelFactory.Endpoint.Behaviors.Add(new HmacHeaderBehaivour(_firstDataSetting.HmacKey, _firstDataSetting.KeyId));
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
