using System.ComponentModel.DataAnnotations;

namespace PropertyHandler.Api.ViewModels
{
    public class PropertyViewModel
    {
        public int Codigo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        public decimal ValorAluguel { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCondominio { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string NomeProprietario { get; set; }
        public bool Destaque { get; set; }
        public AddressViewModel Address { get; set; }
        public DetailsViewModel Details { get; set; }
    }
}
