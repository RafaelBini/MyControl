using Google.Cloud.Firestore;
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
                // Instancia o valor_filho e o valor_mae
                double valor_filho;
                double valor_mae;
                string valor_mae_str = (GlobalVars.rotinaPage2.dgDebitos.SelectedItem as DataRowView).Row.ItemArray[2].ToString().Replace(".", ",").Replace("-","");

                // Se não tem linha selecionada, encerra (aqui também se atribui os valores mãe e filho)
                if (GlobalVars.rotinaPage2.dgDebitos.SelectedItem == null ||  !Double.TryParse(txValor.Text.Replace(".",","), out valor_filho) || !Double.TryParse(valor_mae_str, out valor_mae))
                    return;

                // Calcula o novo valor mãe
                valor_mae = valor_mae - valor_filho;

                // Insere nova transacao filha e atualiza a transacao mae
                RotinaDAO.dividirTransacao(txValor.Text, GlobalVars.rotinaPage2.transacaoSelecionada);

                // Atualiza as transações conforme o firestore e encerra pela nova thread gerada
                AtualizarTransacoesConformeFire(valor_filho, valor_mae);

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

        public async void AtualizarTransacoesConformeFire(double valor_filho, double valor_mae)
        {
            // Instancia a coleção transações
            CollectionReference collectionReference = GlobalVars.db.Collection("transacoes");

            // Determina o máximo de registros que serão consultados
            int batchSize = 500;

            // Consulta todos os documentos (no maximo 500) da coleção transações
            QuerySnapshot snapshot = await collectionReference.WhereIn("valor",new double[] { valor_filho, valor_mae }).Limit(batchSize).GetSnapshotAsync();

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

            // Atualiza a grid
            GlobalVars.rotinaPage2.AtualizarDgs();

            // Fecha
            this.Close();
        }
    }
}
