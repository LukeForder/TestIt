   M S B A M L   `   `   `  ����D  @UI.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UI.Client.ViewsUI.Client.Views  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      (    "UI.Client.Views.TestEvaluationView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      +vclr-namespace:UI.Client.Views     5      6sys&clr-namespace:System;assembly=mscorlib     5        � DataContext  5      �� *TestEvaluationViewModel+   Locator �� Source# [   � 	Resources 5        OutcomesDataTemplate!   UI.Client.Views.OutcomeView��%2   &	       �� 5	   
   .R�5        5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �5      �� 5      �� 5      $
��auto �5      .+�5   
   ]�   MainBackground#��[ 5      $��0��5      .+�5      �� $��10q�5      
�������$��28(�5      $��Assessment Results��5      �� 5      $��Right=�5      $��Right=�5        �� Orientation$ 
Horizontal=�5!       5"       �� 
TargetType �� Property �� Value��%\  '��       '�� x     �� 5#      " ��5$      .�5%      ��  � ��$ 48��5'      �� 5(       � ��$ 48��5*      5+      �� 5,      " ��5-      .�5.      ��  � ��$ Center=�50      �� 51       � ��$ Center=�53      �� 54       � ��$ 32��56      �� 57       � ��$ 32��59      5:      5;      .+�5<      �� $��2q�5=      ��5>      �� *PrintPageCommand+��5?      ��  �� Path$ .�	 �� ElementName$	 Report��.��5@      �� $7��1/UI.Common;component/Resources/Images/printer.png��5A      5B      �� 5C      $��0 2 2 2q�5D      ��5E      �� *SavePageCommand+��5F      �� $ .�$	 Report��.��5G      �� $<��6/UI.Common;component/Resources/Images/saveDocument.png��5H      5I      5J      5K      ,� 5L   
   -   5        
 ,�Name$
 Report��5N      " ,� VerticalScrollBarVisibility$ Hidden=�5M      $��1��5O      $��Center=�5P      $��Stretch=�5Q      .��5R      /�  /� ColumnWidth$
 9999��6   .T�5S      �� .r�5T      � $��Stretch=�5U      $
��Left=�5V       5W      �� 5X      �� 5Y      .+�5[      �� ���#999$��0 0 0 1q�5]      ._�5^      �  5_      ��%�   '��       �� 5`   "   " ��5a   %   .�5b   &   ��  � ��$ 22(�5d   )   �� 5e   &    � ��$ 5q�5g   )   5h   #   5i       5j      �� 5k   "   $
��auto �5l   %   �� 5m   "   $
��auto �5n   %    � ColumnDefinitions 5p      �� 5q   "   �� 5r   "   $
��auto �5s   %   .+�5u      �� $��0��5v   !   $��26(�5w   !   $��Center=�5x   !   ��5y   !   �� *Test+�� 5z      $��1��5{   !   $
��Left=�5|   !   $ 
Horizontal=�5}   !    5~   "   ��%Z   '��       �� 5   &   " ��5�   )   .�5�   *   ��  � ��$ 16(�5�   -   5�   '   5�   #   .+�5�   "   �� $��Center=�5�   %   $	��5 0q�5�   %   $��
Candidate:��5�   %   �� 5�   "   $��Center=�5�   %   ��5�   %   �� *User+5�      �� 5�      $��1��5�   !   $��0��5�   !   $��24(�5�   !   ��5�   !   �� 
 �� Now# Z  �� StringFormat$ {0:yyyy-MM-dd}��5�      5�      �� 5�      $��1��5�      $��20q�5�      #��[ 5�      ��5�      �� *
Outcomes+5�      5�      5�      5�      5�      5�      5�      