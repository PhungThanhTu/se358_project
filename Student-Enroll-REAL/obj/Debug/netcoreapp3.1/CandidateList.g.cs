﻿#pragma checksum "..\..\..\CandidateList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A7EDE862D3C83AF386329DA0EAD1CE1A318E0637"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Student_Enroll_REAL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Student_Enroll_REAL {
    
    
    /// <summary>
    /// CandidateList
    /// </summary>
    public partial class CandidateList : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 155 "..\..\..\CandidateList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCandidateList;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\CandidateList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnScoring;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\CandidateList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnStatistic;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\CandidateList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLogOut;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\CandidateList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame Holder;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Student-Enroll-REAL;component/candidatelist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CandidateList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 115 "..\..\..\CandidateList.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 122 "..\..\..\CandidateList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnCandidateList = ((System.Windows.Controls.Button)(target));
            
            #line 158 "..\..\..\CandidateList.xaml"
            this.BtnCandidateList.Click += new System.Windows.RoutedEventHandler(this.BtnCandidateList_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnScoring = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\..\CandidateList.xaml"
            this.BtnScoring.Click += new System.Windows.RoutedEventHandler(this.BtnScoring_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnStatistic = ((System.Windows.Controls.Button)(target));
            
            #line 170 "..\..\..\CandidateList.xaml"
            this.BtnStatistic.Click += new System.Windows.RoutedEventHandler(this.BtnStatistic_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnLogOut = ((System.Windows.Controls.Button)(target));
            
            #line 177 "..\..\..\CandidateList.xaml"
            this.BtnLogOut.Click += new System.Windows.RoutedEventHandler(this.BtnLogOut_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Holder = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

