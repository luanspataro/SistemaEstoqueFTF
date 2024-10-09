using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace SistemaEstoqueFTF.Models
{
    public class Item
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = "";

        [Precision(16, 2)]
        public decimal Preco { get; set; }

        [MaxLength(20)]
        public string Raridade { get; set; } = "";

        [MaxLength(4)]
        public int Quantidade { get; set; }

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; }


    }
}
