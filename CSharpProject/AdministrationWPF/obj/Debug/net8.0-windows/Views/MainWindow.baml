   M S B A M L   `   `   `  ����L  HAdministrationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdministrationWPF.ViewsAdministrationWPF.Views  5   	   (    "AdministrationWPF.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdministrationWPF.Views     5   	     � Title$  Administrace��5   	   $	��450��6   $	��800��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��2* �6   �� 5      $
��1.2* �6   .+�5   
   �� $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5      �� $��Přihlášky��6   $��20��6)   $	��100��65   $
��Left=�6A   $	��5 5q�6\   ��5      �� * NavigateApplicationFormCommand+�� 5      $��Školy��6   $��20��6%   $	��100��61   $
��Left=�6=   $	��5 5q�6X   ��5      �� *NavigateHighSchoolCommand+5      �� 5   
   $��1��6   $��1��6&   ��66   �� *Navigation.CurrentView+5      �� 5   
   $��2��6   $��1��6   5!      5"      5#      