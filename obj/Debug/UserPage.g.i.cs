﻿#pragma checksum "..\..\UserPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "015502AE4ED6FD1B9557C80CC7A37B0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
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
    /// UsersPage
    /// </summary>
    public partial class UsersPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame _viewUserFrame;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid employeeDataGrid;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backToUsersBtn;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteUserBtn;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userNameLabel3;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addUserBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editUserBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.SearchControl searchUserControl;
        
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
            System.Uri resourceLocater = new System.Uri("/Meraki101;component/userpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserPage.xaml"
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
            this._viewUserFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.employeeDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 16 "..\..\UserPage.xaml"
            this.employeeDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.employeeDataGridRow_DoubleClicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.backToUsersBtn = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\UserPage.xaml"
            this.backToUsersBtn.Click += new System.Windows.RoutedEventHandler(this.backToUsersBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.deleteUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\UserPage.xaml"
            this.deleteUserBtn.Click += new System.Windows.RoutedEventHandler(this.deleteUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.userNameLabel3 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.addUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\UserPage.xaml"
            this.addUserBtn.Click += new System.Windows.RoutedEventHandler(this.addUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.editUserBtn = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\UserPage.xaml"
            this.editUserBtn.Click += new System.Windows.RoutedEventHandler(this.editUserBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.searchUserControl = ((DevExpress.Xpf.Editors.SearchControl)(target));
            
            #line 24 "..\..\UserPage.xaml"
            this.searchUserControl.KeyDown += new System.Windows.Input.KeyEventHandler(this.searchUser_KeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
