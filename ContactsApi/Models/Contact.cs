﻿using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace ContactsApi.Models
{
    public class Contact
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? ProfileImage { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? WorkPhoneNumber { get; set; }
        public string? PersonalPhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }


    }
}
