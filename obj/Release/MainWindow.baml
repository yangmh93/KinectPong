   M S B A M L   `   `   `  ���� E  AKinectPong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:KinectPong
KinectPong      KinectPong.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:KinectPong     -     � Title$  
MainWindow��$	��600��$	��600�� � WindowStartupLocation$ Manual=� � Left$ 0��
 � Top$ 0�� � 	Resources    MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush��%]   &	        &	      &	 *     �� $ 	#ff6e6e6e���� $ 	#ff52318f���� $ 	#ff00BCF2��.��� -    �Name$ UIGrid��$��0 0 0 0q� � RowDefinitions �� $	��600 � � ColumnDefinitions �� $	��600 �.+�(� $��0��$��0��$��Center=�.���� $��UniformToFill=����� *ImageSource+�� -   $��	statusBar��$��1��$��Stretch=�$��Bottom=�
�������#��[  .@��� ���� *
StatusText+A� -   	 A�Name$	 uniGrid��$��0��$��1���� -   
 ��Name$
 
canvasOver��$��0��$��1��.+��� -   $��scoreOneLabel��$��0��$��40��$
��Left=�$��
200,50,0,0q�$	��Top=�$��18(�$��40��$��Center=�
��������� -   $��scoreTwoLabel��$��0��$��40��$
��Left=�$��
360,50,0,0q�$	��Top=�$��18(�$��40��$��Center=�
��������� -    ��Name$
 Hand�� �� Left$ 0��
 �� Top$ 0��$��1��$��Images\openhand.png��$
��Fill=�$
��Left=�$	��Top=�$��50��$��50���� -	   $ Hand2��$ 0��$ 0��$��1��$��Images\openhand.png��$
��Fill=�$
��Left=�$	��Top=�$��50��$��50��