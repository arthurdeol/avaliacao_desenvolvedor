using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Models
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        public decimal Price { get; set; }
        public string Purchaser { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string Address { get; set; }
        public string Provider { get; set; }
    }
}
