using MediatR;

namespace Microservices.Application.Querys
{
    public class GetAssetValueRequest : IRequest<GetAssetValueResponse>
    {
        public decimal InitialValue { get; set; }
        public decimal CDI { get; set; }
        public decimal BankFee { get; set; }
    }
}
