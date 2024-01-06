using System;
using System.Collections.Generic;

namespace ContactsApi.Models
{
    public partial class Address
    {
        public Address()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
