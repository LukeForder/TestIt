   M S B A M L   `   `   `  ����   UI.Admin    UI.Admin.Views.Shell   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$>  8I.T. Aptitude Assessment & Interest Analysis Tool: Admin�� � WindowState$ 	Maximized=� � DataContext ��  �� Path$ ShellViewModel�   Locator �� Source# [  .���  � RowDefinitions �� $
��auto ��� .+��� $��0 0 0 1q�
���   �._�]�   MainBackground#��[ .+��� $��55��$��24��$
��10 5q�.+�(� $��24��.���� -    ��Name$
 Logo��$
��None=�$4��./UI.Common;component/Resources/Images/gear.png���� $��admin��
�������$��15(�$��Italic$� �� Bottom$ -5�� �� Left$ 15���� 	 �� Orientation$	 
Horizontal=�$
��Left=�$��Center=�
 � 	Resources
   ButtonBaseStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding �� Triggers P� Property P� Value �� RelativeSource��%�  &	       7  �� " 	�.���  � ��$ 20(���  � ��$	 5 3q���  � ��
 �������  �@�Cursor$
 Hand����  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0 5 0 0q���  � �� �� " 	�.R��� $ Outer��#��z��# z��#��z��#��z��._��� #��z��#��z�� P�  �@J�IsMouseOver . . ���  � ��
 ������� 8    � ����  �� *
Background+ �� *Self+ � ��.+�	� $��Subjects������ *GoToSubjects+����  �� BasedOn# [ " 	� �� Triggers ��  �� Binding �� *CurrentView+ �� Value SubjectsView.a���  � ��
 �������  � ��$ SemiBold"���  � {� .  	� $��Tests������ *	GoToTests+���� # [ " 	� ��  �� *CurrentView+ 	TestsView.a���  � ��
 �������  � ��$ SemiBold"���  � {� .   � -     �Name$ MainContent��  PageBackground#��[ $��0q�$��1��  � JournalOwnership$ OwnsJournal=�  � NavigationUIVisibility$ Hidden=�