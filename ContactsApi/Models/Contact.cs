using System;
using System.Collections.Generic;

namespace ContactsApi.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int? AddressId { get; set; }
        public int? CompanyId { get; set; }
        public int? ContactStatusId { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ContactStatus? ContactStatus { get; set; }
    }
}
