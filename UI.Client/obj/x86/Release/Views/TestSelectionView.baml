   M S B A M L   `   `   `  ����   	UI.Client'    !UI.Client.Views.TestSelectionView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � DataContext  �� *TestSelectionViewModel+   Locator �� Source# [  .���   MainBackground#��[ .+�� $��Center=� � RowDefinitions �� �� $
��auto ��� $
��auto ��� $
��auto �.+��� $
��None=�$��Center=�$��Center=�$:��4/UI.Common;component/Resources/Images/logo-large.png���� $��1��$��24(�
�������$��Center=�  TextBase#��[ $(��"Select the test you wish to write.���� -    ��Name$
 Test��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��10q�$��2�� �� DisplayMemberPath$
 Name��$��3q�$	��200��$��Center=�$��Center=����� *Tests+�� $��Center=�  NavigationButton#��[ $��3��$��Begin!������ *BeginTestCommand+���� *SelectedItem+ �� ElementName$
 Test��