#include <stdio.h>
#define bool int
#define true 1
#define false 0


extern void TomaUnInt8(char param);
extern void TomaUnInt16(short param);
extern void TomaUnInt32(long param);
//extern void TomaUnInt64(double param);
extern void TomaUnIEEE32(float param);
extern void TomaUnIEEE64(double param);
extern void TomaUnBool(bool param);


 
// Esta funcion tiene por objetivo convertir un float a double para ser llamada en ASM.
double floatToDouble(float f){
	double ret;
	ret = f;
	return ret;
}

int main(long argc, char** argv) {
	// >>>BYTE<<<
	char unByte;
	unByte = 'A';
	TomaUnInt8(unByte);

	// >>>WORD<<<
	short unWord;
	unWord = -32767;	//Minimo
	TomaUnInt16(unWord);

	// >>>DWORD<<<
	long unDWord;
	unDWord = -2147483648;	//Minimo
	TomaUnInt32(unDWord);

	// >>>IEEE32<<<
	float unFloat;
	unFloat = -100.123;
	TomaUnIEEE32(unFloat);

	// >>>IEEE64<<<
	double unDouble;
	unDouble = 922337203.123;
	TomaUnIEEE64(unDouble);

	// >>>BOOLEAN<<<
	bool booleano;
	booleano = true;
	TomaUnBool(booleano);

	
	// >>>QWord<<<
	long long int a;
	a = -922337203685477580;
	printf("lonlong %d",sizeof(a));


	
	return 0;

}

