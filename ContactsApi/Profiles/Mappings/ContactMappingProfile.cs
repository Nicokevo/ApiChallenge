using ContactsApi.Dtos;
using ContactsApi.Models;
using AutoMapper;

namespace ContactsApi.Profiles.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
           
            CreateMap<ContactDto, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ReverseMap();

         
            CreateMap<Contact, ContactDto>();
        }
    }
}
