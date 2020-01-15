using Google.Cloud.Firestore;
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
    /// Interação lógica para RotinaPage1_Importar_Extratos.xam
    /// </summary>
    public partial class RotinaPage1_Importar_Extratos : Page
    {
        Banco bancoSelecionado;
        public RotinaPage1_Importar_Extratos()
        {
            InitializeComponent();

            // Seta a pbar
            GlobalVars.mainWindow.setRotinaBarValue(1);

            // Recebe em que parte está a rotina
            int parte = RotinaDAO.getParte();

            // Se não tiver inciado a rotina ainda (parte=0), determina o mês para a rotina que irá iniciar
            if (parte == 0)
                RotinaDAO.setAnoMes();

            // Seta parte da rotina
            RotinaDAO.setParte(1);

            // Atualiza
            lbAnoMes.Content = RotinaDAO.getAnoMes();
            AtualizarDg();
            AtualizaCb();

            dgImportacoes.SelectedIndex = 0;
            cbBcoId.SelectedIndex = 0;
        }

        private void AtualizaCb()
        {
            cbBcoId.ItemsSource = RotinaDAO.getBancosImportados();
            cbBcoId.DisplayMemberPath = "bco_id";

        }

        private void AtualizarDg()
        {
            dgImportacoes.ItemsSource = RotinaDAO.getBancosImportados();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new HomePage());
            GlobalVars.mainWindow.getAnimation("SairRotina").Begin();
        }

        private void BtnProximo_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new RotinaPage2_Alocar_Debitos());
        }

        private void CkConfere_Click(object sender, RoutedEventArgs e)
        {
            if (ckConfere.IsChecked == true)
                btnProximo.IsEnabled = true;
            else
                btnProximo.IsEnabled = false;
        }

        private void CbBcoId_DropDownClosed(object sender, EventArgs e)
        {
            // Se não existe nada selecionado, encerra
            if (cbBcoId.Text == null)
                return;

            // Recebe o banco selecionado
            bancoSelecionado = BancoDAO.getBanco(cbBcoId.Text);

            // Atualiza imagem do botão
            imgBtn.Source = bancoSelecionado.GetImagem();
        }

        private void DgImportacoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se não existe linha selecionada, encerra
            if (dgImportacoes.SelectedItem == null)
                return;

            // Recebe o banco selecionado
            bancoSelecionado = BancoDAO.getBanco((dgImportacoes.SelectedItem as DataRowView).Row.ItemArray[1].ToString());

            // Atualiza o combo
            cbBcoId.Text = bancoSelecionado.Id;

            // Atualiza imagem do botão
            imgBtn.Source = bancoSelecionado.GetImagem();
        }

        private void BtnImportarExtrato_Click(object sender, RoutedEventArgs e)
        {
            // Se não existe banco selecionado, encerra
            if (bancoSelecionado == null)
                return;

            // Verifica se o extrato do banco já foi importado
            if (RotinaDAO.foiImportado(bancoSelecionado))
            {
                // Confirma a substituiçao
                if (MessageBox.Show("Certeza que deseja substituir o extrato já importado?","Atenção - Extrato já importado", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Deleta os registros existentes
                    RotinaDAO.deleteExtrato(bancoSelecionado);

                    // Atualiza a grid
                    AtualizarDg();
                }
                else
                {
                    return;
                }
                    
            }

            // Abre a dialogo para escolha de arquivo
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Extrato "+bancoSelecionado.Id+" |*." + bancoSelecionado.Extrato;
            dialogo.ShowDialog();

            // Se nada foi selecionado, encerra
            if (dialogo.FileName == null || !File.Exists(dialogo.FileName))
                return;

            // Recebe as informações conforme o tipo de extrato
            if (bancoSelecionado.Extrato == "OFX")
            {
                try
                {
                    RotinaDAO.importarOfx(dialogo.FileName, bancoSelecionado, RotinaDAO.getAnoMes());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                    return;
                }
                
            }

            // Atualiza com os dados do FireStore
            AtualizarTransacoesConformeFire();

            // Atualiza datagrid
            AtualizarDg();

        }

        public async void AtualizarTransacoesConformeFire()
        {
            // Instancia a coleção transações
            CollectionReference collectionReference = GlobalVars.db.Collection("transacoes");

            // Determina o máximo de registros que serão consultados
            int batchSize = 500;

            // Consulta todos os documentos (no maximo 500) da coleção transações
            QuerySnapshot snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();

            // Instancia uma lista com esses documentos
            IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;

            // Se a lista não está vazia,
            if (documents.Count > 0)
            {
                // Passa cada transacao
                foreach (DocumentSnapshot document in documents)
                {
                    // Faz update no postgres
                    RotinaDAO.updateTransacaoFire(document);
                }

            }
        }
    }
}
