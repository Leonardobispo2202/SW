using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LojaVirtual.Models
{
    public class compra
    {
        [Key]
        public int Idcompra { get; set; }
        public int ID { get; set; }
        public virtual produto produto { get; set; }
        public int Quantidade { get; set; }
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

    }
}