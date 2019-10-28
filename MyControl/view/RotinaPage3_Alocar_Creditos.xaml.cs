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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Interação lógica para RotinaPage3_Alocar_Creditos.xam
    /// </summary>
    public partial class RotinaPage3_Alocar_Creditos : Page
    {
        public RotinaPage3_Alocar_Creditos()
        {
            InitializeComponent();
            GlobalVars.mainWindow.setRotinaBarValue(3);
            RotinaDAO.setParte(3);
            AtualizarDg();
            AtualizarTotal();
            txValor.Focus();
        }

        private void AtualizarTotal()
        {
            lbTotal.Content = RotinaDAO.getCreditosTotal();
        }

        private void AtualizarDg()
        {
            dgCreditos.ItemsSource = RotinaDAO.getCreditosDistribuir();
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

            // Atualiza o total geral
            try { lbTotal.Content = Convert.ToDouble(RotinaDAO.getCreditosTotal()) - Convert.ToDouble(txValor.Text.Replace(".", ","));    }
            catch { }

            // Atualiza o saldo da conta
            try { lbSaldoConta.Content = Convert.ToDouble(RotinaDAO.getSaldoConta(lbNomeConta.Content.ToString())) + Convert.ToDouble(txValor.Text.Replace(".", ",")); }
            catch { }

        }

        private void DgCreditos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se não tem linha selecionada, vaza
            if (dgCreditos.SelectedItem == null)
                return;

            // Foca valor
            txValor.Text = "";
            txValor.Focus();

            // Recebe a conta
            string conta = ((DataRowView)dgCreditos.SelectedItem as DataRowView).Row.ItemArray[0].ToString();

            // Atualiza estatisticas
            lbNomeConta.Content = conta;
            lbSaldoConta.Content = RotinaDAO.getSaldoConta(conta);
            lbMediaGastos.Content = RotinaDAO.getMediaGastosConta(conta);
            lbMediaCreditos.Content = RotinaDAO.getMediaCreditosConta(conta);

        }

        private void DgCreditos_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Verifica se a linha está de boa ou não
            if (!RotinaDAO.isContaDeBoa((e.Row.DataContext as DataRowView).Row.ItemArray[0].ToString()))
            {
                e.Row.Background = Brushes.Red;
            }
        }
    }
}
