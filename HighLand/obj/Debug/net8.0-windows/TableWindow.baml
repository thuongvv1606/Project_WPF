   M S B A M L   `   `   `  ����C  ?HighLand, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:HighLandHighLand  5   	       HighLand.TableWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:HighLand     5   	     � Title$  TableWindow��5   	   $	��800��6   $
��1200��6*    � WindowStartupLocation$ CenterScreen=�5	   
   .��5
      � .+�5   
   � $��821,0,0,222q�6   5      � 5   
   $
��Left=�6   $	��821��6*   $��	0,0,0,222q�66    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��10q�6O   $��0��6[   .+�5      �� $��Vui lòng chọn bàn��6   $��20(�5      $
��Bold"�5      
���   �$��0, 0, 10, 0q�5      5      �� 5"      -   5         ��Name$ TableListBox��5"      $��Stretch=�5$      ���	#FFE0D0F4$��	0,0,0,-28q�5&      $��1��5(      ��5#      �� *Tables+��5+      �� 5,      .R�5-      A�  A� Rows$ 7��6&    A� Columns$ 6��6/   5.      ��52      �� 53      .R�54      �� $��2q�6!   
�������$��1q�56       � Cursor$
 Hand��57      $	��110��58      $��55��59      ��5:      �� 5;   "    �� 
TargetType" ��6(   .�5<   &   �� 	 �� Property	 � ��
 �� Value

 �����6C    �� Triggers 5=   &   �� 5>   *    �� Binding 66   �� *Status+ �� Value true6Q   .a�5?   .   �� 	 � ��

 ����6K   5@   +   �� 5A   *    66   �� *Status+	 false6Q   .a�5B   .   �� 	 � ��

 �����6K   5C   +   5E   #   ._�5G      � .+�5H   "   �� $
��Bold"�5I   $   $��16(�5J   $   
���   �$��Center=�5L   $   $��Center=�5M   $   ��5H   ,   �� *TableNumber+��5N   &   �� 5O   *   " ��60   .�5P   .   �� 	 � ��

 �   �6K    5Q   .   P� 5R   2    P� Property �@J�IsMouseOver P� Value$
 True��6Q   . �5S   6   �� 	 � ��

 �  ��6S   5T   3   5V   +   5X   #   5Y      5Z      5[      5]      5^      �� 5`   
   $;��5F:\SE\ky9\PRN212\HighLand\HighLand\images\banner.jfif��6   $
��Left=�6O   $	��238��6j   $��	0,546,0,0q�6w   $	��Top=�6�   $	��569��6�   �� 5a   
   $;��5F:\SE\ky9\PRN212\HighLand\HighLand\images\banner.jfif��6   $
��Left=�6O   $	��238��6j   $��562,546,0,0q�6w   $	��Top=�6�   $	��569��6�   �� 5b   
   $;��5F:\SE\ky9\PRN212\HighLand\HighLand\images\banner.jfif��6   $
��Left=�6O   $	��238��6j   $��1124,546,0,0q�6w   $	��Top=�6�   $	��569��6�   5d      5e      