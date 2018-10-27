using System;
using SstRegistrationTestHarness.Core.Exceptions;
using SstRegistrationTestHarness.Core.Repositories;
using SstRegistrationTestHarness.Core.Security;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected User()
        {
        }

        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username?.Trim();

            if (!string.IsNullOrEmpty(password?.Trim()))
            {
                Password = Crypter.HashMd5(password);
            }
        }

        public void Validate(IUserRepository userRepository)
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new EntityModelException(nameof(Username), "The username is mandatory.");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new EntityModelException(nameof(Password), "The password is mandatory.");
            }

            if (!userRepository.IsUnique(this))
            {
                throw new EntityModelException(nameof(Username), "There is another user with the same username.");
            }
        }

        public void SetPassword(string password)
        {
            Password = Crypter.HashMd5(password);
        }

        public void ResetPassword()
        {
            Password = Crypter.HashMd5(Username);
        }
    }
}
