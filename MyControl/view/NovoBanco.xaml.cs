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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Lógica interna para NovoBanco.xaml
    /// </summary>
    public partial class NovoBanco : Window
    {
        BancosPage bancosPage;

        public NovoBanco(BancosPage bancosPage)
        {
            InitializeComponent();
            this.bancosPage = bancosPage;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();
            if (ofd.FileName == null)
                return;

            txImg.Text = ofd.FileName;
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            Banco banco = new Banco(txCodigo.Text, Convert.ToInt32(txBcoId2.Text), txNome.Text, cbExtrato.Text, txImg.Text, txGrupo.Text, true);
            BancoDAO.setBanco(banco);
            bancosPage.AtualizarGrid();
            this.Close();
        }
    }
}
