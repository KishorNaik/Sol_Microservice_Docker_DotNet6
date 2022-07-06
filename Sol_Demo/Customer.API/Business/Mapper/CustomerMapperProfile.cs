using Customer.API.DTO.Request;
using Customer.API.Infrastructures.DatabaseContext;

//using Customer.API.Infrastructures.DatabaseContext;
using Customer.API.Infrastructures.DataService.Command;

namespace Customer.API.Business.Mapper
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            base.CreateMap<CreateCustomerRequetsDTO, CreateCustomerCommand>()
                   .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName))
                   .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                   .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId))
                   .ForMember((dest) => dest.Password, (opt) => opt.MapFrom((src) => src.Password));

            base.CreateMap<CreateCustomerRequetsDTO, CreateCustomerDataServiceCommand>()
                   .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName))
                   .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                   .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId))
                   .ForMember((dest) => dest.Password, (opt) => opt.MapFrom((src) => src.Password));

            base.CreateMap<CreateCustomerDataServiceCommand, Customer.API.Infrastructures.DatabaseContext.Customer>()
                .ForMember((dest) => dest.FullName, (opt) => opt.MapFrom((src) => src.FullName));

            base.CreateMap<CreateCustomerDataServiceCommand, Communication>()
                .ForMember((dest) => dest.MobileNo, (opt) => opt.MapFrom((src) => src.MobileNo))
                .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId));

            base.CreateMap<CreateCustomerDataServiceCommand, Login>()
                .ForMember((dest) => dest.EmailId, (opt) => opt.MapFrom((src) => src.EmailId));

            base.CreateMap<AddCustomerAddressDataServiceCommand, Address>();

            base.CreateMap<AddCustomerAddressRequestDTO, AddCustomerAddressCommand>();
            base.CreateMap<AddCustomerAddressCommand, AddCustomerAddressDataServiceCommand>();
        }
    }
}