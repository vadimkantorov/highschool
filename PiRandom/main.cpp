#include <cstdio>
#include <cstdlib>
#include <cstring>
#include "pi.cpp"

void main()
{
	printf("Enter digit count (9 must divide it!): ");
	int n;
	scanf("%d",&n);
	if(n%9 != 0)
	{
		puts("Error. 0 doesn't divide digit count");
		return;
	}
	int pin,a=0,b=0;
	
	for(int i = 0; i < n; i+= 9)
	{
		int pin = NineDigitsOfPi::StartingAt(i+1);
		itoa(pin,temp,2);
		for(int i = 0; i < strlen(temp); i++)
		{
			if(temp[i] == '0')
				a++;
			else
				b++;
		}
	}
	printf("a(zeros) = %d, b(ones) = %d\n",a,b);
	if(b != 0)
		printf("%lf",double(a)/double(b));
}