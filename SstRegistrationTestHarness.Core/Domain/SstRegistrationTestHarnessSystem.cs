using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class SstRegistrationTestHarnessSystem
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        
        public static Expression<Func<SstRegistrationTestHarnessSystem, ICollection<User>>> UsersAccessor = x => x.UserStorage;
        protected virtual ICollection<User> UserStorage { get; set; }
        public virtual IReadOnlyCollection<User> Users => UserStorage.ToList();

        public static Expression<Func<SstRegistrationTestHarnessSystem, ICollection<Contractor>>> ContractorAccessor = x => x.ContractorStorage;
        protected virtual ICollection<Contractor> ContractorStorage { get; set; }
        public virtual IReadOnlyCollection<Contractor> Contractors =>  ContractorStorage.ToList();

        protected SstRegistrationTestHarnessSystem()
        {
        }

        public SstRegistrationTestHarnessSystem(Guid id)
        {
            Id = id;
            Name = "SstRegistrationTestHarnessSystem";
            UserStorage = new Collection<User>();
            ContractorStorage = new Collection<Contractor>();
        }

        public void AddUser(User user, IGenericRepository<Guid> genericRepository, IUserRepository userRepository)
        {
            user.Validate(userRepository);
            UserStorage.Add(user);
            genericRepository.Save(user);
        }

        public void AddContractor(Contractor contractor, IGenericRepository<Guid> genericRepository, IContractorRepository contractorRepository)
        {
            contractor.Validate(contractorRepository);
            ContractorStorage.Add(contractor);
            genericRepository.Save(contractor);
        }

        public void AddTransmittalLog(TransmittalLog log, IGenericRepository<Guid> genericRepository)
        {
            log.SstRegistrationTestHarnessSystem = this;
            genericRepository.Save(log);
        }
    }
}
