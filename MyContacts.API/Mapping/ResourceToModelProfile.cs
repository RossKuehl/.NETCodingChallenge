using AutoMapper;
using MyContacts.API.Domain.Models;
using MyContacts.API.Resources;

namespace MyContacts.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveContactResource, Contact>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.First))
                .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.Name.Middle))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.Last))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.State, o => o.MapFrom(s => s.Address.State))
                .ForMember(d => d.Zip, o => o.MapFrom(s => s.Address.Zip));
        }
    }
}
