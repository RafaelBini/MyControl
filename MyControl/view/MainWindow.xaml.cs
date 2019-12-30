using MyControl.dao;
using MyControl.model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MyControl
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Salvar a variavel global main window
            GlobalVars.mainWindow = this;

            // Direciona para pagina inicial
            mFrame.NavigationService.Navigate(new view.HomePage());

        }

        public Storyboard getAnimation(String AnimationKey)
        {
            return this.FindResource(AnimationKey) as Storyboard;
        }

        public void setRotinaBarValue(int parte)
        {
            // Determina a barra de progresso
            if (parte < 1 || parte > 4)
            {
                pbarRotina.Value = 0;
                btnRotina1.Background = pbarRotina.Foreground;
                btnRotina1.Foreground = pbarRotina.Background;
                btnRotina2.Background = pbarRotina.Background;
                btnRotina1.Foreground = pbarRotina.Foreground;
                btnRotina3.Background = pbarRotina.Background;
                btnRotina1.Foreground = pbarRotina.Foreground;
                btnRotina4.Background = pbarRotina.Background;
                btnRotina1.Foreground = pbarRotina.Foreground;
                return;
            }            
            pbarRotina.Value = parte - 1;
            btnRotina1.Background = pbarRotina.Foreground;
            btnRotina1.Foreground = pbarRotina.Background;
            btnRotina2.Background = parte > 1 ? pbarRotina.Foreground : pbarRotina.Background;
            btnRotina2.Foreground = parte > 1 ? pbarRotina.Background : pbarRotina.Foreground;
            btnRotina3.Background = parte > 2 ? pbarRotina.Foreground : pbarRotina.Background;
            btnRotina3.Foreground = parte > 2 ? pbarRotina.Background : pbarRotina.Foreground;
            btnRotina4.Background = parte > 3 ? pbarRotina.Foreground : pbarRotina.Background;
            btnRotina4.Foreground = parte > 3 ? pbarRotina.Background : pbarRotina.Foreground;

            // Determina o label
            lbParte.Content = RotinaDAO.getAnoMes() + " - ";
            if (parte == 2)
                lbParte.Content += "Alocar Débitos";
            else if (parte == 3)
                lbParte.Content += "Alocar Créditos";
            else if (parte == 4)
                lbParte.Content += "Finalizar";
            else
                lbParte.Content += "Importar Extratos";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (GlobalVars.rotinaPage2 != null)
                GlobalVars.rotinaPage2.Grid_KeyDown(sender, e);
        }

        private void BtnRotina1_Click(object sender, RoutedEventArgs e)
        {
            if (RotinaDAO.getParte() >= 1)
                mFrame.NavigationService.Navigate(new view.RotinaPage1_Importar_Extratos());
        }

        private void BtnRotina2_Click(object sender, RoutedEventArgs e)
        {
            if (RotinaDAO.getParte() >= 2)
                mFrame.NavigationService.Navigate(new view.RotinaPage2_Alocar_Debitos());
        }

        private void BtnRotina3_Click(object sender, RoutedEventArgs e)
        {
            if (RotinaDAO.getParte() >= 3)
                mFrame.NavigationService.Navigate(new view.RotinaPage3_Alocar_Creditos());
        }

        private void BtnRotina4_Click(object sender, RoutedEventArgs e)
        {
            if (RotinaDAO.getParte() >= 4)
                mFrame.NavigationService.Navigate(new view.RotinaPage4_Finalizar());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Confirma a ação
            if (MessageBox.Show("Você tem certeza de que deseja DELETAR toda a rotina construida até agora para o mês " + RotinaDAO.getAnoMes() + " ?", "Atenção", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            // Deleta todos os registros da temp
            RotinaDAO.deleteTransacaoTemp();

            // Seta rotina para zero
            RotinaDAO.setParte(0);

            // Direciona para Home Page
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new view.HomePage());
            GlobalVars.mainWindow.getAnimation("SairRotina").Begin();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            // Direciona para Home Page
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new view.HomePage());
            GlobalVars.mainWindow.getAnimation("SairRotina").Begin();
        }
    }
}
