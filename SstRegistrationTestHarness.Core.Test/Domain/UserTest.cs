using System;
using System.IO;
using System.Linq;
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
            Assert.AreEqual(1, _genericRepository.Count<User>());

            var user2 = new User("marcelo.burgos", "Sovos1234!");
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user2, _genericRepository, _userRepository), "There is another user with the same username.");
            Assert.AreEqual(nameof(User.Username), e1.PropertyName);

            var user3 = new User("federico.navarro", "Sovos1234!");
            _root.AddUser(user3, _genericRepository, _userRepository);
            Assert.AreEqual(2, _genericRepository.Count<User>());

            var user4 = new User("federico.navarro", "Sovos1234!");
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddUser(user4, _genericRepository, _userRepository), "There is another user with the same username.");
            Assert.AreEqual(nameof(User.Username), e2.PropertyName);
        }

        [TestMethod]
        public void CreateManyUsers_RetrievePages_ShouldWork()
        {
            for (int i = 0; i < 36; i++)
            {
                var user = new User(Path.GetRandomFileName(), "Sovos1234!");
                _root.AddUser(user, _genericRepository, _userRepository);
            }

            Assert.AreEqual(36, _genericRepository.Count<User>());

            var page1 = _genericRepository.ListAllPaged<User>(1, 10);
            Assert.AreEqual(10, page1.Count());

            var page2 = _genericRepository.ListAllPaged<User>(2, 10);
            Assert.AreEqual(10, page2.Count());

            var page3 = _genericRepository.ListAllPaged<User>(3, 10);
            Assert.AreEqual(10, page3.Count());

            var page4 = _genericRepository.ListAllPaged<User>(4, 10);
            Assert.AreEqual(6, page4.Count());
        }

        [TestMethod]
        public void CreateManyUsers_RetrievePages_WithSearch_ShouldWork()
        {
            for (int i = 0; i < 36; i++)
            {
                var user = new User($"agustin_{i}", "Sovos1234!");
                _root.AddUser(user, _genericRepository, _userRepository);
            }

            Assert.AreEqual(36, _genericRepository.Count<User>());

            // should be _1, _10, ..., _19, _21, _31 => 13 items
            var page1 = _genericRepository.ListByQueryPaged<User>(u => u.Username.Contains("1"), 1, 10);
            Assert.AreEqual(10, page1.Count());

            var page2 = _genericRepository.ListByQueryPaged<User>(u => u.Username.Contains("1"), 2, 10);
            Assert.AreEqual(3, page2.Count());
        }
    }
}
