   M S B A M L   `   `   `  ����  	UI.Client'    !UI.Client.Views.TestSelectionView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5
      �� *TestSelectionViewModel+   Locator �� Source# [  .��5      �   MainBackground#��[ 5   	   .+�5   
   � $��Center=�5       � RowDefinitions 5      �� 5      �� 5      $
��auto �5      �� 5      $
��auto �5      �� 5      $
��auto �5      .+�5      �� $
��None=�5      $��Center=�5      $��Center=�5      $:��4/UI.Common;component/Resources/Images/logo-large.png��5      �� 5      $��1��5      $��24(�5       
�������$��Center=�5"        TextBase#��[ 5#      $(��"Select the test you wish to write.��5$      �� 5%      -   5         ��Name$
 Test��5&      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5'      $��10q�5(      $��2��5)       �� DisplayMemberPath$
 Name��5+      $��3q�5,      $	��200��5-      $��Center=�5.      $��Center=�5/      ��5*      �� *Tests+5/      �� 50      $��Center=�53        NavigationButton#��[ 54      $��3��55      $��Begin!��56      ��51      �� *BeginTestCommand+��52      �� *SelectedItem+ �� ElementName$
 Test��56      57      58      59      