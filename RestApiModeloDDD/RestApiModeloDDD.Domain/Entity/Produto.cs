using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiModeloDDD.Domain.Entity
{
    public class Produto : Base
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel { get; set; }
    }
}
