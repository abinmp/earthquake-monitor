   M S B A M L   `   `   `  ����F  BEarthquakes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:EarthquakesEarthquakes  5   	       Earthquakes.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Earthquakes     5   	     � Title$  Earthquake Monitor��5   	   $	��780��6$   $	��690��61    � Icon$  ./Resources/earth-icon.png��5	   	   .��5
   
   �� 
���p�$��1q�6+    �� Padding$ 1q�66   
�������$��1,1,1,1q�5   $    �� CornerRadius$ 1��6>   $	��Top=�6O   ._�5   
   �� .+�5      ]� .+�5      �� $
��Bold"�6   
�������$��20(�6A   $��Center=�6O   Earthquake List6}   �� 5      -   5        $��LastUpdated��5      
�������$��Bottom=�6B   $��Right=�6]   $
��Bold"�6y   6�   5      ]� 5      .+�5      �� -   5        $��
TxtMessage��5      $y�	Collapsed=�6.   
���  ��6X   5      �� 5      -   5        $��LvQuakes��5   "   $��2q�6   $	��700��62   $��Visible=�6?   ��5      �� 5      .R�5      �� $��1q�6!   $ 1q�6,   
�������
���p�$	��Top=�5      ._�5      �� $��3q�6*    �� Orientation$ Vertical=�65   $	��625��6L   .+�5   "   ]� .+�5   &   �� $
��Bold"�6G   $��13(�6Y   ��60   �� *Place+6Y   �� 5   &   $��       ��60   �� 5   &   $
��Bold"�6`   $��13(�6r   ��60   �� *
DateTime+ �� StringFormat$ {0:F}��6r   �� 5   &   $��	         ��60   �� 5    &   $
��Bold"�6K   
���  ��$��13(�6r   ��60   �� *	Magnitude+6r   �� 5!   &   $
�� mag��60   $
��Bold"�6=   $��13(�6O   
���  ��6]   5"   #   ]� 5#   "   .+�5$   &   �� $
��Lat:��60   �� 5%   &   $
��Bold"�6J   ��60   �� *
Latitude+6J   �� 5&   &   $��  ��60   �� 5'   &   $��Long:��60   �� 5(   &   $
��Bold"�6K   ��60   �� *	Longitude+6K   �� 5)   &   $��  ��60   �� 5*   &   $��Depth:��60   �� 5+   &   $
��Bold"�6G   ��60   �� *Depth+6G   �� 5,   &   $��km��60   $
��Bold"�6:   �� 5-   &   $��  ��60   �� 5.   &   $��Nearest Cities:��60   �� 5/   &   $
��Bold"�6O   ��60   �� *NearestCities+6O   50   #   51      52      53      55      56      57      58      