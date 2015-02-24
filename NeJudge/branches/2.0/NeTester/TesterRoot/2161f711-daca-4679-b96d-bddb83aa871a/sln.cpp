#include <cstdio>
#include <cstring>

struct Digit
{
	char X[6][5];
};

Digit etalon[10];
Digit time[4];
int rt[4],tmp[4];

bool Compare(Digit& x, Digit& e)
{
	for(int i = 0; i < 6; i++)
		for(int j = 0; j < 5; j++)
			if(x.X[i][j] == '#' && e.X[i][j] == '.')
				return false;
	return true;
}

void ScanDigits(Digit* M, int n)
{
	for(int i = 0; i < 6; i++)
	{
		for(int j = 0; j < 5*n; j++)
			M[j/5].X[i][j%5] = getchar();
		getchar();
	}
	getchar();
}

bool CheckTime(int* t)
{
	if ( t[0] > 2 )
		return false;
	if ( t[0] == 2 && t[1] > 3 )
		return false;
	if ( t[2] > 5 )
		return false;
	return true;
}

int main()
{
	//freopen("input.txt","r",stdin);
	
	ScanDigits(etalon,10);
	ScanDigits(time,4);

	for(int i = 0; i < 4; i++)
	{
		int foundInd = -1;
		for(int j = 0; j < 10; j++)
		{
			if(Compare(time[i],etalon[j]))
			{
				if ( foundInd != -1 )
				{
					memcpy(tmp,rt,sizeof(rt));
					tmp[i] = j;
					if(CheckTime(tmp))
					{
						puts("AMBIGUITY");
						return 0;
					}
				}
				else
					foundInd = j;
			}
		}
		if ( foundInd == -1 )
		{
			puts("ERROR");
			return 0;
		}
		else
			rt[i] = foundInd;
	}
	if ( !CheckTime(rt) )
		puts("ERROR");
	else
		printf("%d%d:%d%d",rt[0],rt[1],rt[2],rt[3]);
	return 0;
}