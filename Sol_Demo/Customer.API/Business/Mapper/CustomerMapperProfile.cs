using Customer.API.Applications.Features.Query;
using Customer.API.DTO.Request;
using Customer.API.Infrastructures.DatabaseContext;

//using Customer.API.Infrastructures.DatabaseContext;
using Customer.API.Infrastructures.DataService.Command;
using Customer.API.Infrastructures.DataService.Query;

namespace Customer.API.Business.Mapper
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            base.CreateMap<RegisterCustomerRequetsDTO, RegisterCustomerCommand>()
                   .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName))
                   .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                   .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId))
                   .ForMember((dest) => dest.Password, (opt) => opt.MapFrom((src) => src.Password));

            base.CreateMap<RegisterCustomerRequetsDTO, RegisterCustomerDataServiceCommand>()
                   .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName))
                   .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                   .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId))
                   .ForMember((dest) => dest.Password, (opt) => opt.MapFrom((src) => src.Password));

            base.CreateMap<RegisterCustomerDataServiceCommand, Customer.API.Infrastructures.DatabaseContext.Customer>()
                .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName));

            base.CreateMap<RegisterCustomerDataServiceCommand, Communication>()
                .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId));

            base.CreateMap<RegisterCustomerDataServiceCommand, Login>()
                .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId));

            base.CreateMap<AddCustomerAddressDataServiceCommand, Address>();

            base.CreateMap<AddCustomerAddressRequestDTO, AddCustomerAddressCommand>();
            base.CreateMap<AddCustomerAddressCommand, AddCustomerAddressDataServiceCommand>();

            base.CreateMap<CustomerAuthDataServiceQuery, Login>()
                .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailID));

            base.CreateMap<CustomerAuthQuery, CustomerAuthDataServiceQuery>()
                .ForMember((dest) => dest.EmailID, (opt) => opt.MapFrom((src) => src.EmailID));

            base.CreateMap<CustomerAuthRequestDTO, CustomerAuthQuery>()
                .ForMember((dest) => dest.EmailID, (opt) => opt.MapFrom((src) => src.EmailID))
                .ForMember((dest) => dest.Password, (opt) => opt.MapFrom((src) => src.Password));
        }
    }
}