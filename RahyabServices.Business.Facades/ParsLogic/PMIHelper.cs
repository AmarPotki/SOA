using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using RahyabServices.Business.Facades.Proxy.wPMI;
namespace RahyabServices.Business.Facades.ParsLogic

{
    internal class PMIHelper
    {
        protected string address = "";
        public IpmiClient client;

        public PMIHelper(string _address)
        {
            address = _address;
        }
        public void Login(string a_Username, string a_Password)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            if (address.ToLower().StartsWith("https"))
            {
                var binding = new CustomBinding();
                binding.Elements.Add(new ReliableSessionBindingElement());
                binding.Elements.Add(new MtomMessageEncodingBindingElement());
                var transport = new HttpsTransportBindingElement();
                transport.MaxReceivedMessageSize = 64000000;
                transport.MaxBufferPoolSize = 64000000;
                binding.Elements.Add(transport);
                client = new IpmiClient(binding, new EndpointAddress(address));
            }
            else
            {
                var binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.None;
                binding.ReliableSession.Enabled = true;
                binding.MaxReceivedMessageSize = 64000000;
                binding.MaxBufferPoolSize = 64000000;
                client = new IpmiClient(binding, new EndpointAddress(address));
            }

          

            client.login(a_Username, a_Password);
        }
    }
}