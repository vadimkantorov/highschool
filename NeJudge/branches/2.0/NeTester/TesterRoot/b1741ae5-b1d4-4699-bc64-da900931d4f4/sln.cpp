#include <stdio.h>

char number[10][7][6];
char number1[5][7][6];
int t1, t2, t3, t4;

int xxx(int q, int w)
{
	int re = 1;
	for (int i = 1; i <= 6; ++i)
	{
		for (int j = 1; j <= 5; ++j)
		{
			if (number[q][i][j] == number1[w][i][j])
			{
				re = 1;
			}
			else
			if (number1[w][i][j] == '.')
			{
				re = 1;
			}
			else
			{
				return 0;
			}
		}
	}
	return 1;
}

int main()
{
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w", stdout);
	int k, o;
	int re = 0;
	for (int i = 1; i <= 6; ++i)
	{
		for (int j = 1; j <= 50; ++j)
		{
			if (j % 5 == 0)
			{
				k = j / 5 - 1;
				o = 5;
			}
			else
			{
				k = j / 5;
				o = j % 5;
			}
			number[k][i][o] = getchar();
			if (j == 50)
				getchar();
		}
	}
	getchar();
	for (int i = 1; i <= 6; ++i)
	{
		for (int j = 1; j <= 20; ++j)
		{
			if (j % 5 == 0)
			{
				k = j / 5 - 1;
				o = 5;
			}
			else
			{
				k = j / 5;
				o = j % 5;
			}
			number1[k][i][o] = getchar();
			if (j == 20)
				getchar();
		}
	}
	for (int i = 0; i <= 5; ++i)
	{
		for (int j = 0; j <= 9; ++j)
		{
			for (int p = 0; p <= 2; ++p)
			{
				for (int y = 0; y <= ((p != 2)?9:3); ++y)
				{
					if (xxx(p, 0) == 1 && xxx(y, 1) == 1 && xxx(i, 2) == 1 && xxx(j, 3) == 1)
					{
						if (re == 1)
						{
							printf("AMBIGUITY");
							return 0;
						}
						else
						if (re == 0)
						{
							re = 1;
							t1 = p;
							t2 = y;
							t3 = i;
							t4 = j;
						}
					}
				}
			}
		}
	}
	if (re == 0)
	{
		printf("ERROR");
		return 0;
	}
	else
	{
		printf("%d%d:%d%d", t1, t2, t3, t4);
		return 0;
	}
	return 0;
}