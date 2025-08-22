using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class User: BaseEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;


        protected User(){}

        internal User(string fullName, string email, string password )
        {
            Id = Guid.NewGuid();
            FullName = fullName.Trim();
            Email = email;
            Password = BCrypt.Net.BCrypt.HashPassword(password.Trim());
        }

        internal void Update(string fullName, string email)
        {
            FullName = fullName.Trim();
            Email = email;
        }

        internal void UpdatePassword(string fullName, string email)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(fullName.Trim());
        }

        internal void Delete()
        {
            IsDeleted = true;
            DeletedDate = DateTime.Now;
        }
    }
}
