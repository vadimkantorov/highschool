#include <cstdio>

int main()
{
	int d;
	while ( scanf("%d", &d) == 1 )
	{
		printf("%d\n", d + 1);
		fprintf(stderr, "%d\n", d + 2);
	}
	return 0;
}
