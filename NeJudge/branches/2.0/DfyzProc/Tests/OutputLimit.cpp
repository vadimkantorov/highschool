#include <cstdio>

int main()
{
	FILE *f = fopen("big-file.txt", "w");
	for ( int i = 0; i < 1000 * 1000; i++ )
		fprintf(f, "f");
	return 0;
}
