using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LojaVirtual.Models
{
    public class produto
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        [Display(Name = "Código da Promoção")]
        public int TipoPromocao { get; set; }
        public virtual promocao promocao { get; set; }
        public virtual ICollection<compra> compra { get; set; }

    }
}