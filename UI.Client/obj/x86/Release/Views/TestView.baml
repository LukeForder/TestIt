   M S B A M L   `   `   `  ���� D  @UI.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UI.Client.ViewsUI.Client.Views      UI.Client.Views.TestView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  %vclr-namespace:UI.Client.Views    � DataContext  �� *TestViewModel+   Locator �� Source# [  .��]� .+�� $��65��$	��Top=�  MainBackground#��[ .+��� $	��Top=� �� Orientation$ 
Horizontal=�.+��� $��Center=�$
��10 0q�
�������$��22(����� *Name+�� $��Stretch=����� *	Context+���� .R�� $��Stretch=�'  !System.Windows.VisualStateManager   CustomVisualStateManager H  BMicrosoft.Expression.Interactivity.Core.ExtendedVisualStateManager  .  (System.Windows.Interactivity.Interaction   	Behaviors ?  9Microsoft.Expression.Interactivity.Core.DataStateBehavior     Value$
 True��   	TrueState$ AsQuestionList��   
FalseState$ 
AsQuestion��   Binding �� *IsReviewing+	   VisualStateGroups	 %  System.Windows.VisualStateGroup  BehaviorStates��
   States.
    System.Windows.VisualState  AsQuestionList��   
Storyboard. �� .��T�  g� Duration$ 0D� �� 
TargetName$
 List�� �� TargetProperty$ 
Visibility�..�i�  R� Value   System.Windows.Visibility Visible=�T� $ 0D�$ Single��$ 
Visibility�..�i�   	Collapsed=�  
AsQuestion��. �� .��T� $ 0D�$
 List��$ 
Visibility�..�i�   	Collapsed=�T� $ 0D�$ Single��$ 
Visibility�..�i�   Visible=�.+�]�  ]�Name$ TestFinished��$y�	Collapsed=�.+�� $��Bottom=�#��[ .+��� $��22(�$��5q�$��Right=�$
��Done��  NavigationButton#��[ ���� *GoToTestEvaluationCommand+�� $��22(�$��5q�$��Center=�$��Center=�$��Review��  NavigationArrowlessButton#��[ ���� *GoToQuestionsReviewCommand+�� $��22(�$��5q�$
��Left=�$
��Back��  NavigationLeftButton#��[ ���� *GoToPreviousQuestionCommand+�� $��20q�$��14(�$
��Wrap=���You have reached the end of the test. If you need to review any questions that you have missed, click the review button to see a list all all the questions. Otherwise click done to assess the test and get your results... Good luck!��  ��Name$
 List��$��10q�$y�	Collapsed=�.��&    UI.Client.Views.QuestionListView  #��z��]� $ Single��.+�� $��65��#��[ $��Bottom=� � 	Resources  �� 
TargetType �� Property �� Value��%,   '��       �� " ��.���  � ��$ 5q�.+��� $��22(�#��[ $��5q�$
��Back��$
��Left=����� *GoToPreviousQuestionCommand+�� #��[ $��5q�$��22(�$��Review��$��Center=�$��Center=����� *GoToQuestionsReviewCommand+�� $��22(�#��[ $��5q�$
��Next��$��Right=����� *GoToNextQuestionCommand+�� $��10q�.��"	   UI.Client.Views.QuestionView	  #��z��  �� *	Context+ �� Mode$ OneWay=� �� Triggers ��  �� Value$
 true�� �� Binding �� *
IsFinished+.a���  �� 
TargetName$ TestFinished�� �@K�ZIndex$ 1���� $ TestFinished�� � y�$ Visible=��� $
 List�� � {� .  �� $ Single�� � {� .  �� $
 List�� �@J�Opacity$ 0Q��� $ Single�� �@J�Opacity$ 0Q��� $ false�� �� *
IsFinished+.a��� $ TestFinished�� � y�$ 	Collapsed=�