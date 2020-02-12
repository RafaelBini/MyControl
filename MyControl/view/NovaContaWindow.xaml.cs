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
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Lógica interna para NovaContaWindow.xaml
    /// </summary>
    public partial class NovaContaWindow : Window
    {
        private ContasPage page;

        public NovaContaWindow(ContasPage page)
        {
            InitializeComponent();
            txNome.Focus();
            this.page = page;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Valida se o nome já existe
            if (ContaDAO.getConta(txNome.Text) != null) { 
                MessageBox.Show("Esse nome de conta já está sendo usado!");
                return;
            }

            // Valida se a prioridade é um numero entre 1 e 100
            int p;
            if (!int.TryParse(txPrioridade.Text, out p))
            {
                MessageBox.Show("Digite um número de 1 a 100 na prioridade");
                return;
            }
            if (p <1 || p > 100)
            {
                MessageBox.Show("Digite um número de 1 a 100 na prioridade");
                return;
            }

            // Efetua o insert
            Conta c = new Conta();
            c.nome = txNome.Text;
            c.descricao = txDescricao.Text;
            c.prioridade = p;
            c.notificar = Convert.ToBoolean(ckNotificar.IsChecked);
            c.grupo = txGrupo.Text;
            ContaDAO.insertConta(c);

            // Atualiza a página
            page.AtualizarDg();

            // Fecha a janela
            this.Close();
        }


    }
}
