namespace Microservices.Application.Querys
{
    public class GetAssetValueResponse
    {
        public decimal FinalValue { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CDI { get; set; }
        public decimal BankFee { get; set; }
    }
}
