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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyControl.view
{
    /// <summary>
    /// Interação lógica para RotinaPage4_Finalizar.xam
    /// </summary>
    public partial class RotinaPage4_Finalizar : Page
    {
        public RotinaPage4_Finalizar()
        {
            InitializeComponent();
            GlobalVars.mainWindow.setRotinaBarValue(4);
        }
    }
}
