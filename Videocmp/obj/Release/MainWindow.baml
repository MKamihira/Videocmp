   M S B A M L   `   `   `  ���� C  ?Videocmp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:VideocmpVideocmp      Videocmp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Videocmp     -     �Name$  MainWindow1�� � Title$ 動画圧縮ツール��$	��210��$	��600��&   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�   TextRenderingMode$ 	ClearType=� � UseLayoutRounding . '  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 0Q�   ResizeBorderThickness$ 0q� � 	Resources %   Videocmp.CBoolNegativeConverter   boolNeg  
HoverBrush	 �� Color
 �� Opacity �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� EndPoint � MappingMode �� 
StartPoint � Color � Offset �� Triggers P� Property P� Value �� 
TargetName 	 close  size  convAnimeKey g� RepeatBehavior �� 
TargetName �� TargetProperty �� Value �� KeyTime��%&  &	        &	      '�� $     '�� g     '�� �     7  &	 �    &	     &	 �      �� $	 	#FF2789FD��$	
 0.5Q��� " ��.���  � ��$ 10,0,0,0q���  � ��$ Bottom=��� " ��.���  � ��$ 5q��� " ��.���  � ��$	 5,0q���  � �� �� " ��.R��� $��border��$ 5�����	#FF838181$��1q����� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FFACABAB��$ 1Q�� $ White��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ border��8    � ��P�  � �� . . ��� $ border�� � �� �	#FF0B7AFF�� $ border�� �@J�Opacity$	 0.3Q�P�  � {� .  . ��� $ border�� �@J�Opacity$	 0.5Q��� " ��.���   �@ IsHitTestVisibleInChrome . ��  � ��$ 24����  � ��$ 24����  � ��$ Marlett)���  � �� �� " ��.R��� $��captionBorder��$��0q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ captionBorder�� � ��
 �  ����  � ��
 �����P�  � �� . . ��� $ captionBorder�� �@J�Opacity$	 0.3Q��� " ��.���   �@ IsHitTestVisibleInChrome . ��  � ��$ 24����  � ��$ 24����  � ��$ Marlett)���  � �� �� " ��.R��� $��
sizeBorder��$��0q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ 
sizeBorder�� � �� �	#FFD4D4D4��  � ��
 �   �P�  � �� . . ��� $ 
sizeBorder�� � �� �	#FFB0B0B0P�  � {� .  . ���  � �� �	#FFB6B6B6�� $ Forever��.���� $ 
convertNow��$
 Text�.�a� $? 9完了するまで閉じないでください　変換中��$ 0:0:0��a� $B <完了するまで閉じないでください　変換中・��$ 0:0:0.5��a� $E ?完了するまで閉じないでください　変換中・・��$ 0:0:1��a� $H B完了するまで閉じないでください　変換中・・・��$ 0:0:1.5��a� $H B完了するまで閉じないでください　変換中・・・��$ 0:0:2��.���� ���	#FF007ACC���	#FFF3F2F2$��1q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�    
BlurRadius$  8Q�!   Color$! 	#FF007ACC��._�� " � RowDefinitions" �� $��28 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��* ��� $��23 �# � ColumnDefinitions# �� $��* ��� $	��100 ��� $	��100 �.+��� $��0��$��3�����	#FFE8E8E8$��0,0,0,1q����	#FFBFBBBB�� $��0��$��3��$��動画圧縮ツール��$��Center=�
���   �$��16(�$��Center=�$��Modern No. 20)��� $��0��$��2��$��0,0,0,4q�$ �� Orientation$$ 
Horizontal=�$	��Top=�$��Right=�.+��� -   $��0��#��[ �� $��1��{�.  #��[ �� -   $��r��#��[ �� $��1��$��2��$��0�����	#FFFDFBFB% �� Target% �� & �� ElementName$& input��.���� ' �� 
Foreground' �	#FF474747.��対象ファイル(_F)�� -   ( ��Name$( input��$��2��$��2��$��1���� -   $��2��$��2��$��2��.���� .��
参照(_R)�� $��3��$��2��$��3��% �� $& output��.���� ' �	#FF343434.��出力ファイル(_O)�� -   $( output��$��4��$��2��$��4���� -   ) ��Name$
) conv��$��5��$��1��$��5��{�`� * `� 	Converter#* [  + `� UpdateSourceTrigger$+ PropertyChanged=�.1��� , �� Path$
, Text�$& input���� $
, Text�$& output���� $, Enable�.���� .��変換する(_D)�� -   $��5��$��2��$��6��.���� .��クリア(_C)�� $��7��$��3�����	#FF007ACC$��0,1,0,0q�._��� -	   - ��Name$- 
convertNow��
�������$
��Left=�$�� ��$��14(�$��5,0,0,0q�$��Center=�