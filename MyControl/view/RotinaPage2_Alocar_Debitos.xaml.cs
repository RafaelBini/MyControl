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
using System.Windows.Controls.Primitives;
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
    /// Interação lógica para RotinaPage2_Alocar_Debitos.xam
    /// </summary>
    /// 
    // Classe necessária para  editar o valor de uma celula no datagrid
    public static class suplementos
    {
        // Add functions
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
        public static DataGridCell GetCell(this DataGrid grid, int row, int column)
        {
            DataGridRow rowContainer = grid.GetRow(row);
            return grid.GetCell(rowContainer, column);
        }
        // Add
    }

    public partial class RotinaPage2_Alocar_Debitos : Page
    {
        private bool pintandoContas = false;
        public int transacaoSelecionada;

        public RotinaPage2_Alocar_Debitos()
        {
            InitializeComponent();
            GlobalVars.mainWindow.setRotinaBarValue(2);
            GlobalVars.rotinaPage2 = this;
            RotinaDAO.setParte(2);
            AtualizarDgs();
        }

        public void AtualizarDgs()
        {
            dgSaldoSoma.ItemsSource = RotinaDAO.getSaldoDebitoContas();
            dgDebitos.ItemsSource = RotinaDAO.getDebitosTemp();
            VerificarCanGo();
        }

        public void VerificarCanGo()
        {
            if (RotinaDAO.debitosCanGo())
                btnNext.IsEnabled = true;
            else
                btnNext.IsEnabled = false;
        }

        private void DgSaldoSoma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se nenhuma conta foi selecionada
            if (dgSaldoSoma.SelectedItem == null)
            {
                // Vai para o modo default
                pintandoContas = false;
                dgDebitos.Cursor = Cursors.Arrow;
                dgDebitos.IsReadOnly = false;
            }
            // Se uma conta foi selecionada    
            else
            {
                // Vai para o modo "Pintar"
                pintandoContas = true;
                dgDebitos.Cursor = Cursors.Cross;
                dgDebitos.IsReadOnly = true;

            }
                
        }

        private void DgDebitos_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if(e.Column.Header.ToString().ToUpper() != "DESCRICAO" && e.Column.Header.ToString().ToUpper() != "DESCRIÇÃO")
            {
                e.Cancel = true;
                return;
            }
            
        }

        private void DgDebitos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataRowView cell = e.Row.Item as DataRowView;
            if (cell == null || e.EditAction == DataGridEditAction.Cancel)
            {
                e.Cancel = true;
                return;
            }

            string nova_d = (e.EditingElement as TextBox).Text;
            int transacaoSelecionada = Convert.ToInt32((dgDebitos.SelectedItem as DataRowView).Row.ItemArray[7].ToString());

            // Atualiza os valores no banco de dados
            RotinaDAO.updateDescricaoTransTemp(nova_d, transacaoSelecionada);

            // Verifica se pode ir
            VerificarCanGo();
        }

        private void DgDebitos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se não tem ninguem selecionado, encerra
            if (dgDebitos.SelectedItem == null)
                return;

            // Recebe as info da seleção
            transacaoSelecionada = Convert.ToInt32((dgDebitos.SelectedItem as DataRowView).Row.ItemArray[7].ToString());

            // Se não está pintando, encerra
            if (dgSaldoSoma.SelectedItem==null || !pintandoContas)
                return;

            // Guarda a linha selecionada no saldoSoma
            int indSelecionado = dgSaldoSoma.SelectedIndex;

            // Pega a nova conta          
            string nova_c = (dgSaldoSoma.SelectedItem as DataRowView).Row.ItemArray[0].ToString();

            try
            {
                // UPDATE no banco de daos  
                RotinaDAO.updateContaTransTemp(nova_c, transacaoSelecionada);

                // Atualiza a grid de forma offline
                dgDebitos.GetCell(dgDebitos.SelectedIndex, 0).Content = nova_c;

                // Atualiza as DGs
                AtualizarDgs();

                // Seleciona de novo no dgSadoSoma
                dgSaldoSoma.SelectedIndex = indSelecionado;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

            // Verifica can go
            VerificarCanGo();
        }

        public void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Esc pressionado -- Cancela pintura de conta
            if (e.Key == Key.Escape)
            {
                pintandoContas = false;
                dgSaldoSoma.SelectedItem = null;
            }

            // Shift pressionado -- Advinha a conta
            else if (e.Key == Key.LeftShift)
            {
                // Se não tem nenhuma linha selecionada, cai fora
                if (dgDebitos.SelectedItem == null)
                    return;

                // Recebe as descrições atuais
                DataRowView linha = dgDebitos.SelectedItem as DataRowView;
                string descricao = linha.Row.ItemArray[1].ToString() + " " + linha.Row.ItemArray[3].ToString() + " " + linha.Row.ItemArray[4].ToString();
                string valor = linha.Row.ItemArray[2].ToString();
                int transacaoSelecionada = Convert.ToInt32((dgDebitos.SelectedItem as DataRowView).Row.ItemArray[7].ToString());

                // Recebe a adivinhacao
                String nova_conta = RotinaDAO.adivinharConta(descricao, Convert.ToDouble(valor));

                try
                {
                    // Atualiza de forma online
                    RotinaDAO.updateContaTransTemp(nova_conta, transacaoSelecionada);

                    // Atualiza a grid de forma offline
                    dgDebitos.GetCell(dgDebitos.SelectedIndex, 0).Content = nova_conta;

                    // Atualiza as DGs
                    AtualizarDgs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao tentar atualzar o banco de dados: " + ex.Message);
                }
            }

            // Ctrl pressionado -- Advinha descrição
            else if (e.Key == Key.LeftCtrl)
            {
                // Se não tem nenhuma linha selecionada, cai fora
                if (dgDebitos.SelectedItem == null)
                    return;

                // Recebe as descrições atuais
                DataRowView linha = dgDebitos.SelectedItem as DataRowView;
                string descricao = linha.Row.ItemArray[1].ToString() + " " + linha.Row.ItemArray[3].ToString() + " " + linha.Row.ItemArray[4].ToString();
                string valor = linha.Row.ItemArray[2].ToString();
                int transacaoSelecionada = Convert.ToInt32((dgDebitos.SelectedItem as DataRowView).Row.ItemArray[7].ToString());

                // Recebe a adivinhacao
                String nova_descricao = RotinaDAO.adivinharDescricao(descricao, Convert.ToDouble(valor));

                try
                {
                    // Atualiza de forma online
                    RotinaDAO.updateDescricaoTransTemp(nova_descricao, transacaoSelecionada);

                    // Atualiza a grid de forma offline
                    dgDebitos.GetCell(dgDebitos.SelectedIndex, 1).Content = nova_descricao;

                    // Atualiza as DGs
                    AtualizarDgs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao tentar atualzar o banco de dados: "+ex.Message);
                }
                
            }
        }

        private void DgDebitos_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point mousePoint = System.Windows.Forms.Control.MousePosition;
            
            DividirDebitoWindow ddw = new DividirDebitoWindow();
            ddw.Left = Mouse.GetPosition(this).X;
            ddw.Top = Mouse.GetPosition(this).Y + ddw.Height + 10;
            ddw.Show();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            #region Verifica as contas obrigatórias

            // Recebe as contas obrigatórias que não estão na transacaoTemp
            DataTable dtCO = ContaDAO.getObrigatoriasPendente();

            // Verifica se existem obrigatorias não encontradas
            if (dtCO.Rows.Count >= 1)
            {
                // Monta uma string com todas as contas
                string naoEncontradas = "";
                foreach (DataRow linha in dtCO.Rows)
                {
                    naoEncontradas += linha[0] + "\n";
                }

                // Mostra mensagem
                if (MessageBox.Show("As seguintes contas obrigatórias não foram encontradas:\n\n"+naoEncontradas+"\nTem certeza de que deseja continuar?","Atenção",MessageBoxButton.YesNo,MessageBoxImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            #endregion
            GlobalVars.mainWindow.mFrame.NavigationService.Navigate(new RotinaPage3_Alocar_Creditos());
        }
    }
}
