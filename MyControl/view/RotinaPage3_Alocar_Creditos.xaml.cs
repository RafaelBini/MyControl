using MyControl.dao;
using MyControl.model;
using MyControl.model.items;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Interação lógica para RotinaPage3_Alocar_Creditos.xam
    /// </summary>
    public partial class RotinaPage3_Alocar_Creditos : Page
    {
        public DataRow contaClicadaInfo;

        public RotinaPage3_Alocar_Creditos()
        {
            InitializeComponent();
            GlobalVars.mainWindow.setRotinaBarValue(3);
            RotinaDAO.setParte(3);
            AtualizarStackPanel();
            AtualizarTotal();
            txValor.Focus();
        }

        public void AtualizarStackPanel()
        {
            DataTable dtContas = RotinaDAO.getCreditosDistribuir();
            spContas.Children.Clear();
            foreach (DataRow l in dtContas.Rows)
            {
                ContaCreditarLinha linha = new ContaCreditarLinha(l,this);
                spContas.Children.Add(linha);
            }
        }

        private void AtualizarTotal()
        {
            // COloca o novo total
            lbTotal.Content = RotinaDAO.getCreditosTotal();

            // Valida se pode liberar o botão
            if (Convert.ToDecimal(lbTotal.Content.ToString() == "" ? "0" : lbTotal.Content) == 0)
                btnNext.IsEnabled = true;
            else
                btnNext.IsEnabled = false;
        }

        private void TxValor_TextChanged(object sender, TextChangedEventArgs e)
        {           
            // Verifica se é numero
            if (!double.TryParse(txValor.Text, out double a) && txValor.Text != "")
            {                
                foreach(TextChange tc in e.Changes)
                {
                    txValor.Text = txValor.Text.Replace(txValor.Text.Substring(tc.Offset, 1), "");
                    txValor.CaretIndex = tc.Offset;
                }
            }

            /*
            // Atualiza o total geral
            try { lbTotal.Content = Math.Round(Convert.ToDouble(RotinaDAO.getCreditosTotal()) - Convert.ToDouble(txValor.Text.Replace(".", ",")), 2);    }
            catch { }

            // Atualiza o saldo da conta
            try { lbSaldoConta.Content = Math.Round(Convert.ToDouble(RotinaDAO.getSaldoConta(lbNomeConta.Content.ToString())) + Convert.ToDouble(txValor.Text.Replace(".", ",")), 2); }
            catch { }
            */

        }



        private void DgCreditos_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Verifica se a linha está de boa ou não
            if (!RotinaDAO.isContaDeBoa((e.Row.DataContext as DataRowView).Row.ItemArray[0].ToString()))
            {
                e.Row.Background = Brushes.Red;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                // Recebe a conta e o valor
                string conta = lbNomeConta.Content.ToString();
                string valor = txValor.Text.Replace(".", ",");
                string disponivel = lbTotal.Content.ToString() == "" ? "0" : lbTotal.Content.ToString();

                // Valida o valor
                if (Convert.ToDouble(valor) > Convert.ToDouble(disponivel))
                {
                    MessageBox.Show("Saldo insuficiente!");
                    return;
                }
                    

                // Insere uma nova transacao temporaria para inserir valor na conta
                RotinaDAO.insertCredito(conta, valor);

                // Atualiza
                AtualizarTotal();
                AtualizarStackPanel();

                // Limpa o txt
                txValor.Text = "";
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return;
            }

        }

        private void ColocarRecomendado(object sender, MouseButtonEventArgs e)
        {
            txValor.Text = lbRecomendado.Content.ToString().Replace(".","").Replace(",",".");
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Recebe a conta e o valor
                string conta = lbNomeConta.Content.ToString();
                string valor = txValor.Text;

                // Deleta as transacaos temporarias
                RotinaDAO.deleteCredito(conta, valor);

                // Atualiza
                AtualizarTotal();
                AtualizarStackPanel();

                // Limpa o txt
                txValor.Text = "";

            }
            catch
            {
                return;
            }
        }

        private void LbTotal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txValor.Text = lbTotal.Content.ToString().Replace(".", "").Replace(",", ".");
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // Confere novamente se o saldo está zerado
            if (Convert.ToDecimal(lbTotal.Content.ToString() == "" ? "0" : lbTotal.Content) != 0)
                return;

            // Direciona para a próxima página
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new RotinaPage4_Finalizar());
        }
    }
}
