using System.Runtime.Serialization;

namespace Wallets.API.DTO.Request
{
    [DataContract]
    public class CreateWalletRequestDTO
    {
        [DataMember]
        public Guid? CustomerIdentifier { get; set; }
    }
}