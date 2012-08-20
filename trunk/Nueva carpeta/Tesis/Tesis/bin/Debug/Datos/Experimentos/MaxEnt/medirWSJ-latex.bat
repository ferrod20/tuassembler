..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj.tt" "WSJ\Mediciones\wsj_VS_wsjMaxEntWsj.l" -l "WSJ original contra WSJ etiquetado con MaxEnt (entrenado con WSJ)" "WSJ" "MaxEnt(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsj_VS_wsjMaxEntWsj+NFI.l" -l "WSJ original contra WSJ etiquetado con MaxEnt (entrenado con WSJ + NFI)" "WSJ" "MaxEnt(WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsj.tt" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsjMaxEntWsj_VS_wsjMaxEntWsj+NFI.l" -l "WSJ etiquetado con MaxEnt (entrenado con WSJ) vs WSJ etiquetado con MaxEnt (entrenado con WSJ + NFI)" "MaxEnt(WSJ)" "MaxEnt(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1.tt" "WSJ\Mediciones\wsjM1_VS_MaxEntWsjM2.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ)" "WSJ" "MaxEnt(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2.tt" "WSJ\Mediciones\wsjM2_VS_MaxEntWsjM1.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ)" "WSJ" "MaxEnt(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1_VS_MaxEntWsjM2+NFI.l" -l "1 mitad WSJ original contra 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "MaxEnt(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2_VS_MaxEntWsjM1+NFI.l" -l "2 mitad WSJ original contra 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ)" "WSJ" "MaxEnt(1WSJ+NFI)"

..\..\..\tesis -comparar "WSJ\Taggeado\wsjM1.tt" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1__MaxEntWsjM2_vs_MaxEntWsjM2+NFI.l" -l "1 mitad WSJ etiquetado por MaxEnt (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ + NFI)" "MaxEnt(2WSJ)" "MaxEnt(2WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsjM2.tt" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2__MaxEntWsjM1_vs_MaxEntWsjM1+NFI.l" -l "2 mitad WSJ etiquetado por MaxEnt (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ + NFI)" "MaxEnt(1WSJ)" "MaxEnt(1WSJ+NFI)"


..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj.tt" "WSJ\Mediciones\wsj_VS_wsjMaxEntWsj.mc" -p "WSJ original contra WSJ etiquetado con MaxEnt (entrenado con WSJ)" "WSJ" "MaxEnt(WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsj.g" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsj_VS_wsjMaxEntWsj+NFI.mc" -p "WSJ original contra WSJ etiquetado con MaxEnt (entrenado con WSJ + NFI)" "WSJ" "MaxEnt(WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsj.tt" "WSJ\Taggeado\wsj+NFI.tt" "WSJ\Mediciones\wsjMaxEntWsj_VS_wsjMaxEntWsj+NFI.mc" -p "WSJ etiquetado con MaxEnt (entrenado con WSJ) vs WSJ etiquetado con MaxEnt (entrenado con WSJ + NFI)" "MaxEnt(WSJ)" "MaxEnt(WSJ+NFI)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1.tt" "WSJ\Mediciones\wsjM1_VS_MaxEntWsjM2.mc" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ)" "WSJ" "MaxEnt(2WSJ)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2.tt" "WSJ\Mediciones\wsjM2_VS_MaxEntWsjM1.mc" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ)" "WSJ" "MaxEnt(1WSJ)"

..\..\..\tesis -comparar "..\WSJ\wsjM1.g" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1_VS_MaxEntWsjM2+NFI.mc" -p "1 mitad WSJ original contra 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ + NFI)" "WSJ" "MaxEnt(2WSJ+NFI)"
..\..\..\tesis -comparar "..\WSJ\wsjM2.g" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2_VS_MaxEntWsjM1+NFI.mc" -p "2 mitad WSJ original contra 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ)" "WSJ" "MaxEnt(1WSJ+NFI)"

..\..\..\tesis -comparar "WSJ\Taggeado\wsjM1.tt" "WSJ\Taggeado\wsjM1+NFI.tt" "WSJ\Mediciones\wsjM1__MaxEntWsjM2_vs_MaxEntWsjM2+NFI.mc" -p "1 mitad WSJ etiquetado por MaxEnt (entrenado con 2 mitad WSJ) vs 1 mitad WSJ etiquetado con MaxEnt (entrenado con 2 mitad de WSJ + NFI)" "MaxEnt(2WSJ)" "MaxEnt(2WSJ+NFI)"
..\..\..\tesis -comparar "WSJ\Taggeado\wsjM2.tt" "WSJ\Taggeado\wsjM2+NFI.tt" "WSJ\Mediciones\wsjM2__MaxEntWsjM1_vs_MaxEntWsjM1+NFI.mc" -p "2 mitad WSJ etiquetado por MaxEnt (entrenado con 1 mitad WSJ) vs 2 mitad WSJ etiquetado con MaxEnt (entrenado con 1 mitad de WSJ + NFI)" "MaxEnt(1WSJ)" "MaxEnt(1WSJ+NFI)"

