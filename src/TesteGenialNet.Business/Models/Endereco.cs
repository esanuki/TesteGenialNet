using System.Security.Principal;

namespace TesteGenialNet.Business.Models
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }

        public override string ToString()
        {
            return $"{Logradouro}, {Bairro}, {CEP}, {Localidade}-{UF}";
        }
    }
}
