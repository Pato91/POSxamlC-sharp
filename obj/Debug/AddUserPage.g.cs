﻿#pragma checksum "..\..\AddUserPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "383941B347CD9C219C0D9DAD2AC1BF8F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.WindowsUI;
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


namespace Meraki101 {
    
    
    /// <summary>
    /// AddUserPage
    /// </summary>
    public partial class AddUserPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame _adduserFrame;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passcodeTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createUserBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userNameLabel1;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userContact;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\AddUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox makeAdminChkBx;
        
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
            System.Uri resourceLocater = new System.Uri("/Meraki101;component/adduserpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddUserPage.xaml"
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
            this._adduserFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.firstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\AddUserPage.xaml"
            this.firstNameTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.firstNameTextBox_PreviewText);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\AddUserPage.xaml"
            this.lastNameTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.lastNameTextBox_PreviewText);
            
            #line default
            #line hidden
            return;
            case 4:
            this.userNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.passcodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.createUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\AddUserPage.xaml"
            this.createUserBtn.Click += new System.Windows.RoutedEventHandler(this.createUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 38 "..\..\AddUserPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.userNameLabel1 = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.userContact = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\AddUserPage.xaml"
            this.userContact.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.userContact_PreviewText);
            
            #line default
            #line hidden
            return;
            case 10:
            this.makeAdminChkBx = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

