   M S B A M L   `   `   `  ����D  @UI.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UI.Client.ViewsUI.Client.Views  5	          UI.Client.Views.TestView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      %vclr-namespace:UI.Client.Views  5	        � DataContext  5      �� *TestViewModel+   Locator �� Source# [  .��5      ]� .+�5   
   � $��65��5      $	��Top=�5        MainBackground#��[ 5      .+�5      �� $	��Top=�5       �� Orientation$ 
Horizontal=�5      .+�5      �� $��Center=�5      $
��10 0q�5      
�������$��22(�5      ��5      �� *Name+5      5      �� 5    
   $��Stretch=�5!      ��5"      �� *	Context+��5#      �� 5$      .R�5%      � $��Stretch=�5&      '  !System.Windows.VisualStateManager   CustomVisualStateManager 5'      H  BMicrosoft.Expression.Interactivity.Core.ExtendedVisualStateManager  5(      .  (System.Windows.Interactivity.Interaction   	Behaviors 5*      ?  9Microsoft.Expression.Interactivity.Core.DataStateBehavior  5+         Value$
 True��5-   !      	TrueState$ AsQuestionList��5.   !      
FalseState$ 
AsQuestion��5/   !      Binding 5,   !   �� *IsReviewing+5/   !   	   VisualStateGroups	 51      %  System.Windows.VisualStateGroup  52      BehaviorStates��
   States.
 54   "      System.Windows.VisualState  AsQuestionList��   
Storyboard. 56   &   �� .��57   *   T�  g� Duration$ 0D�58   -    �� 
TargetName$
 List��59   -    �� TargetProperty$ 
Visibility�5:   -   ..�5;   .   i�  R� Value 5<   2     System.Windows.Visibility 5=   6   Visible=�5?   7   5A   /   5B   +   T� 5C   *   $ 0D�5D   -   $ Single��5E   -   $ 
Visibility�5F   -   ..�5G   .   i�  5H   2    5I   6   	Collapsed=�5K   7   5M   /   5N   +   5O   '   5P   #     5Q   "   
AsQuestion��. 5S   &   �� .��5T   *   T� $ 0D�5U   -   $
 List��5V   -   $ 
Visibility�5W   -   ..�5X   .   i�  5Y   2    5Z   6   	Collapsed=�5\   7   5^   /   5_   +   T� 5`   *   $ 0D�5a   -   $ Single��5b   -   $ 
Visibility�5c   -   ..�5d   .   i�  5e   2    5f   6   Visible=�5h   7   5j   /   5k   +   5l   '   5m   #   5�      .+�5�      ]�  ]�Name$ TestFinished��5�      $y�	Collapsed=�5�      .+�5�      � $��Bottom=�6#   #��[ 5�   #   .+�5�   "   �� $��22(�5�   %   $��5q�5�   %   $��Right=�5�   %   $
��Done��5�   %     NavigationButton#��[ 5�   %   ��5�   %   �� *GoToTestEvaluationCommand+5�   %   �� 5�   "   $��22(�5�   %   $��5q�5�   %   $��Center=�5�   %   $��Center=�5�   %   $��Review��5�   %     NavigationArrowlessButton#��[ 5�   %   ��5�   %   �� *GoToQuestionsReviewCommand+5�   %   �� 5�   "   $��22(�5�   %   $��5q�5�   %   $
��Left=�5�   %   $
��Back��5�   %     NavigationLeftButton#��[ 5�   %   ��5�   %   �� *GoToPreviousQuestionCommand+5�   %   5�      �� 5�      $��20q�5�   !   $��14(�5�   !   $
��Wrap=�5�   !   ��You have reached the end of the test. If you need to review any questions that you have missed, click the review button to see a list all all the questions. Otherwise click done to assess the test and get your results... Good luck!5�      5�      �� 5�       ��Name$
 List��5�      $��10q�5�      $y�	Collapsed=�5�      .��5�      &    UI.Client.Views.QuestionListView  #��z��5�   !   5�      ]� 5�      $ Single��5�      .+�5�      � $��65��5�   !   #��[ 5�   !   $��Bottom=�5�   !    � 	Resources 5�   "    �� 
TargetType �� Property �� Value��%Y   '��       �� 5�   &   " ��5�   )   .�5�   *   ��  � ��$ 5q�5�   -   5�   '   5�   #   .+�5�   "   �� $��22(�5�   %   #��[ 5�   %   $��5q�5�   %   $
��Back��5�   %   $
��Left=�5�   %   ��5�   %   �� *GoToPreviousQuestionCommand+5�   %   �� 5�   "   #��[ 5�   %   $��5q�5�   %   $��22(�5�   %   $��Review��5�   %   $��Center=�5�   %   $��Center=�5�   %   ��5�   %   �� *GoToQuestionsReviewCommand+5�   %   �� 5�   "   $��22(�5�   %   #��[ 5�   %   $��5q�5�   %   $
��Next��5�   %   $��Right=�5�   %   ��5�   %   �� *GoToNextQuestionCommand+5�   %   5�      �� 5�      $��10q�5�   !   .��5�   "   "	   UI.Client.Views.QuestionView	  #��z��5�   %     5�   %   �� *	Context+ �� Mode$ OneWay=�5�      5�      5�       �� Triggers 5�      �� 5�       �� Value$
 true��5�       �� Binding 5�      �� *
IsFinished+.a�5�      ��  �� 
TargetName$ TestFinished��5�   !    �@K�ZIndex$ 1��5�   !   �� 5�      $ TestFinished��5�   !    � y�$ Visible=�5�   !   �� 5�      $
 List��5�   !    � {� .  5�   !   �� 5�      $ Single��5�   !    � {� .  5   !   �� 5     $
 List��5  !    �@J�Opacity$ 0Q�5  !   �� 5     $ Single��5  !    �@J�Opacity$ 0Q�5  !   5	     �� 5
     $ false��5      5     �� *
IsFinished+.a�5     �� $ TestFinished��5  !    � y�$ 	Collapsed=�5  !   5     5     5     5     5     