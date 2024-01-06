using System;
using System.Collections.Generic;

namespace ContactsApi.Models
{
    public partial class Company
    {
        public Company()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
