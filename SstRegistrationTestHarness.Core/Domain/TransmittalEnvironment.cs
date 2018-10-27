using System;
using SstRegistrationTestHarness.Core.Exceptions;
using SstRegistrationTestHarness.Core.Repositories;
using SstRegistrationTestHarness.Core.Security;
using SstRegistrationTestHarness.Core.Transmitter;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TransmittalEnvironment
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string TransmissionVersion { get; set; }
        public string EndpointUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ETransmissionMode TransmissionMode { get; set; }
        public ServiceMockParameters SetupMockParameters { get; set; }

        protected  TransmittalEnvironment()
        {
        }

        public TransmittalEnvironment(string name, string endpointUrl, string username, string password, string transmissionVersion, ETransmissionMode transmissionMode)
        {
            Id = Guid.NewGuid();
            Name = name;
            EndpointUrl = endpointUrl?.Trim();
            Username = username?.Trim();
            Password = password?.Trim();
            TransmissionVersion = transmissionVersion?.Trim();
            TransmissionMode = transmissionMode;

            if (!string.IsNullOrEmpty(Password))
            {
                Password = Crypter.EncryptString(Password);
            }
        }

        public void Validate(ITransmittalEnvironmentRepository transmittalEnvironmentRepository)
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new EntityModelException(nameof(Username), "The username is mandatory.");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new EntityModelException(nameof(Password), "The password is mandatory.");
            }

            if (string.IsNullOrEmpty(TransmissionVersion))
            {
                throw new EntityModelException(nameof(TransmissionVersion), "The transmission version is mandatory.");
            }
        }

        public ServiceBuilder CrateServiceBuilder()
        {
            return new ServiceBuilder(this);
        }
    }
}
