#include <cstdio>

int main(int argc, char **argv)
{
	FILE *f = fopen("args.txt", "w");
	for ( int i = 1; i < argc; i++ )
		fprintf(f, "%s\n", argv[i]);
	return 0;
}
