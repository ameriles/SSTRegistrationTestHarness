using System.Collections.Generic;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface ITransmittalLogRepository
    {
        IList<TransmittalLog> FilterByParameters();
    }
}