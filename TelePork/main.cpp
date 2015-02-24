/*#include <cstdio>
#include <ctime>
#include <conio.h>

#include "comport.hpp"

void main()
{
	freopen("C:\\out.txt","w",stdout);
	
	int count = 0;
	COMPort com("COM1");
	com.setDtr(true);
	clock_t start = clock();
	while(!_kbhit())
	{
		com.setDtr(false);
		double d = 0;
		for( int i = 0; i <= 7; i++ )
		{
			com.setRts(true);
			bool cts = com.getCts();
			com.setRts(false);
			if( cts )
				d += ( 1 << (7 - i) );
		}
		d = 5.0 * ( d / ( (1 << 8) - 1 ) );
		clock_t end = clock();
		com.setDtr(true);
		double time = 1000*(end-start)/CLOCKS_PER_SEC;
		printf("%.3d %.2lf\n",int(time),d);
		count++;
	}
	double time = double((clock()-start))/CLOCKS_PER_SEC;

	freopen("con","w",stdout);
	printf("For %.15lf seconds %d measurings were made",time,count);
}*/
#ifndef false
#include <ctime>
#include <conio.h>
#include <cstdio>

#include <windows.h>
#include <winioctl.h>

#include <cstddef>
#include "gpioctl.h"
#include "comport.hpp"

const int Begin = 0x3F8;
const int MCR = Begin + 4;
const int MSR = Begin + 6;

HANDLE port;

bool OpenPort()
{
	port = CreateFile(
				"\\\\.\\GpdDev",					// Open the Device "file"
				GENERIC_READ | GENERIC_WRITE,
				0,
				NULL,
				OPEN_EXISTING,
				0,
				NULL
				);

	return port != INVALID_HANDLE_VALUE;
}

UCHAR ReadPort(int relativeAddress)
{
	UCHAR DataBuffer;
	DWORD ReturnedLength;
	
	ULONG DataLength = sizeof(DataBuffer);
	ULONG PortNumber = relativeAddress;
	
	LONG IoctlCode = IOCTL_GPD_READ_PORT_UCHAR;
		
	BOOL IoctlResult = DeviceIoControl(
							port,			// Handle to device
							IoctlCode,		  // IO Control code for Read
							&PortNumber,		// Buffer to driver.
							sizeof(PortNumber), // Length of buffer in bytes.
							&DataBuffer,		// Buffer from driver.
							DataLength,		 // Length of buffer in bytes.
							&ReturnedLength,	// Bytes placed in DataBuffer.
							NULL				// NULL means wait till op. completes.
							);

	if (IoctlResult)							// Did the IOCTL succeed?
	{
		if (ReturnedLength != DataLength)
		{
			printf(
				"Ioctl transfered %d bytes, expected %d bytes\n",
				ReturnedLength, DataLength );
		}
		return DataBuffer;
	}
}

void WritePort(int relativeAddress, UCHAR val)
{
	DWORD ReturnedLength;
	GENPORT_WRITE_INPUT InputBuffer;
	
	InputBuffer.PortNumber = relativeAddress;
	InputBuffer.CharData = val;

	ULONG DataLength =  /*offsetof(GENPORT_WRITE_INPUT, CharData) +
						 sizeof(InputBuffer.CharData);*/
						 sizeof(InputBuffer);
	
	LONG IoctlCode = IOCTL_GPD_WRITE_PORT_UCHAR;
		
	BOOL IoctlResult = DeviceIoControl(
						port,					// Handle to device
						IoctlCode,				// IO Control code for Write
						&InputBuffer,			// Buffer to driver.  Holds port & data.
						DataLength,				// Length of buffer in bytes.
						NULL,					// Buffer from driver.   Not used.
						0,						// Length of buffer in bytes.
						&ReturnedLength,		// Bytes placed in outbuf.  Should be 0.
						NULL					// NULL means wait till I/O completes.
						);

	if (!IoctlResult)							// Did the IOCTL succeed?
	{
		puts("Writing failed");
		exit(1);
	}
}

void SetDtr(bool dtrv)
{
	WritePort(MCR,dtrv);
}

void SetRts(bool rtsv)
{
	WritePort(MCR,rtsv?2:0);
}

void EnableDevice()
{
	//_outp(Begin+3,64);
}

bool GetCts()
{
	return (ReadPort(MSR) & 16) != 0;
}

void main()
{
	freopen("C:\\out.txt","w",stdout);
		
	COMPort com("COM1");
	
	if(!OpenPort())
	{
		puts("Unable to open port");
		exit(1);
	}
	/*bool res = InitializeWinIo();
	if(!res)*/
	
	{
		int count = 0;
		SetDtr(true);
		//EnableDevice();
		clock_t start = clock();
		while(!_kbhit())
		{
			SetDtr(false);
			double d = 0;
			for( int i = 0; i <= 7; i++ )
			{
				SetRts(true);
				bool cts = GetCts();
				SetRts(false);
				if( cts )
					d += ( 1 << (7 - i) );
			}
			d = 5.0 * ( d / ( (1 << 8) - 1 ) );
			clock_t end = clock();
			SetDtr(true);
			double time = 1000*(end-start)/CLOCKS_PER_SEC;
			printf("%.3d %.2lf\n",int(time),d);
			count++;
		}
		double time = double((clock()-start))/CLOCKS_PER_SEC;
		SetDtr(false);
		freopen("con","w",stdout);
		printf("For %.15lf seconds %d measurings were made",time,count);
	}
	
	if (!CloseHandle(port))				  // Close the Device "file".
	{
		puts("Failed to close device.");
		exit(1);
	}
}
#endif