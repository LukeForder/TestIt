   M S B A M L   `   `   `  ���� C  ?UI.Admin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:UI.Admin.ConvertersUI.Admin.Converters  /clr-namespace:UI.Admin.ViewsUI.Admin.Views      UI.Admin.Views.AnswerView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7	converter!clr-namespace:UI.Admin.Converters     *vclr-namespace:UI.Admin.Views     $��Stretch=�  � 	Resources  2   ,UI.Admin.Converters.TextToParagraphConverter   TextToParagraphConverter��%   &	          .���� ���#AAA$��1q�._�9� $��Stretch=�
�����������  �� 
TargetType" 9�.R�]� '  !System.Windows.VisualStateManager   VisualStateGroups %  System.Windows.VisualStateGroup  ExpansionStates��   States.    System.Windows.VisualState  Expanded��   
Storyboard. �� .��T�  �� 
TargetName$ ContentSite�� �� TargetProperty$ 
Visibility�..�i�  R� KeyTime$ 0�� R� Value   System.Windows.Visibility Visible=�  	Collapsed��. �� .��W� $ ContentSite��$ Opacity�	 g� Duration$	 
00:00:00.0D�	
 W� To$
 0X�W� $ ContentSite��$6 0(Border.LayoutTransform).(ScaleTransform.ScaleY)�$
 0X�$	 00:00:00.25D�T�  g� 	BeginTime$ 00:00:00.25X�$ ContentSite��$ 
Visibility�..�i� $ 0��  	Collapsed=�   Transitions %  System.Windows.VisualTransition     From$ 	Collapsed��	   To$ Expanded��   
Storyboard. �� .��W� $ 00:00:00.20X�$ ContentSite��$ Opacity�$	 00:00:00.05D�$
 1X�W� $ ContentSite��$6 0(Border.LayoutTransform).(ScaleTransform.ScaleY)�$	 00:00:00.25D� W� From$ 0X�  $ 	Collapsed��$ Expanded��. �� .��W� $ ContentSite��$ Opacity�$	 
00:00:00.0D�$
 0X�W� $ ContentSite��$6 0(Border.LayoutTransform).(ScaleTransform.ScaleY)�$	 00:00:00.25D�$
 0X�.+�� $	��Top=�#��z�� � ColumnDefinitions �� $��30 ��� .+�d�   PageBackground#��[ $��Center=�$��Center=�$��3q�$��24��$��24�� d� 	IsChecked �� *
IsExpanded+ �� Mode$ TwoWay=� �� RelativeSource ��  �� Mode$ TemplatedParent=����� " d�.R���  �� CornerRadius$ 10��
������    CheckStates��.   Checked��. �� .��W� $ arrow��$4 .(Path.RenderTransform).(RotateTransform.Angle)�$	
 180X�$	 
00:00:00.2D�  	Unchecked��. �� .��W� $ arrow��$4 .(Path.RenderTransform).(RotateTransform.Angle)�$
 0X�$	 
00:00:00.2D�   $ Checked��$ 	Unchecked��  $ Unhecked��$ Checked��._�(�  (� Stretch$ Uniform=�#��z��#��z��.���� $��34��$��34��.+��� $��layer1��.+��� z�V� .~�T�  T� X$ 
0.20830654Q� T� Y$ 	-0.011017Q�.+�F� $��arrow�����#999$��1��g����)��h@㪕 �)���@�� Ѯ'� JOV�v��v�>-JOV��8��� �̬㪕 �v���5�*��h@㪕 	z���  �� CenterX$ 17Q� �� CenterY$ 17Q� �� Angle$ 0Q��� $��1��$��5q�$	��Top=�#��z����  ��Name$ ContentSite��#��z�� � OverridesDefaultStyle .  � LayoutTransform ��   �� ScaleY$  1Q�._��� ��� $��Stretch=� �� �� $
��auto �.+��� $��Center=�$��14(����� *Text+! �� 	Converter#! [  " �� ConverterParameter$" 40��$ OneWay=�# �� TargetNullValue$ # [This answer has no text.]���� $��1��$��Right=����� *RequestRemovalCommand+.���� $6��0/UI.Common;component/Resources/Images/delete.png����� $��Stretch=�$��Stretch=�$ � RowDefinitions$ �� $
��auto ��� $
��auto ��� $
��auto ���  �� $
��auto ��� .+���  
 Legend#��[ $	��Top=�$��Text:���� $��1��#��[ $	��Top=�$��Image:���� $��1��$��80��% �� TextWrapping$
% Wrap=����� *Text+$ TwoWay=�& �� NotifyOnValidationError& . ' �� ValidatesOnDataErrors' . ( �� UpdateSourceTrigger$( PropertyChanged=�� $��20q�$��1��$��1��$ �� $
��auto ��� .+��� $��Right=�) �� Orientation$) 
Horizontal=�.+��� $��Select��$��50��$��30��$��2q�* � ToolTip$3* -Select the image to display with this answer.������ *SelectImageCommand+�� $* Remove this image.��$��Remove��$��2q�$��50��$��30������ *MakeImageNullCommand+�� $��20q�$��Uniform=�$��1��$	��128��$	��128������ *Image+& . ' . $ OneWay=��� $��2��#��[ $��Marks:��+   %UI.Admin.Views.AssociatedSubjectsView  $��Stretch=�$��3��$��2��+ � DataContext+ �� *
Subjects+& . ' . 