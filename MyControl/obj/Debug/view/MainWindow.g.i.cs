﻿#pragma checksum "..\..\..\view\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AD5AAB81AFA2371DA148C9906C59DE212AB345C206A420372F49851C15DB2503"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
        
        
        #line 38 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame mFrame;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gBar;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbParte;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHome;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pbarRotina;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina1;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina2;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\view\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotina3;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\view\MainWindow.xaml"
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
            
            #line 10 "..\..\..\view\MainWindow.xaml"
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
            this.lbParte = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnHome = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\view\MainWindow.xaml"
            this.btnHome.Click += new System.Windows.RoutedEventHandler(this.BtnHome_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\view\MainWindow.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.pbarRotina = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 8:
            this.btnRotina1 = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\view\MainWindow.xaml"
            this.btnRotina1.Click += new System.Windows.RoutedEventHandler(this.BtnRotina1_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnRotina2 = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\view\MainWindow.xaml"
            this.btnRotina2.Click += new System.Windows.RoutedEventHandler(this.BtnRotina2_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnRotina3 = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\view\MainWindow.xaml"
            this.btnRotina3.Click += new System.Windows.RoutedEventHandler(this.BtnRotina3_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnRotina4 = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\view\MainWindow.xaml"
            this.btnRotina4.Click += new System.Windows.RoutedEventHandler(this.BtnRotina4_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

