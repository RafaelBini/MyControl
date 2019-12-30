using MyControl.dao;
using MyControl.model;
using System;
using System.Collections.Generic;
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
    /// Interação lógica para RotinaPage4_Finalizar.xam
    /// </summary>
    public partial class RotinaPage4_Finalizar : Page
    {
        public RotinaPage4_Finalizar()
        {
            InitializeComponent();
            GlobalVars.mainWindow.setRotinaBarValue(4);
            RotinaDAO.setParte(4);
            AtualizaDgs();
        }

        private void AtualizaDgs()
        {
            dgSaldoBcos.ItemsSource = RotinaDAO.getSaldoBancos();
            dgSaldoContas.ItemsSource = RotinaDAO.getSaldoContas();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            // Libera ou desabilita o botão
            if (ckBancos.IsChecked == true && ckContas.IsChecked == true)
                btnFinalizar.IsEnabled = true;
            else
                btnFinalizar.IsEnabled = false;
        }

        private void CkContas_Click(object sender, RoutedEventArgs e)
        {
            // Libera ou desabilita o botão
            if (ckBancos.IsChecked == true && ckContas.IsChecked == true)
                btnFinalizar.IsEnabled = true;
            else
                btnFinalizar.IsEnabled = false;
        }

        private void BtnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            // Insere as transações reais
            RotinaDAO.insertTransacoesReais();

            // Insere Creditos reais
            RotinaDAO.insertCreditosReais();

            // Deleta o temporário
            RotinaDAO.deleteTransacaoTemp();

            // Seta rotina zerada
            RotinaDAO.setParte(0);

            // Insere os saldos no firebase


            // Direciona para Home Page
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new HomePage());
            GlobalVars.mainWindow.getAnimation("SairRotina").Begin();
        }
    }
}
