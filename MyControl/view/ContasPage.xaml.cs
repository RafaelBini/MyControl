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
    /// Interação lógica para ContasPage.xam
    /// </summary>
    public partial class ContasPage : Page
    {
        public ContasPage()
        {
            InitializeComponent();
            AtualizarDg();
            dgContas.SelectedIndex = 0;
        }

        private void AtualizarCb(string nomeConta, string grupo)
        {
            cbContas.ItemsSource = ContaDAO.getContasExceto(nomeConta, grupo);
            cbContas.DisplayMemberPath = "nome";
            cbContas.SelectedIndex = 0;
        }

        public void AtualizarDg()
        {
            dgContas.ItemsSource = ContaDAO.getSaldoContas();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new HomePage());
        }

        private void DgContas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgContas.SelectedItem == null)
                return;

            string nomeConta = (dgContas.SelectedItem as DataRowView).Row.ItemArray[0].ToString();

            AtualizarInfoConta(nomeConta);
        }

        private void AtualizarInfoConta(string nomeConta)
        {
            // Recebe Infos da Conta
            Conta c = ContaDAO.getConta(nomeConta);

            // Seta as Info
            lbNomeConta.Content = c.nome;
            lbDescrConta.Text = c.descricao;
            lbSaldoConta.Content = "R$ " + c.saldo;
            lbGrupo.Content = c.grupo;

            txTransf.Text = "0.00";

            if (c.ativo)
            {
                btnInativar.Content = "Inativar";
                btnInativar.Background = Brushes.Red;
            }
            else
            {
                btnInativar.Content = "Ativar";
                btnInativar.Background = Brushes.Green;
            }

            AtualizarCb(nomeConta, c.grupo);
        }

        private void BtnTrans_Click(object sender, RoutedEventArgs e)
        {
            // Valida o valor digitado
            if (txTransf.Text.Trim() == "" || Convert.ToDecimal(txTransf.Text) <= 0)
                return;

            // Valida o saldo
            if (Convert.ToDecimal(txTransf.Text.Replace(".",",")) > Convert.ToDecimal(lbSaldoConta.Content.ToString().Replace(".", ",").Replace("R$ ","")))
            {
                MessageBox.Show("Saldo insuficiente.");
                return;
            }

            // Confirma a ação
            if (MessageBox.Show("Você tem certeza de que deseja transferir R$ " + txTransf.Text + " da conta " + lbNomeConta.Content.ToString() + " para a conta " + cbContas.Text + "?", "Atenção", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            // Faz os UPDATES
            TransacaoDAO.Transferir(lbNomeConta.Content.ToString(), cbContas.Text, txTransf.Text.Replace(",","."));

            // Atualiza
            AtualizarInfoConta(lbNomeConta.Content.ToString());
            AtualizarDg();

        }

        private void BtnInativar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica o tipo de botão
            if (btnInativar.Content.ToString() == "Inativar")
            {
                // Valida se a conta está vazia
                if (Convert.ToDecimal(lbSaldoConta.Content.ToString().Replace(".", ",").Replace("R$ ", "")) > 0)
                {
                    MessageBox.Show("Antes de inativar, você deve transfirir o saldo para outra conta.", "Ops!");
                    return;
                }

                // Confirma a ação
                if (MessageBox.Show("Você tem certeza de que deseja inativar a conta " + lbNomeConta.Content.ToString() + "?", "Atenção", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    return;

                // Marca a conta como inativa
                ContaDAO.setStatus(lbNomeConta.Content.ToString(), false);

                // Atualiza
                AtualizarInfoConta(lbNomeConta.Content.ToString());
                AtualizarDg();
            }
            else
            {

            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (new NovaContaWindow(this)).Show();
        }
    }
}
