using AgendaApp.Domain.Validation;
using System.Text.Json.Serialization;

namespace AgendaApp.Domain.Entities
{
    public sealed class Contact : Entity
    {
        public string? Name { get; protected set; }
        public string? Email { get; protected set; }
        public string? Phone { get; protected set; }
       

        public Contact() { }

        [JsonConstructor]
        public Contact(int id, string name, string email, string phone)
        {
            DomainValidation.When(id < 0 ,"Invalid id");
            Id = id;
            ValidateDomain(name, email, phone);
        }

        public void Update(string? name, string? email, string? phone)
        {   
            ValidateDomain(name, email, phone);
        }
        private void ValidateDomain(string name, string email, string phone)
        {
            #region name
            DomainValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required");

            DomainValidation.When(name?.Length > 50,
                "Invalid name, too long, maximum 50 characters");

            DomainValidation.When(name?.Length < 3,
                 "Invalid name, too short, minimum 3 characters");
            #endregion

            #region email
            DomainValidation.When(string.IsNullOrEmpty(email),
               "Invalid email. email is required");

            DomainValidation.When(email?.Length > 50,
               "Invalid email, too long, maximum 50 characters");

            DomainValidation.When(email?.Length < 5,
                "Invalid email, too short, minimum 5 characters");
            #endregion

            #region phone
            DomainValidation.When(string.IsNullOrEmpty(phone),
                "Invalid phone. phone is required");

            DomainValidation.When(phone?.Length > 20,
                "Invalid phone, too long, maximum 20 characters");

            DomainValidation.When(phone?.Length < 6,
                 "Invalid phone, too short, minimum 6 characters");
            #endregion

            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
