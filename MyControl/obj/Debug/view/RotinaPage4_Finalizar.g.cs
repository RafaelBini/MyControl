﻿#pragma checksum "..\..\..\view\RotinaPage4_Finalizar.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B103704C51B7150D193ED714B24EE5749666BCB13B71E575B456CF22F65B6FF7"
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
using MyControl.view;
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


namespace MyControl.view {
    
    
    /// <summary>
    /// RotinaPage4_Finalizar
    /// </summary>
    public partial class RotinaPage4_Finalizar : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSaldoBcos;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSaldoContas;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinalizar;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ckContas;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ckBancos;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\view\RotinaPage4_Finalizar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbTotal;
        
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
            System.Uri resourceLocater = new System.Uri("/MyControl;component/view/rotinapage4_finalizar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\view\RotinaPage4_Finalizar.xaml"
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
            this.dgSaldoBcos = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.dgSaldoContas = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnFinalizar = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\view\RotinaPage4_Finalizar.xaml"
            this.btnFinalizar.Click += new System.Windows.RoutedEventHandler(this.BtnFinalizar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ckContas = ((System.Windows.Controls.CheckBox)(target));
            
            #line 21 "..\..\..\view\RotinaPage4_Finalizar.xaml"
            this.ckContas.Click += new System.Windows.RoutedEventHandler(this.CkContas_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ckBancos = ((System.Windows.Controls.CheckBox)(target));
            
            #line 22 "..\..\..\view\RotinaPage4_Finalizar.xaml"
            this.ckBancos.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LbTotal = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

