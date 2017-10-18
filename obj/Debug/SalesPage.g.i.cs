﻿#pragma checksum "..\..\SalesPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A6DBC8E81E9576630DC3A9419D74CD57"
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
    /// SalesPage
    /// </summary>
    public partial class SalesPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame _salesPageFrame;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateFilter;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid salesDataGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.SearchControl salesSearchControl;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userNameLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backToInventoryPage3;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterByComboBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox salesTypeCheckBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox soldByNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\SalesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button printSalesReportBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/Meraki101;component/salespage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SalesPage.xaml"
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
            
            #line 10 "..\..\SalesPage.xaml"
            ((Meraki101.SalesPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.SalesPage_Load);
            
            #line default
            #line hidden
            return;
            case 2:
            this._salesPageFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 3:
            this.dateFilter = ((System.Windows.Controls.DatePicker)(target));
            
            #line 17 "..\..\SalesPage.xaml"
            this.dateFilter.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePicker_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.salesDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\SalesPage.xaml"
            this.salesDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.salesSearchControl = ((DevExpress.Xpf.Editors.SearchControl)(target));
            return;
            case 6:
            this.userNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.backToInventoryPage3 = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\SalesPage.xaml"
            this.backToInventoryPage3.Click += new System.Windows.RoutedEventHandler(this.backToInventoryPage3_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.filterByComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 24 "..\..\SalesPage.xaml"
            this.filterByComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filterComboBox_SelectedItem);
            
            #line default
            #line hidden
            return;
            case 9:
            this.salesTypeCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 26 "..\..\SalesPage.xaml"
            this.salesTypeCheckBox.Checked += new System.Windows.RoutedEventHandler(this.SalesType_Checked);
            
            #line default
            #line hidden
            
            #line 26 "..\..\SalesPage.xaml"
            this.salesTypeCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.SalesType_Unchecked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.soldByNameComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.printSalesReportBtn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\SalesPage.xaml"
            this.printSalesReportBtn.Click += new System.Windows.RoutedEventHandler(this.printSalesReportBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
