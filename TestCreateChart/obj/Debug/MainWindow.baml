   M S B A M L   `   `   `  ����J  FTestCreateChart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TestCreateChartTestCreateChart  5   	        TestCreateChart.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9testCreateChartclr-namespace:TestCreateChart     5   	     � Title$  
MainWindow��5   	    � DataContext 5	   	   ��  �� RelativeSource �� *Self+ � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Themes/Generic.xaml?�6%   6D   %    5       J� InputBindings 5      �� 5   
   
 �� Key$ Up��6    �� Command 6   �� *IncrementCommand+6E   �� 5   
   $
 Down��6    6    �� *DecrementCommand+6G   .��5      �  5   
   	 �� 
TargetType
 �� Property �� Value#   TestCreateChart.ChartAxisItem��%�   '��       '  r     �� 5      "	 ��6   .�5      �� 
 � ��
 �   �6;   �� 5      
 � ��$ 2��64   6@   5      �� 5      "	  6   .�5      �� 
 � ��$ 20(�6-   6:   5      5       � ColumnDefinitions 5    
   �� 5!      6!   �� 5"      6!    � RowDefinitions 5$   
   �� 5%      $��9* �6   6*   �� 5&      $
��Auto �6   6,   .+�5)   
   "   TestCreateChart.MeterControl  -   5          Name$ MyMeter��5)   ;   $��2��6'   $��0��6L   $��Stretch=�6Z   
�����_�$��80��6(     MinValue 6v   �� *
MinValue+   MaxValue 6�   �� *
MaxValue+   
UpperValue 6�   �� *
UpperValue+ �� Mode$ TwoWay=�   
LowerValue 6�   �� *
LowerValue+$ TwoWay=�65  �� 5*   
   $��0��6   
���   �$	��100��6m   $��30��6y   $��1��6�   ��63   ��  �� ElementName$ MyMeter�� �� Path$ 
UpperValue�6�   �� 5+   
   
���   �$��1��6#   $	��100��6m   $��30��6y   $��1��6�   ��63   �� $ MyMeter��$ 
LowerValue�6�   5,      5-      