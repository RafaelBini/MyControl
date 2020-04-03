using Microsoft.Win32;
using MyControl.dao;
using MyControl.model;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Interação lógica para BancosPage.xam
    /// </summary>
    public partial class BancosPage : Page
    {
        public BancosPage()
        {
            InitializeComponent();
            AtualizarGrid();

        }

        private void AtualizarGrid()
        {
            // Reseta grid
            gBancos.Children.Clear();
            gBancos.RowDefinitions.Clear();
            gBancos.ColumnDefinitions.Clear();

            // Desenha os botões
            int c=0, l = 0, i = 0;
            foreach(Banco banco in BancoDAO.getBancos())
            {
                // Cria imagem                
                Image img = new Image();
                img.Source = banco.GetImagem();

                // Cria o botão
                Button btn = new Button();
                btn.Background = Brushes.White;
                btn.BorderBrush = Brushes.White;
                btn.Height = 100;
                btn.Width = 100;
                btn.Padding = new Thickness(0,0,0,0);
                btn.Content = img;
                btn.Click += Btn_Click;
                void Btn_Click(object sender, RoutedEventArgs e)
                {
                    txCodigo.Text = banco.Id;
                    txBcoId2.Text = banco.Id2.ToString();
                    txNome.Text = banco.Nome;
                    txImg.Text = banco.Imagem;
                    cbExtrato.Text = banco.Extrato;
                    txGrupo.Text = banco.Grupo;
                }

                // Se é o primeiro botão a ser criado, seleciona-o
                if (i == 0)
                    Btn_Click(null, null);

                // Se é o ultimo da linha,
                if (i % 4 == 0)
                {
                    // Adiciona uma nova linha
                    RowDefinition linha = new RowDefinition();
                    linha.Height = new GridLength(100, GridUnitType.Star);                    
                    gBancos.RowDefinitions.Add(linha);
                    l++;
                }

                if (c < 4 && l <= 1)
                {
                    // Adiciona uma nova coluna
                    ColumnDefinition coluna = new ColumnDefinition();
                    coluna.Width = new GridLength(100, GridUnitType.Star);
                    gBancos.ColumnDefinitions.Add(coluna);
                    c++;
                }
                else if (c < 4)
                {
                    c++;
                }
                else
                {
                    c = 1;
                }

                // Adiciona botão dentro
                gBancos.Children.Add(btn);
                Grid.SetRow(btn, l-1);
                Grid.SetColumn(btn, c-1);

                // Conta
                i++;
            }           

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.ShowDialog();
            if (ofd.FileName == null)
                return;

            txImg.Text = ofd.FileName;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Banco banco = new Banco(txCodigo.Text,Convert.ToInt32(txBcoId2.Text), txNome.Text,cbExtrato.Text,txImg.Text, txGrupo.Text);
            BancoDAO.setBanco(banco);
            AtualizarGrid();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new HomePage());
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Você tem certeza de que deseja excluir o banco " + txCodigo.Text + "?", "Atenção", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            BancoDAO.deletar(txCodigo.Text);
            AtualizarGrid();
        }
    }
}
