using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface ITransmittalEnvironmentRepository
    {
        bool IsUnique(TransmittalEnvironment transmittalEnvironment);
        TransmittalEnvironment GetTransmittalEnvironmentForMode(ETransmissionMode transmissionMode);
    }
}