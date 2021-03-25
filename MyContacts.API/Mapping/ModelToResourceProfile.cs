using AutoMapper;
using MyContacts.API.Domain.Models;
using MyContacts.API.Resources;

namespace MyContacts.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Contact, ContactResource>()
                .ForMember(d => d.Name, o => o.MapFrom(
                    s => new NameResource
                    {
                        First = s.FirstName,
                        Middle = s.MiddleName,
                        Last = s.LastName
                    }))
                .ForMember(d => d.Address, o => o.MapFrom(
                    s => new AddressResource
                    {
                        Street = s.Street,
                        City = s.City,
                        State = s.State,
                        Zip = s.Zip
                    }));

            CreateMap<Phone, PhoneResource>();
        }
    }
}
