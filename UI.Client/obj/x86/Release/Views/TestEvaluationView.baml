   M S B A M L   `   `   `  ���� D  @UI.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UI.Client.ViewsUI.Client.Views  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem (    "UI.Client.Views.TestEvaluationView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     +vclr-namespace:UI.Client.Views     6sys&clr-namespace:System;assembly=mscorlib       � DataContext  �� *TestEvaluationViewModel+   Locator �� Source# [   � 	Resources   OutcomesDataTemplate!   UI.Client.Views.OutcomeView��%   &	       �� .R�  .���  � RowDefinitions �� $
��auto ��� �� $
��auto �.+�]�   MainBackground#��[ $��0��.+��� $��10q�
�������$��28(�$��Assessment Results���� $��Right=�$��Right=� �� Orientation$ 
Horizontal=�  �� 
TargetType �� Property �� Value��%�   '��       '�� 9     �� " ��.���  � ��$ 48����  � ��$ 48���� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ 32����  � ��$ 32��.+��� $��2q����� *PrintPageCommand+����  �� Path$ .�	 �� ElementName$	 Report��.���� $7��1/UI.Common;component/Resources/Images/printer.png���� $��0 2 2 2q����� *SavePageCommand+���� $ .�$	 Report��.���� $<��6/UI.Common;component/Resources/Images/saveDocument.png��,� -   
 ,�Name$
 Report��" ,� VerticalScrollBarVisibility$ Hidden=�$��1��$��Center=�$��Stretch=�.��/�  /� ColumnWidth$
 9999��.T��� .r�� $��Stretch=�$
��Left=� �� �� .+��� ���#999$��0 0 0 1q�._��  ��%B   '��       �� " ��.���  � ��$ 22(���  � ��$ 5q� �� $
��auto ��� $
��auto � � ColumnDefinitions �� �� $
��auto �.+��� $��0��$��26(�$��Center=����� *Test+�� $��1��$
��Left=�$ 
Horizontal=� ��%-   '��       �� " ��.���  � ��$ 16(�.+��� $��Center=�$	��5 0q�$��
Candidate:���� $��Center=����� *User+�� $��1��$��0��$��24(����� 
 �� Now# Z  �� StringFormat$ {0:yyyy-MM-dd}���� $��1��$��20q�#��[ ���� *
Outcomes+