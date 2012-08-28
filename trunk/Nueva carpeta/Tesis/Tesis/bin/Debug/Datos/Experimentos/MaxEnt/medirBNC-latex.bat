..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsj.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJ.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con WSJ)" "BNC" "MaxEnt(BNC)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsj+NFI.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJ+NFI.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con WSJ + NFI)" "BNC" "MaxEnt(WSJ+NFI)" -bnc 
..\..\..\tesis -comparar "BNC\Taggeado\wsj.tt" "BNC\Taggeado\wsj+NFI.tt" "BNC\Mediciones\bncMaxEntWSJ_VS_bncMaxEntWSJ+NFI.l" -l "BNC etiquetado con MaxEnt (entrenado con WSJ) vs BNC etiquetado con MaxEnt (entrenado con WSJ + NFI)" "MaxEnt(WSJ)" "MaxEnt(WSJ+NFI)" -bnc 

..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM1.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJM2.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con 2 mitad de WSJ)" "BNC" "MaxEnt(2WSJ)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM2.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJM1.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con 1 mitad de WSJ)" "BNC" "MaxEnt(1WSJ)" -bnc 

..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM1+NFI.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJM2+NFI.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con 2 mitad de WSJ+NFI)" "BNC" "MaxEnt(2WSJ+NFI)" -bnc 
..\..\..\tesis -comparar "..\BNC\BNC.g" "BNC\Taggeado\wsjM2+NFI.tt" "BNC\Mediciones\bnc_VS_bncMaxEntWSJM1+NFI.l" -l "BNC original contra BNC etiquetado con MaxEnt (entrenado con 1 mitad de WSJ+NFI)" "BNC" "MaxEnt(1WSJ+NFI)" -bnc 

..\..\..\tesis -comparar "BNC\Taggeado\wsjM1.tt" "BNC\Taggeado\wsjM1+NFI.tt" "BNC\Mediciones\bncMaxEntWSJM2_vs_bncMaxEntWSJM2+NFI.l" -l "BNC etiquetado por MaxEnt (entrenado con 2 mitad WSJ) vs BNC etiquetado con MaxEnt (entrenado con 2 mitad de WSJ + NFI)" "MaxEnt(2WSJ)" "MaxEnt(2WSJ+NFI)"
..\..\..\tesis -comparar "BNC\Taggeado\wsjM2.tt" "BNC\Taggeado\wsjM2+NFI.tt" "BNC\Mediciones\bncMaxEntWSJM1_vs_bncMaxEntWSJM1+NFI.l" -l "BNC etiquetado por MaxEnt (entrenado con 1 mitad WSJ) vs BNC etiquetado con MaxEnt (entrenado con 1 mitad de WSJ + NFI)" "MaxEnt(1WSJ)" "MaxEnt(1WSJ+NFI)"
