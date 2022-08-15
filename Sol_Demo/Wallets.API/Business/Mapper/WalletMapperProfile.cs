using AutoMapper;
using Wallets.API.DTO.Request;
using Wallets.API.Infrastructure.DatabaseContext;
using Wallets.API.Infrastructures.Dataservice.Command;

namespace Wallets.API.Business.Mapper
{
    public class WalletMapperProfile : Profile
    {
        public WalletMapperProfile()
        {
            this.CreateWalletDataServiceCommandToWalletEntity();
            this.CreateWalletRequetDTOToCreateWalletDataServiceCommand();
        }

        private void CreateWalletRequetDTOToCreateWalletDataServiceCommand()
        {
            base.CreateMap<CreateWalletRequestDTO, CreateWalletDataServiceCommand>()
                .ForMember((dest) => dest.CustomerIdentifier, (opt) => opt.MapFrom((src) => src.CustomerIdentifier));
        }

        private void CreateWalletDataServiceCommandToWalletEntity()
        {
            base.CreateMap<CreateWalletDataServiceCommand, Wallet>()
                .ForMember((dest) => dest.CustomerIdentifier, (opt) => opt.MapFrom((src) => src.CustomerIdentifier));
        }
    }
}