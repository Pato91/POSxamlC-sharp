   M S B A M L   `   `   `  ����  	Meraki101    Meraki101.Expenses   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  	Payements��5       N� 	KeepAlive . .��5
      ]� .+�5   
    � -   5          �Name$ _expensesFrame��5      � 5   
   $
��Left=�6   $��818.567��6*   $��0,0,0,0q�6:   ���	#FF6EE2D2$	��490��6b   $��Bottom=�6o   .+�5      �� -   5         ��Name$ 
backButton��5      $
��Back��6)   
���/���$
��Left=�6Q   $��24��6l   $��	32,24,0,0q�6x   $	��Top=�6�   $��75��6�   6�   �� 5      $
��User��6   $
��Left=�6#   $��26��6>   $��
573,18,0,0q�6J   $	��Top=�6^   $��41��6v   �� 5      -   5         ��Name$ userNameLabel��5      $�� ��6+   $
��Left=�66   $��33��6Q   $��
624,17,0,0q�6]   $	��Top=�6q   $	��116��6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ expensesDataGrid��5      $
��Left=�61   $	��336��6L   $��
267,94,0,0q�6Y   $	��Top=�6m   $	��492��6�   �� 5      $��
Consumable��6   $
��Left=�6)   $��26��6D   $��	12,92,0,0q�6P   $	��Top=�6c   $	��110��6{   �� 5      -   5         ��Name$ consumableName��5      $
��Left=�6.   $��21��6I   $��
124,94,0,0q�6U    �� TextWrapping$
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��109��6�    J� RenderTransformOrigin$ 0.506,-0.143&�6�   6�   �� 5      $
��Rate��6   $
��Left=�6#   $��25��6>   $��
14,137,0,0q�6J   $	��Top=�6^   $	��109��6v   �� 5      -   5        	 ��Name$	 rateByCombobox��5      $
��Left=�6/   $��21��6J   $��124,140,0,0q�6V   $	��Top=�6k   $	��109��6�   �� 5      $��
Quantifier��6   $
��Left=�6)   $��27��6D   $��
15,181,0,0q�6P   $	��Top=�6d   $	��109��6|   �� 5      -   5        $ serviceHoursTextBox��5      $
��Left=�63   $��21��6N   $��125,183,0,0q�6Z   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��109��6�   6�   �� 5      $��Due Date��6   $
��Left=�6'   $��25��6B   $��
13,223,0,0q�6N   $	��Top=�6b   $	��109��6z   (  "System.Windows.Controls.DatePicker  5      -	   5        
  Name$
 
datePicker��5      $
��Left=�6-   $��24��6H   $��124,224,0,0q�6T   $	��Top=�6i   $	��109��6�   �� 5      $��Priority��6   $
��Left=�6'   $��2��6B   $��
38,270,0,0q�6M   $	��Top=�6a   $��63��6y   �� 5      -
   5        $	 priorityCombobox��5      $
��Left=�61   $��21��6L   $��124,272,0,0q�6X   $	��Top=�6m   $	��109��6�   �� 5      $��
Payment To��6   $
��Left=�6)   $��29��6D   $��
13,314,0,0q�6P   $	��Top=�6d   $	��109��6|   �� 5      -   5        $ paidToTextBox��5      $
��Left=�6-   $��21��6H   $��124,314,0,0q�6T   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��109��6�   6�   �� 5      $��Unit Amount Paid��6   $
��Left=�6/   $��26��6J   $��
14,356,0,0q�6V   $	��Top=�6j   $	��109��6�   �� 5      -   5        $ unitAmountPaid��5      $
��Left=�6/   $��21��6J   $��125,357,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��109��6�   6�   �� 5      -   5        $ 	payButton��5      $	��Pay��6(   
���/���$
��Left=�6O   $��21��6j   $��143,393,0,0q�6v   $	��Top=�6�   $��64��6�   6�   �� 5       $��Priority��6   $
��Left=�6'   $��24��6B   $��
14,267,0,0q�6N   $	��Top=�6b   $��60��6z   5"      5#      5%      