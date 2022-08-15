using AutoMapper;
using MediatR;
using Wallets.API.DTO.Request;
using Wallets.API.Infrastructure.DatabaseContext;

namespace Wallets.API.Infrastructures.Dataservice.Command
{
    public class CreateWalletDataServiceCommand : CreateWalletRequestDTO, INotification
    {
    }

    public class CreateWalletDataServiceCommandHandler : INotificationHandler<CreateWalletDataServiceCommand>
    {
        private readonly WalletContext? walletContext;
        private readonly IMapper? mapper;

        public CreateWalletDataServiceCommandHandler(WalletContext? walletContext, IMapper mapper)
        {
            this.walletContext = walletContext;
            this.mapper = mapper;
        }

        async Task INotificationHandler<CreateWalletDataServiceCommand>.Handle(CreateWalletDataServiceCommand notification, CancellationToken cancellationToken)
        {
            using var transaction = await this?.walletContext?.Database?.BeginTransactionAsync(cancellationToken)!;
            try
            {
                Wallet wallet = this?.mapper?.Map<Wallet>(notification)!;

                wallet.WalletIdentifier = Guid.NewGuid();
                wallet.Balance = 0;

                await this.walletContext.Wallets.AddAsync(wallet);
                await this.walletContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction?.RollbackAsync(cancellationToken)!;
            }
            finally
            {
                await this.walletContext.DisposeAsync();
            }
        }
    }
}