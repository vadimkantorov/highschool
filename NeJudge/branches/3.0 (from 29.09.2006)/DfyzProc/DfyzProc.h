#ifndef DFYZPROC_HH
#define DFYZPROC_HH

#undef UNICODE

#include <windows.h>
#include <psapi.h>

#include "DfStrArr.h"

#define DF_COMMENT_LEN 512

typedef enum
{
	RUN_STATUS_OK = 0,
	RUN_STATUS_TIME_LIMIT = 1,
	RUN_STATUS_MEMORY_LIMIT = 2,
	RUN_STATUS_OUTPUT_LIMIT = 3,
	RUN_STATUS_SECURITY_VIOLATION = 4,
	RUN_STATUS_RUNTIME_ERROR = 5,
	RUN_STATUS_FAILURE = 6
} DfRunStatus;

struct _DfRunResult
{
	int time_worked;
	int mem_used;
	int output_size;

	DfRunStatus status;
	unsigned long int exit_code;
};

typedef struct _DfRunResult DfRunResult;

struct _DfLimits
{
	int time;
	int memory;
	int output;
	int idleness;
};

typedef struct _DfLimits DfLimits;

struct _DfRedirections
{
	char *std_in;
	char *std_out;
	char *std_err;
	int dup_out_to_err;
};

typedef struct _DfRedirections DfRedirections;

struct _DfRunAsParams
{
	char *username;
	char *password;
};

typedef struct _DfRunAsParams DfRunAsParams;

struct _DfProcess
{
	char *exe_name;
	char *work_dir;
	DfStrArr *args;

	DfLimits limits;
	DfRedirections redirs;
	DfRunAsParams run_as;

	int allow_process_creation;
};

typedef struct _DfProcess DfProcess;

/*DfProcess * df_new(const char *exe_name, const char *work_dir);
void df_free(DfProcess *prc);
void df_add_arg(DfProcess *prc, const char *arg);
void df_run(DfProcess *prc, DfRunResult *res);*/

__declspec(dllexport) DfProcess * df_new(const char *exe_name, const char *work_dir);
__declspec(dllexport) void df_free(DfProcess *prc);
__declspec(dllexport) void df_add_arg(DfProcess *prc, const char *arg);
__declspec(dllexport) void df_run(DfProcess *prc, DfRunResult *res, char *comment);

#endif // DFYZPROC_HH
