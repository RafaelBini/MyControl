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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Interação lógica para HomePage.xam
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            AtualizarDgRotinas();
            AtualizarDgSaldoBancos();
            AtualizaMiniEstatistica();
            AtualizaBotaoGo();
            
        }

        private void AtualizaBotaoGo()
        {
            if(RotinaDAO.getParte() == 0)
            {
                btnGo.Content = "INICIAR ROTINA";

            }
            else
            {
                btnGo.Content = "CONTINUAR ROTINA";
            }
        }



        private void AtualizaMiniEstatistica()
        {
            lbValorEstatistica.Content = EstatisticaDAO.getMesesSobrevivencia().ToString("0.0") + " meses";
        }

        private void AtualizarDgRotinas()
        {
            dgRotinas.ItemsSource = RotinaDAO.getRotinasView().DefaultView;
        }

        private void AtualizarDgSaldoBancos()
        {
            dgSaldos.ItemsSource = BancoDAO.getSaldos();
        }

        private void BtnBancos_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new BancosPage());
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            // Recebe em que parte está a rotina
            int parte = RotinaDAO.getParte();

            // Direciona para a pagina da parte e inicia animação
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(getRotinaPage(parte));            
            GlobalVars.mainWindow.getAnimation("IniciarRotina").Begin();              

        }

        private Page getRotinaPage(int parte=1)
        {
            switch(parte)
            {
                case 1:                    
                    return new RotinaPage1_Importar_Extratos();
                case 2:
                    return new RotinaPage2_Alocar_Debitos();
                case 3:
                    return new RotinaPage3_Alocar_Creditos();
                case 4:
                    return new RotinaPage4_Finalizar();
                default:
                    return new RotinaPage1_Importar_Extratos();
            }
        }

        private void BtnContas_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new ContasPage());
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            new RotinaPage4_Finalizar().inserirFirebase();
        }
    }
}
