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
    /// Lógica interna para DividirDebitoWindow.xaml
    /// </summary>
    public partial class DividirDebitoWindow : Window
    {
        public DividirDebitoWindow()
        {
            InitializeComponent();

            // Foca tx
            txValor.Focus();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Enter)
            {
                // Se não tem linha selecionada, encerra
                if (GlobalVars.rotinaPage2.dgDebitos.SelectedItem == null ||  !Double.TryParse(txValor.Text, out Double a))
                    return;              

                // Insere nova transacao filha e atualiza a transacao mae
                RotinaDAO.dividirTransacao(Convert.ToDouble(txValor.Text), GlobalVars.rotinaPage2.transacaoSelecionada);

                // Atualiza a grid
                GlobalVars.rotinaPage2.AtualizarDgs();

                // Fecha
                this.Close();
            }
        }


        private void Window_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex) { }
        }
    }
}
