   M S B A M L   `   `   `  ���� C  ?UI.Admin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:UI.Admin.ViewsUI.Admin.Views  "    UI.Admin.Views.QuestionsView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  $vclr-namespace:UI.Admin.Views  ���#CCC$��1q�$��
20 0 10 10q�   PageBackground#��[  .���   � ColumnDefinitions  �� $��1* ��� $��3* �.+��  � RowDefinitions �� $
��auto ��� �� $
��auto �.+��� $��0�� �� Orientation$ 
Horizontal=�$��Right=�.+���  � ToolTip$ Add a new question.������ *NewQuestionCommand+.���� $��22��$��22��$3��-/UI.Common;component/Resources/Images/new.png���� $# Delete the selected question.������ *DeleteQuestionCommand+���� *
Question+.���� $
��None=�$6��0/UI.Common;component/Resources/Images/delete.png���� $��Right=�$��2���� -    ��Name$ QuestionsList��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��1�����#CCC$	��0 1q�$��Disabled=����� *	Questions+����  �� 
TargetType" �� �� Triggers P�  P� Property �@��
IsSelected	 P� Value	 . . ��� 
 �� Property
 � �� �� Value �#DDD.��� 
 � �� ��  �� 
TargetType" ��.R��� #��z��._��� ���� .R�%   UI.Admin.Views.QuestionListView  �� $��1��$��1 0 0 0q����#AAA._��� $��Visible=�.���� ���� " ��.��� 
 � �� �� " ��.R���  ��Name$
 Root��.  (System.Windows.Interactivity.Interaction   	Behaviors ?  9Microsoft.Expression.Interactivity.Core.DataStateBehavior     Value$ 0��   	TrueState$ AsEmpty��   
FalseState$ 
AsQuestion��   Binding ��  �� ElementName$ QuestionsList�� �� Path$ SelectedItem� �� TargetNullValue$ 0��'  !System.Windows.VisualStateManager   VisualStateGroups %  System.Windows.VisualStateGroup  QuestionStates��   States.    System.Windows.VisualState  AsEmpty��   
Storyboard. �� .��T�  �� 
TargetName$ question�� �� TargetProperty$ 
Visibility�..�i�  R� KeyTime$ 0�� R� Value   System.Windows.Visibility 	Collapsed=�T� $
 info��$ 
Visibility�..�i� $ 0��  Visible=�  
AsQuestion��. �� .��T� $ question��$ 
Visibility�..�i� $ 0��  Visible=�T� $
 info��$ 
Visibility�..�i� $ 0��  	Collapsed=�._�� .+���  ��Name$
 info��$y�	Collapsed=�$��15q�$
��Wrap=���No question is selected, you can view a question by choosing one frem the list on the left or add a new question by clicking the add button on the top left.!   UI.Admin.Views.QuestionView    Name$ question��$��20q�$y�	Collapsed=� � DataContext �� $ QuestionsList��$ SelectedItem�  �� Mode$  OneWay=�