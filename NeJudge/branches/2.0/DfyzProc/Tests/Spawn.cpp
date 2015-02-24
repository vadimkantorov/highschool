#include <windows.h>
#include <cstring>

int main()
{
	STARTUPINFO sinfo;
	PROCESS_INFORMATION pinfo;
	memset(&sinfo, 0, sizeof(sinfo));
	memset(&pinfo, 0, sizeof(pinfo));
	CreateProcess(NULL, "notepad.exe", NULL, NULL, FALSE, 0, NULL, ".", &sinfo, &pinfo);
	WaitForSingleObject(pinfo.hProcess, INFINITE);
	CloseHandle(pinfo.hProcess);
	CloseHandle(pinfo.hThread);
	return 0;
}
