using MyControl.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControl.dao
{
    class BancoDAO
    {
        public static List<Banco> getBancos()
        {
            List<Banco> bancos = new List<Banco>();
            DataTable dtBancos = SqlTool.Consultar("SELECT bco_id, bco_id2, bco_nome, bco_extrato, bco_img_src, grupo from banco order by 1, 2");
            foreach (DataRow linha in dtBancos.Rows)
            {
                bancos.Add(new Banco(linha["bco_id"].ToString(), Convert.ToInt32(linha["bco_id2"]), linha["bco_nome"].ToString(), linha["bco_extrato"].ToString(), linha["bco_img_src"].ToString(), linha["grupo"].ToString()));
            }
            return bancos;
        }

        public static int setBanco(Banco bco)
        {
            if(existe(bco.Id))
                return SqlTool.Executar("UPDATE banco SET bco_nome='" + bco.Nome + "',bco_id2='"+bco.Id2+"' bco_extrato='" + bco.Extrato + "', bco_img_src='" + bco.Imagem + "', grupo='"+bco.Grupo+"' WHERE bco_id='" + bco.Id+"'");
            else
                return SqlTool.Executar("INSERT INTO banco (bco_id, bco_id2, bco_nome, bco_extrato, bco_img_src, grupo) VALUES ('" + bco.Id + "', '"+bco.Id2+"', '" + bco.Nome + "', '" + bco.Extrato + "', '" + bco.Imagem + "', '" + bco.Grupo + "')");
        }

        public static bool existe(string bco_id)
        {
            return SqlTool.Consultar("select bco_id from banco where bco_id ='"+bco_id+"'").Rows.Count > 0;
        }

        public static DataView getSaldos()
        {

            return SqlTool.Consultar("select bco_id as banco, saldo from saldo_bco_view").DefaultView;
        }

        public static Banco getBanco(string bco_id)
        {
            DataRow dr = SqlTool.Consultar("select bco_id, bco_id2, bco_nome, bco_extrato, bco_img_src, grupo from banco where bco_id='" + bco_id+"'").Rows[0];
            return new Banco(dr["bco_id"].ToString(), Convert.ToInt32(dr["bco_id2"]), dr["bco_nome"].ToString(), dr["bco_extrato"].ToString(), dr["bco_img_src"].ToString(), dr["grupo"].ToString());
        }

        public static int deletar(string bco_id)
        {
            return SqlTool.Executar("DELETE FROM banco WHERE bco_id = '"+bco_id+"'");
        }

    }
}
