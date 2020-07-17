using MyControl.dao;
using MyControl.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Lógica interna para InserirManualmente.xaml
    /// </summary>
    public partial class InserirManualmente : Window
    {
        public DataTable Transacoes;
        public Banco bancoSelecionado;
        public RotinaPage1_Importar_Extratos paginaMae;

        public InserirManualmente(Banco banco, RotinaPage1_Importar_Extratos paginaMae)
        {
            InitializeComponent();
            bancoSelecionado = banco;
            this.lbBancoEmQuestao.Content = bancoSelecionado.Nome;
            this.cbTipo.SelectedIndex = 0;
            this.paginaMae = paginaMae;
            Transacoes = new DataTable();
            Transacoes.Columns.Add("bco", typeof(String));
            Transacoes.Columns.Add("descricaobco2", typeof(String));
            Transacoes.Columns.Add("valor", typeof(Double));
            Transacoes.Columns.Add("datapgto", typeof(String));
            Transacoes.Columns.Add("tipo", typeof(String));
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            this.txValor.Text = this.txValor.Text.Replace(".", ",");

            if(this.txValor.Text == "")
            {
                MessageBox.Show("Digite uma descrição");
                return;
            }
            else if(this.txValor.Text == "")
            {
                MessageBox.Show("Digite um valor");
                return;
            }
            else if (!Double.TryParse(this.txValor.Text, out double a2))
            {
                MessageBox.Show("Digite um valor valido (Ex.: 10.25)");
                return;
            }
            else if (!DateTime.TryParse(this.dtpData.SelectedDate.ToString(), out DateTime a1))
            {
                MessageBox.Show("Digite uma data valida");
                return;
            }

            object[] novaTransacao =
            {
                this.bancoSelecionado.Id,
                this.txDescricao.Text,
                Double.Parse(this.txValor.Text),
                Convert.ToDateTime(this.dtpData.SelectedDate).ToShortDateString(),
                this.cbTipo.Text == "Débito" ? "D" : "C"
            };


            Transacoes.Rows.Add(novaTransacao);
            this.dgTransacoes.ItemsSource = this.Transacoes.DefaultView;
            this.txValor.Text = "";
            
        }

        private void DgTransacoes_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Delete 
                && this.dgTransacoes.SelectedCells != null 
                && MessageBox.Show("Certeza que deseja deletar?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                for (int i = 0; i< this.dgTransacoes.SelectedItems.Count; i++)
                {
                    DataRowView transacao = this.dgTransacoes.SelectedItems[i] as DataRowView;
                    this.Transacoes.Rows.Remove(transacao.Row);
                    i--;

                }
            }
            
        }

        private void BtnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.Transacoes.Rows.Count; i++)
            {
                object[] transacao = this.Transacoes.Rows[i].ItemArray;
                RotinaDAO.insertTransacaoTemp(transacao[0].ToString(), transacao[1].ToString(), Convert.ToDouble(transacao[2]), transacao[3].ToString(), transacao[4].ToString());
            }

            this.paginaMae.AtualizarDg();
            this.Close();
;        }
    }


}
