using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.VO
{
    public class Pecas : BaseVO
    {
        public string descricao { get; set; }
        public string codigoBarras  { get; set; }
        public Veiculo veiculo { get; set; }
    }
}
