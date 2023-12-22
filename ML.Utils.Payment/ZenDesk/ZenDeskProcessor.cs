using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using log4net;

namespace ML.Utils.Payment.ZenDesk
{
    public class ZenDeskProcessor
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ZenDeskProcessor));

        private readonly ZenDeskSetting _zenDeskSetting;
        private readonly ServiceModelSectionGroup _serviceModelSectionGroup;

        public ZenDeskProcessor(ZenDeskSetting zenDeskSetting)
        {
            _zenDeskSetting = zenDeskSetting;

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
                request.ExactID = _zenDeskSetting.GatewayId;
                request.Password = _zenDeskSetting.PassWord;
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

        private ServiceSoapClient CreateService()
        {
            var binding = ResolveBinding(bindingConfiguration: _zenDeskSetting.BindingConfigurationName);
            var remoteAddress = new EndpointAddress(new Uri(_zenDeskSetting.ApiUrl));

            var client = new ServiceSoapClient(binding, remoteAddress);
            //Add Headers 
            client.ChannelFactory.Endpoint.Behaviors.Add(new HmacHeaderBehaivour(_zenDeskSetting.HmacKey, _zenDeskSetting.KeyId));
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
