using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyControl.model
{
    public class Banco
    {
        public String Id;
        public int Id2;
        public String Nome;
        public String Extrato;
        public String Imagem;
        public String Grupo;

        public Banco(string bco_id, int bco_id2, string bco_nome, string bco_extrato, string bco_img_src, string grupo)
        {
            this.Id = bco_id;
            this.Id2 = bco_id2;
            this.Nome = bco_nome;
            this.Extrato = bco_extrato;
            this.Imagem = bco_img_src;
            this.Grupo = grupo;
        }

        public BitmapImage GetImagem()
        {
            if (File.Exists(Imagem))
                return new BitmapImage(new Uri(Imagem));
            else
                return new BitmapImage(new Uri("pack://application:,,,/MyControl;component/resources/bank.png"));
                        
        }
    }
}
