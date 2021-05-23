using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiModeloDDD.Domain.Entity
{
    public class Cliente : Base
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool isAtivo { get; set; }

    }
}
