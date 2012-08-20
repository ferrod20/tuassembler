..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj.tt" "WSJ\Mediciones\wsj_VS_wsjTnTWSJ.l" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ)" "WSJ" "TnT(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsj_VS_wsjTnTWSJ+NFI.l" -l "WSJ original contra WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "WSJ" "TnT(WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsj.tt" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsjTnTwsj_VS_wsjTnTwsj+NFI.l" -l "WSJ etiquetado con TnT (entrenado con WSJ) vs WSJ etiquetado con TnT (entrenado con WSJ + NFI)" "TnT(WSJ)" "TnT(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1.tt" "WSJ\Mediciones\wsjM1_VS_TnTwsjM2.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ)" "WSJ" "TnT(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2.tt" "WSJ\Mediciones\wsjM2_VS_TnTwsjM1.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1_VS_TnTwsjM2+NFI.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2_VS_TnTwsjM1+NFI.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ)" "WSJ" "TnT(1WSJ+NFI)"

..\..\..\tesis -comparar "WSJ\Taggeado\wsjM1.tt" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1__TnTwsjM2_vs_TnTwsjM2+NFI.l" -l "1 mitad WSJ etiquetado por TnT (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con TnT (entrenado con 2 mitad de WSJ + NFI)" "TnT(2WSJ)" "TnT(2WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsjM2.tt" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2__TnTwsjM1_vs_TnTwsjM1+NFI.l" -l "2 mitad WSJ etiquetado por TnT (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con TnT (entrenado con 1 mitad de WSJ + NFI)" "TnT(1WSJ)" "TnT(1WSJ+NFI)"
