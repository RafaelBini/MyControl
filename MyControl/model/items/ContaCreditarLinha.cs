using MaterialDesignThemes.Wpf;
using MyControl.dao;
using MyControl.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MyControl.model.items
{
    class ContaCreditarLinha : StackPanel
    {
        DataRow contaInfo;
        RotinaPage3_Alocar_Creditos page;
        Brush normalColor = new BrushConverter().ConvertFromString("#B9FFFC") as Brush;
        Brush selectedColor = new BrushConverter().ConvertFromString("#B9D4FF") as Brush;
        Brush hoverColor = new BrushConverter().ConvertFromString("#FFB9BA") as Brush;
        Brush problemColor = new BrushConverter().ConvertFromString("#FFB9B9") as Brush;

        public ContaCreditarLinha(DataRow contaInfo, RotinaPage3_Alocar_Creditos page)
        {
            this.contaInfo = contaInfo;
            this.page = page;
            // Define as propriedades da linha
            this.Height = 60;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            if (RotinaDAO.isContaDeBoa(contaInfo["nome"].ToString()))
                this.Background = normalColor;
            else
                this.Background = problemColor; 
            this.Orientation = Orientation.Horizontal;
            this.Cursor = Cursors.Hand;
            this.MouseEnter += ContaCreditarLinha_MouseEnter;
            this.MouseLeave += ContaCreditarLinha_MouseLeave;
            this.MouseLeftButtonDown += ContaCreditarLinha_MouseLeftButtonDown;
            
            // Adiciona as celulas
            foreach(object campo in contaInfo.ItemArray)
            {
                this.Children.Add(new ContaCreditarCell(campo, 300));
            }
            
        }

        private void ContaCreditarLinha_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Nova Seleção
            if (page.contaClicadaInfo != contaInfo)
            {
                // Limpa geral
                foreach(ContaCreditarLinha lin in page.spContas.Children)
                {
                    if(lin.Background.ToString() == selectedColor.ToString())
                    {
                        if (RotinaDAO.isContaDeBoa((lin.Children[0] as ContaCreditarCell).Content.ToString()))
                            lin.Background = normalColor;
                        else
                            lin.Background = problemColor;
                    }
                }

                // Recebe as info
                page.contaClicadaInfo = contaInfo;

                // Muda back
                this.Background = selectedColor;
            }
            // Deseleção
            else
            {
                // Zera as info
                page.contaClicadaInfo = null;

                // Muda back
                if (RotinaDAO.isContaDeBoa(contaInfo["nome"].ToString()))
                    this.Background = normalColor;
                else
                    this.Background = problemColor;
            }

            // Limpa o txt
            page.txValor.Text = "";
            
        }

        public void PreencherDadosConta(DataRow contaEscolhida, bool zeraValor = true)
        {
            // Foca valor
            if (zeraValor) page.txValor.Text = "";
            page.txValor.Focus();

            // Recebe conta
            string conta = contaEscolhida["nome"].ToString();

            // Atualiza estatisticas
            page.lbNomeConta.Content = conta;            
            page.lbSaldoConta.Content = RotinaDAO.getSaldoConta(conta);
            page.lbMediaGastos.Content = RotinaDAO.getMediaGastosConta(conta);
            page.lbMediaCreditos.Content = RotinaDAO.getMediaCreditosConta(conta);            
            page.lbRecomendado.Content = getRecomendado(Convert.ToDouble(page.lbSaldoConta.Content), Convert.ToDouble(page.lbMediaGastos.Content)).ToString();
        }

        public double getRecomendado(double saldo, double gastos)
        {
            double recomendado = 0;

            if (saldo < 0)
                recomendado = Math.Abs(Convert.ToDouble(saldo)) + Math.Abs(Convert.ToDouble(gastos));
            else if (saldo < gastos)
                recomendado = Math.Abs(Convert.ToDouble(gastos)) - Math.Abs(Convert.ToDouble(saldo));

            return Math.Round(recomendado, 2);
        }

        private void ContaCreditarLinha_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Opacity = 1;

            // Se não tem nada selecionado
            if (page.contaClicadaInfo != null)
            {
                

                PreencherDadosConta(page.contaClicadaInfo, false);
            }
        }

        private void ContaCreditarLinha_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Opacity = 0.75;

            if (page.contaClicadaInfo == null)
            {                
                PreencherDadosConta(contaInfo);
            }
        }

    }

    class ContaCreditarCell : Label
    {
        public ContaCreditarCell(object conteudo, double largura)
        {
            this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.Width = largura;
            this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            this.FontSize = 22;
            this.Content = conteudo;
        }
    }
}
