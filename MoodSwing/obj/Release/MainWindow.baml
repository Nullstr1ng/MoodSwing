   M S B A M L   `   `   `  ����   	MoodSwing    MoodSwing.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     
���   �
�������$��419.335��$��362.304��   
MainWindow  � DataContext#  [   � Icon$ app_icon.ico�� � Title �� *TrackDetails.Title+.���  � RowDefinitions �� $��* ��� $
��auto �.+�� $��0��.+��� $��5,-3,0,0q� � 	Resources  �� 
TargetType �� Property �� Value��%g   '��       �� " ��.���  � ��$ Segoe UI Light)���  � ��$ 30(���  � ��$
 Wrap=�.+��� ��  J� Opacity$
 0.70Q����� *TrackDetails.Artist+	 �� FallbackValue$	 
Jason_Mraz�� �� $
 0.85Q�$
��Bold"����� *TrackDetails.Title+$	 Living_High�� �� $
 0.50Q����� *TrackDetails.Album+$		 Yes��� $��1��.+��� $
��Auto=�.���� $��5q�$
 0.80Q� ��%-   '��       �� " ��.���  � ��$ 15(�.+��� $��Effects���� $��20(�
 �� SelectedIndex
 �� *SelectedEffect+$	 0�� �� Mode$ TwoWay=�.@��� $��Static�� �� 
IsSelected . �� $��Scroll���� $��Dancer���� $��Dance Notes���� $�� ���� $��Pattern���� $��20(����� *	Pattern+$	 %artist%-%title%-%album%��$ TwoWay=��� $�� ����  �� Orientation$ 
Horizontal=�.+��� $�� Update ��$��20(����� *Command_Update+�� $�� ���� $�� About ��$��20(��� $�� ���� $�� Quit ��$��20(�