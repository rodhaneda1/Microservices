namespace Microservices.Domain.Dtos
{

    public class ErrorDto
    {
        public string status { get; set; }

        public int codErro { get; set; }

        public string descricaoErro { get; set; }

        public string ok { get; set; }
    }
}
