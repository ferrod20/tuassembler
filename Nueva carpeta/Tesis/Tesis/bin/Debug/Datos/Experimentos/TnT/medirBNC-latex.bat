..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsj.tt" "BNC\Mediciones\bnc_VS_bncTnTWSJ.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con WSJ)" "BNC" "TnT(BNC)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsj+NFI.tt" "BNC\Mediciones\bnc_VS_bncTnTWSJ+NFI.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con WSJ + NFI)" "BNC" "TnT(WSJ+NFI)" -bnc 
..\..\..\tesis -comparar "BNC\Taggeado\wsj.tt" "BNC\Taggeado\wsj+NFI.tt" "BNC\Mediciones\bncTnTwsj_VS_bncTnTwsj+NFI.l" -l "BNC etiquetado con TnT (entrenado con WSJ) vs BNC etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)" -bnc 

..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM1.tt" "BNC\Mediciones\bnc_VS_bncTnTwsjM2.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con 2 mitad de WSJ)" "BNC" "TnT(2WSJ)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM2.tt" "BNC\Mediciones\bnc_VS_bncTnTwsjM1.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con 1 mitad de WSJ)" "BNC" "TnT(1WSJ)" -bnc 

..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM1+NFI.tt" "BNC\Mediciones\bnc_VS_bncTnTwsjM2+NFI.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con 2 mitad de WSJ+NFI)" "BNC" "TnT(2WSJ+NFI)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM2+NFI.tt" "BNC\Mediciones\bnc_VS_bncTnTwsjM1+NFI.l" -l "BNC original contra BNC etiquetado con TnT (entrenado con 1 mitad de WSJ+NFI)" "BNC" "TnT(1WSJ+NFI)" -bnc 

..\..\..\tesis -comparar "BNC\Taggeado\wsjM1.tt" "BNC\Taggeado\wsjM1+NFI.tt" "BNC\Mediciones\bncTnTwsjM2_vs_bncTnTwsjM2+NFI.l" -l "BNC etiquetado por TnT (entrenado con 2 mitad WSJ) vs BNC etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)" -bnc 
..\..\..\tesis -comparar "BNC\Taggeado\wsjM2.tt" "BNC\Taggeado\wsjM2+NFI.tt" "BNC\Mediciones\bncTnTwsjM1_vs_bncTnTwsjM1+NFI.l" -l "BNC etiquetado por TnT (entrenado con 1 mitad WSJ) vs BNC etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)" -bnc 
