using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.Security;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Transmitter
{
    public class ServiceBuilder
    {
        private readonly TransmittalEnvironment _transmittalEnvironment;

        public ServiceBuilder(TransmittalEnvironment transmittalEnvironment)
        {
            _transmittalEnvironment = transmittalEnvironment;
        }

        public IApiService Build()
        {
            var path = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
            var rootPath = Path.GetDirectoryName(path);

            string certificateFilePath;

            switch (_transmittalEnvironment.TransmissionMode)
            {
                case ETransmissionMode.Test:
                    certificateFilePath = $@"{rootPath}\Certificates\TestEnvironmentCertificate.CER";
                    break;

                case ETransmissionMode.Production:
                    certificateFilePath = $@"{rootPath}\Certificates\ProductionEnvironmentCertificate.CER";
                    break;

                case ETransmissionMode.OnlyValidate:
                    return new ApiServiceClientMock(_transmittalEnvironment.SetupMockParameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }

            var binding = new WSHttpBinding(SecurityMode.Message);
            var message = new NonDualMessageSecurityOverHttp
            {
                ClientCredentialType = MessageCredentialType.UserName,
                NegotiateServiceCredential = false,
                EstablishSecurityContext = false
            };
            binding.Security.Message = message;

            var certificate = new X509Certificate2(X509Certificate.CreateFromCertFile(certificateFilePath));
            var uri = new Uri(_transmittalEnvironment.EndpointUrl);
            var endpointAddress = new EndpointAddress(uri, EndpointIdentity.CreateX509CertificateIdentity(certificate));

            var service = new ApiServiceClient(binding, endpointAddress);

            if (service.ClientCredentials == null)
            {
                throw new ArgumentNullException();
            }

            service.ClientCredentials.UserName.UserName = _transmittalEnvironment.Username;
            service.ClientCredentials.UserName.Password = Crypter.DecryptString(_transmittalEnvironment.Password);
            return service;
        }
    }
}
