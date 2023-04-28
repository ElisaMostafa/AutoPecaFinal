using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.VO
{
    public class Pedidos : BaseVO
    {
        public DateTime dataPedido { get; set; }
        public Pecas pecas { get; set; }
        public Clientes clientes { get; set; }
    }
}
