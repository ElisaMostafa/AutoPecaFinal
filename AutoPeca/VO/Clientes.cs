using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.VO
{
    public class Clientes : BaseVO
    {
       
        public string nome { get; set; }
        public string CPF { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
    }
}
