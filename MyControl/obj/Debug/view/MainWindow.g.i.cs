﻿#pragma checksum "..\..\..\view\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "22539BFBBAEED8B16B484ECDF56573578D8A6ED2073A947F5188B6B15CC17B4A"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using MyControl;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MyControl {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame mFrame;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gBar;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pbarRotina;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina1;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina2;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina3;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina4;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyControl;component/view/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\view\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\view\MainWindow.xaml"
            ((MyControl.MainWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 3:
            this.gBar = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.pbarRotina = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 5:
            this.btnRotina1 = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\view\MainWindow.xaml"
            this.btnRotina1.Click += new System.Windows.RoutedEventHandler(this.BtnRotina1_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnRotina2 = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\view\MainWindow.xaml"
            this.btnRotina2.Click += new System.Windows.RoutedEventHandler(this.BtnRotina2_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnRotina3 = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\view\MainWindow.xaml"
            this.btnRotina3.Click += new System.Windows.RoutedEventHandler(this.BtnRotina3_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnRotina4 = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\view\MainWindow.xaml"
            this.btnRotina4.Click += new System.Windows.RoutedEventHandler(this.BtnRotina4_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

