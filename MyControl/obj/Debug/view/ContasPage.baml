   M S B A M L   `   `   `  ����D  @MyControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MyControl.viewMyControl.view  5          MyControl.view.ContasPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:MyControl.view     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  
ContasPage��5
      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��349* �6   �� 5      $
��451* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgContas��5      $��0,0,0,0q�6%      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 66   $��1��6F      CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     SelectionMode$ Single=�6�      
IsReadOnly . 6�   �� 5   
   -   5        	 ��Name$	 lbNomeConta��5      $��30(�6%   $��Nome da Conta��63   $��Center=�6K   $��	75,5,75,0q�6o   $	��Top=�6�   $
��Bold"�6�   ���	#FF03A9F46�   �� 5   
   -   5        
 ��Name$
 lbDescrConta��5      $n��hDescrição da Conta Descrição da Conta Descrição da Conta Descrição da Conta Descrição da Conta��6*   $
��Wrap=�6�   $��
45,68,45,0q�6�   $	��Top=�6�    �� Padding$ 0,0,0,0q�6�   $	��100��6�   ���	#FFF0F0F06�   �� 5   
   $��Saldo:��6   $��45,181,45.333,0q�6!   $	��Top=�6:   �� 5   
   -   5        $	 lbSaldoConta��5      $��R$ 50.00��6&   $��45,206,45.333,0q�69   $��25(�6R   $��Center=�6`   $	��Top=�6�   $��44��6�   �� 5   
   -   5         ��Name$ btnTrans��5      $��
Transferir��6#   $
��Left=�68   $��
45,292,0,0q�6S   $	��Top=�6g   $��92��6   ���	#DDFFFFFF6�   �� 5   
   -   5        $ btnInativar��5      $��Inativar��6&   $��0,0,29.333,24q�69   ���	#DDFFFFFF���	#FFF76A6A$��32��6�   $��Bottom=�6�   $��Right=�6�   $	��106��6�   ��6g   Z� 6�   �� 5   
   -   5        $ btnEstatisticas��5      $��Estatístias��6*   $
��Left=�6@   $��	45,0,0,24q�6[   $	��104��6n   ���	#DDFFFFFF$��32��6�   $��Bottom=�6�   �� 5   
   -   5         ��Name$ txTransf��5      $
��Left=�6$   $��5,0,0,0q�6?   $��Center=�6Q   $��32��6s   $��142,292,0,0q�6    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��82��6�   $��1q�6�   �� 5   
   $
��para��6   $��0,0,0,0q�6   $��Center=�61   $
��Left=�6U   $��Center=�6p   $��229,292,0,0q�6�   $	��Top=�6�   $��32��6�   $��29��6�   �� 5   
   -	   5         ��Name$ cbContas��5      $��10,0,0,0q�6%   $��Center=�68   $��263,292,45.333,0q�6Z   $	��Top=�6t   $��32��6�   �� 5   
   -
   5        $ 	btnVoltar��5   ,   $
��Left=�6   $��0,0,0,0q�6?   $��	29,16,0,0q�6Q   $	��Top=�6d   $��41��6|   $��33��6�   ��6�   Z� ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ 	ArrowBack=�6&   $��30��67   $��32��6C   5      �� 5   
   -   5        $��	Add Conta��5      $��1��6%   $��Right=�65   $��0,5,25,0q�6Q   $	��Top=�6c   $	��110��6{   ���	#DDFFFFFF���	#FF61B200��6�   Z� 6�   �� 5    
   $��Grupo:��6   $
��Left=�6!   $��
45,329,0,0q�6<   $	��Top=�6P   $��61��6h   �� 5!   
   -   5        $	 lbGrupo��5!      $	��BRL��6!   $
��Left=�6/   $��111,329,0,0q�6J   $	��Top=�6_   $��61��6w   5#      5$      