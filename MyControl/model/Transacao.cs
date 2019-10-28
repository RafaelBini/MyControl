using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.model
{
    public class Transacao
    {
        public DateTime datapgto;
        public String tipo;
        public Double valor;
        public String dctobco;
        public String descricaobco2;
        public String bco;
        public String conta;
        public int CodConta;
        public String descricao;


        public bool hasAllValues()
        {
            return datapgto != null && tipo != null && valor != 0 && dctobco != null && descricaobco2 != null && bco != null;
        }
    }
}
