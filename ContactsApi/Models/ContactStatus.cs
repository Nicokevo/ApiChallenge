using System;
using System.Collections.Generic;

namespace ContactsApi.Models
{
    public partial class ContactStatus
    {
        public ContactStatus()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
