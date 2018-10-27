using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;
using SstRegistrationTestHarness.Core.EntityFramework.Repositories;
using SstRegistrationTestHarness.Core.Exceptions;
using SstRegistrationTestHarness.Core.Repositories;
using SstRegistrationTestHarness.Core.Security;

namespace SstRegistrationTestHarness.Core.Test.Domain
{
    [TestClass]
    public class UserTest
    {
        private SstRegistrationTestHarnessSystem _root;
        private IGenericRepository<Guid> _genericRepository;
        private IUserRepository _userRepository;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            EntityFrameworkDbContextHelper.Instance.InitializeDbContextFactory(new TestFluentConfigurer());
            EntityFrameworkDbContextHelper.Instance.Context.Database.Initialize(true);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _genericRepository = new EntityFrameworkGenericRepository<Guid>();
            _userRepository = new UserRepository();

            EntityFrameworkDbContextHelper.Instance.BeginTransaction();
            
            _root = new SstRegistrationTestHarnessSystem(Guid.NewGuid());
            _genericRepository.Save(_root);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _genericRepository.Delete(_root);
            EntityFrameworkDbContextHelper.Instance.RollbackTransaction();
        }

        [TestMethod]
        public void CreateUserWithValidaData_ShouldPersist()
        {
            var user = new User("marcelo.burgos", "Sovos1234!");
            _root.AddUser(user, _genericRepository, _userRepository);

            var persistedUser = _genericRepository.GetById<User>(user.Id);
            Assert.AreEqual("marcelo.burgos", persistedUser.Username);
            Assert.AreEqual(Crypter.HashMd5("Sovos1234!"), persistedUser.Password);
        }

        [TestMethod]
        public void CreateUserWithAdditionalField_ShouldPersist()
        {
            var user = new User("marcelo.burgos", "Sovos1234!")
            {
                FirstName = "Marcelo",
                LastName = "Burgos",
                Email = "marcelo.burgos@sovos.com"
            };

            _root.AddUser(user, _genericRepository, _userRepository);
        }

        [TestMethod]
        public void ResetPassword_ShouldBeEqualToUsername()
        {
            var user = new User("marcelo.burgos", "Sovos1234!");
            user.SetPassword("Sovos1234!");
            _root.AddUser(user, _genericRepository, _userRepository);

            var persistedUser = _genericRepository.GetById<User>(user.Id);
            Assert.AreEqual(Crypter.HashMd5("Sovos1234!"), persistedUser.Password);

            persistedUser.ResetPassword();
            Assert.AreEqual(Crypter.HashMd5("marcelo.burgos"), persistedUser.Password);
        }

        [TestMethod]
        public void CreateUserWithoutUserName_ShouldThrowException()
        {
            var user1 = new User(null, "sovos1234!");
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user1, _genericRepository, _userRepository), "The username is mandatory.");
            Assert.AreEqual(nameof(User.Username), e1.PropertyName);

            var user2 = new User(string.Empty, "sovos1234!");
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user2, _genericRepository, _userRepository), "The username is mandatory.");
            Assert.AreEqual(nameof(User.Username), e2.PropertyName);

            var user3 = new User("     ", "sovos1234!");
            var e3 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user3, _genericRepository, _userRepository), "The username is mandatory.");
            Assert.AreEqual(nameof(User.Username), e3.PropertyName);
        }

        [TestMethod]
        public void CreateUserWithoutPassword_ShouldThrowException()
        {
            var user1 = new User("marcelo.burgos", null);
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user1, _genericRepository, _userRepository), "The password is mandatory.");
            Assert.AreEqual(nameof(User.Password), e1.PropertyName);

            var user2 = new User("marcelo.burgos", string.Empty);
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user2, _genericRepository, _userRepository), "The password is mandatory.");
            Assert.AreEqual(nameof(User.Password), e2.PropertyName);

            var user3 = new User("marcelo.burgos", "     ");
            var e3 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user3, _genericRepository, _userRepository), "The password is mandatory.");
            Assert.AreEqual(nameof(User.Password), e3.PropertyName);
        }

        [TestMethod]
        public void CreateUserWithSameUsername_ShouldThrowException()
        {
            var user1 = new User("marcelo.burgos", "Sovos1234!");
            _root.AddUser(user1, _genericRepository, _userRepository);
            Assert.AreEqual(1, _genericRepository.ListAll<User>().Count);

            var user2 = new User("marcelo.burgos", "Sovos1234!");
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user2, _genericRepository, _userRepository), "There is another user with the same username.");
            Assert.AreEqual(nameof(User.Username), e1.PropertyName);

            var user3 = new User("federico.navarro", "Sovos1234!");
            _root.AddUser(user3, _genericRepository, _userRepository);
            Assert.AreEqual(2, _genericRepository.ListAll<User>().Count);

            var user4 = new User("federico.navarro", "Sovos1234!");
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user4, _genericRepository, _userRepository), "There is another user with the same username.");
            Assert.AreEqual(nameof(User.Username), e2.PropertyName);
        }
    }
}
