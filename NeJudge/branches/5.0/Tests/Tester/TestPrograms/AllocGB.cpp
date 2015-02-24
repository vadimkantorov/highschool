#include <cstdio>
#include <cstdlib>
#include <ctime>

#include <windows.h>

const int GB = 1024*1024*1024;
char arr[GB];

int main()
{
	srand(time(NULL));
	for (int i = 0; i < 100; i++)
		arr[rand() % GB] = 42;
	for (int i = 0; i < 100; i++)
		;//printf("%d ", rand() % GB);
		
	SIZE_T mn, mx;
	GetProcessWorkingSetSize(GetCurrentProcess(), &mn, &mx);
	printf("%d %d", mn, mx);
}