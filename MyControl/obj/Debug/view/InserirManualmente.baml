   M S B A M L   `   `   `  ����D  @MyControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MyControl.viewMyControl.view  5   	   '    !MyControl.view.InserirManualmente   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:MyControl.view     5   	     � Title$  InserirManualmente��5   	   $��554.028��6$   $��547.646��65    � WindowStartupLocation$ CenterScreen=�6E    � 
ResizeMode$ NoResize=�6j    � WindowStyle$ 
ToolWindow=�6�   .��5	      � .+�5
   
   �� $��Transações Manuais ��6   $
��Left=�6.   $��	19,10,0,0q�6I   $	��Top=�6\   $	��119��6t   �� 5   
   -   5        $��lbBancoEmQuestao��5   #   $��Bradesco��6   $
��Left=�6;   $��
158,10,0,0q�6V   $	��Top=�6j   $	��247��6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��Center=�6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.153Q�6&   T� 5      5      5      �� 5   
   -   5         ��Name$ txDescricao��5      $
��Left=�6'   $��23��6B   $��	93,64,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��422��6�   �� 5   
   -   5        $ txValor��5      $
��Left=�6#   $��23��6>   $��
93,106,0,0q�6J   $
 Wrap=�6^   $	��Top=�6r   $	��120��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtpData��5      $
��Left=�6&   $��
93,146,0,0q�6A   $	��Top=�6U   $	��120��6m   �� 5   
   $��Descrição��6   $
��Left=�6$   $��	19,64,0,0q�6?   $	��Top=�6R   $��69��6j   �� 5   
   $��Valor��6   $
��Left=�6    $��
19,106,0,0q�6;   $	��Top=�6O   $��69��6g   �� 5   
   $
��Data��6   $
��Left=�6   $��
19,146,0,0q�6:   $	��Top=�6N   $��69��6f   &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 dgTransacoes��5      $
��Left=�6)   $	��278��6D   $��
10,194,0,0q�6Q   $	��Top=�6e   $	��522��6}   
   
IsReadOnly
 .    CanUserAddRows .     CanUserDeleteRows .  6�   �� 5   
   -   5         ��Name$ btnAdicionar��5      $��	Adicionar��6'   
�������$
��Left=�6N   $��333,124,0,0q�6i   $	��Top=�6~   $	��128��6�   $��47��6�   5      �� 5    
   -   5        $ btnFinalizar��5       $��	Finalizar��6'   
�������$��Center=�6O   $��234,0,234,11.667q�6l   $��74��6�   $��Bottom=�6�   6�   �� 5!   
   -   5        $��cbTipo��5!   f   $
��Left=�6   $��218,101,0,0q�6.   $	��Top=�6C   $��94��6[   .@�5"      �� .��6   	Débito5$      �� 5%      .��6   
Crédito5'      5(      5*      5+      