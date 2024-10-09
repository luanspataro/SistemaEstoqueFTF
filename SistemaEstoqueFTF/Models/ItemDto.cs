using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoqueFTF.Models
{
    public class ItemDto
    {
        [Required(ErrorMessage = "Por favor, digite um nome válido"), MaxLength(60)]
        public string Nome { get; set; } = "";

        [Required]
        public decimal Preco { get; set; }

        [Required, MaxLength(20)]
        public string Raridade { get; set; } = "";

        [Required]
        public int Quantidade { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
