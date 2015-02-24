#include "Logging.h"
#include "DfyzProc.h"

void format_comment(char *buffer, const char *msg, int error)
{
	void *msg_buf;
	if ( error != -1 )
	{
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, NULL, error, 0, 
			(LPTSTR) &msg_buf, 0, NULL);
		_snprintf(buffer, DF_COMMENT_LEN, "%s: %s", msg, msg_buf);
		LocalFree(msg_buf);
	}
	else
		_snprintf(buffer, DF_COMMENT_LEN, "%s", msg);
	buffer[DF_COMMENT_LEN - 1] = '\0';
}

void format_crash_reason(char *buffer, unsigned int crash_code)
{
	char *s;
	switch (crash_code)
	{
	case EXCEPTION_ACCESS_VIOLATION:
		s = "Access violation";
		break;
	case EXCEPTION_ARRAY_BOUNDS_EXCEEDED:
		s = "Array bounds exceeded";
		break;
	case EXCEPTION_DATATYPE_MISALIGNMENT:
		s = "Datatype misaligment";
		break;
	case EXCEPTION_FLT_DENORMAL_OPERAND:
		s = "Too small operand in floating point expression";
		break;
	case EXCEPTION_FLT_DIVIDE_BY_ZERO:
		s = "Division by zero";
		break;
	case EXCEPTION_FLT_INEXACT_RESULT:
		s = "Inexact result in floating point expression";
		break;
	case EXCEPTION_FLT_INVALID_OPERATION:
		s = "Invalid floating point operation";
		break;
	case EXCEPTION_FLT_OVERFLOW:
		s = "Floating point overflow";
		break;
	case EXCEPTION_FLT_STACK_CHECK:
		s = "Stack over/underflow in floating point expression";
		break;
	case EXCEPTION_FLT_UNDERFLOW:
		s = "Floating point underflow";
		break;
	case EXCEPTION_ILLEGAL_INSTRUCTION:
		s = "Illegal processor instruction";
		break;
	case EXCEPTION_INT_DIVIDE_BY_ZERO:
		s = "Integer division by zero";
		break;
	case EXCEPTION_INT_OVERFLOW:
		s = "Integer overflow";
		break;
	case EXCEPTION_PRIV_INSTRUCTION:
		s = "Processor operation is not allowed";
		break;
	case EXCEPTION_STACK_OVERFLOW:
		s = "Stack overflow";
		break;
	default:
		s = "Unknown reason";
		break;
	}
	format_comment(buffer, s, -1);
}

char *build_exe_name(char *exe_name)
{
	char *res, *src, *dst;
	int len = strlen(exe_name) * 2 + 3;

	src = exe_name;
	dst = ecalloc(len, sizeof(char));
	res = dst;

	while ( *src )
	{
		if ( *src == '\"' )
			*dst++ = '\\';
		*dst++ = *src++;
	}

	*dst++ = '\0';
	return res;
}

char *build_cmd_line(char *first_arg, DfStrArr *arr)
{
	int i;
	size_t total = strlen(first_arg) * 2 + 3;

	char *res, *src, *dst;

	if ( arr != NULL )
		for ( i = 0; i < arr->used; i++ )
			total += strlen(arr->strs[i]) * 2 + 3;
	total++;

	dst = ecalloc(total, sizeof(char));
	res = dst;
	src = first_arg;
	*dst++ = '\"';
	while ( *src )
	{
		if ( *src == '\"' || *src == '\\' )
			*dst++ = '\\';
		*dst++ = *src++;
	}
	*dst++ = '\"';
	if ( arr != NULL )
	{
		for ( i = 0; i < arr->used; i++ )
		{
			src = arr->strs[i];
			*dst++ = ' ';
			//*dst++ = '\"';
			while ( *src )
			{
				if ( *src == '\"' /*|| *src == '\\'*/ )
					*dst++ = '\\';
				*dst++ = *src++;
			}
			//*dst++ = '\"';
		}
	}
	*dst = '\0';
	return res;
}

void get_process_info(HANDLE prc, int *time_worked, int *time_passed, 
					  int *mem_used, int *output_size)
{
	FILETIME exit;
	union
	{
		FILETIME ft;
		__int64 ticks;
	} user, kernel, start, system;
	SYSTEMTIME sys_time;
	PROCESS_MEMORY_COUNTERS mc;
	IO_COUNTERS ioc;

	GetProcessTimes(prc, &start.ft, &exit, &kernel.ft, &user.ft);
	GetProcessMemoryInfo(prc, &mc, sizeof(mc));
	GetProcessIoCounters(prc, &ioc);

	GetSystemTime(&sys_time);
	SystemTimeToFileTime(&sys_time, &system.ft);

	if ( time_worked != NULL )
		*time_worked = (int)((kernel.ticks + user.ticks) / 10000);
	if ( time_passed != NULL )
		*time_passed = (int)((system.ticks - start.ticks) / 10000);
	if ( mem_used != NULL )
		*mem_used = (int)mc.PeakWorkingSetSize;
	if ( output_size != NULL )
		*output_size = (int)ioc.WriteTransferCount;
}

__declspec(dllexport) DfProcess * df_new(const char *exe_name, const char *work_dir)
{
	DfProcess *prc = ecalloc(1, sizeof(DfProcess));
	if ( exe_name == NULL || work_dir == NULL )
		return NULL;
	prc->exe_name = estrdup(exe_name);
	prc->work_dir = estrdup(work_dir);
	prc->args = df_str_arr_new();
	return prc;
}

__declspec(dllexport) void df_free(DfProcess *prc)
{
	df_str_arr_free(prc->args);
	efree(prc);
}

__declspec(dllexport) void df_add_arg(DfProcess *prc, const char *arg)
{
	df_str_arr_append(prc->args, arg);
}

__declspec(dllexport) void df_run(DfProcess *prc, DfRunResult *res, char *comment)
{
	STARTUPINFO sinfo;
	PROCESS_INFORMATION pinfo;
	SECURITY_ATTRIBUTES sa = { sizeof(SECURITY_ATTRIBUTES), NULL, TRUE };
	char *cline, *exe;
	HANDLE hin, hout, herr, huser;
	DEBUG_EVENT de;
	int create_ok;
	int time_passed = 0, time_worked = 0, memory_used = 0, output_size = 0, idle_period = 0;
	int old_time_passed = 0, old_time_worked = 0;
	const int interval = 100;
	int terminated = 0, termination_needed = 0;
	int proc_cnt = 0;
	char msg_buf[300];
	int last_error = -1;

	if ( prc->exe_name == NULL || prc->work_dir == NULL || res == NULL )
	{
		format_comment(comment, "One of the required parameters is NULL", -1);
		return;
	}
	if ( prc->limits.time < 0 || prc->limits.memory < 0 || prc->limits.output < 0 ||
		prc->limits.idleness < 0 )
	{
		format_comment(comment, "One of the limit values is invalid", -1);
		return;
	}

	memset(&sinfo, 0, sizeof(sinfo));
	memset(&pinfo, 0, sizeof(pinfo));
	res->status = RUN_STATUS_FAILURE;

	sinfo.dwFlags = STARTF_USESTDHANDLES | STARTF_USESHOWWINDOW;
	sinfo.wShowWindow = SW_HIDE;
	sinfo.lpDesktop = "";

	log_message("Redirecting standard streams");

	hin = GetStdHandle(STD_INPUT_HANDLE);
	hout = GetStdHandle(STD_OUTPUT_HANDLE);
	herr = GetStdHandle(STD_ERROR_HANDLE);

	if ( prc->redirs.std_in != NULL )
	{
		_snprintf(msg_buf, sizeof(msg_buf), "Standard input: %s", prc->redirs.std_in);
		log_message(msg_buf);
		hin = CreateFile(prc->redirs.std_in, GENERIC_READ, 0, &sa, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
		if ( hin == INVALID_HANDLE_VALUE )
		{
			format_comment(comment, "Cannot redirect standard input", GetLastError());
			return;
		}
	}
	if ( prc->redirs.std_out != NULL )
	{
		_snprintf(msg_buf, sizeof(msg_buf), "Standard output: %s", prc->redirs.std_out);
		log_message(msg_buf);
		hout = CreateFile(prc->redirs.std_out, GENERIC_WRITE, FILE_SHARE_WRITE, &sa, CREATE_ALWAYS,
			FILE_ATTRIBUTE_NORMAL, NULL);
		if ( hout == INVALID_HANDLE_VALUE )
		{
			format_comment(comment, "Cannot redirect standard output", GetLastError());
			return;
		}
		if ( prc->redirs.dup_out_to_err )
			herr = hout;
	}
	if ( !prc->redirs.dup_out_to_err && prc->redirs.std_err != NULL )
	{
		_snprintf(msg_buf, sizeof(msg_buf),"Standard error: %s", prc->redirs.std_err);
		log_message(msg_buf);
		herr = CreateFile(prc->redirs.std_err, GENERIC_WRITE, FILE_SHARE_WRITE, &sa, CREATE_ALWAYS,
			FILE_ATTRIBUTE_NORMAL, NULL);
		if ( herr == INVALID_HANDLE_VALUE )
		{
			format_comment(comment, "Cannot redirect standard error", GetLastError());
			return;
		}
	}

	sinfo.hStdInput = hin;
	sinfo.hStdOutput = hout;
	sinfo.hStdError = herr;

	log_message("Building command line for process");

	exe =  build_exe_name(prc->exe_name);
	cline = build_cmd_line(prc->exe_name, prc->args);

	create_ok = 0;
	if ( prc->run_as.username == NULL || prc->run_as.password == NULL )
	{
		log_message("Starting process with CreateProcess()");
		create_ok = CreateProcess(exe, cline, NULL, NULL, TRUE, DEBUG_PROCESS | CREATE_SUSPENDED, NULL, prc->work_dir, 
			&sinfo, &pinfo);
		if ( !create_ok )
			last_error = GetLastError();
	}
	else
	{
		log_message("Starting process with CreateProcessAsUser()");
		create_ok = LogonUser(prc->run_as.username, ".", prc->run_as.password, LOGON32_LOGON_BATCH, LOGON32_PROVIDER_DEFAULT,
			&huser);
		if ( create_ok )
		{
			_snprintf(msg_buf, sizeof(msg_buf), "Login: %s\nPassword: %s", prc->run_as.username, prc->run_as.password);
			log_message(msg_buf);
			create_ok = CreateProcessAsUser(huser, exe, cline, NULL, NULL, TRUE, DEBUG_PROCESS | CREATE_SUSPENDED, NULL, 
				prc->work_dir, &sinfo, &pinfo);
			if ( !create_ok )
				last_error = GetLastError();
			CloseHandle(huser);
		}
		else
			last_error = GetLastError();
	}

	efree(cline);
	efree(exe);
	if ( !create_ok )
	{
		format_comment(comment, "Process creation failed", last_error);
		return;
	}

	res->status = RUN_STATUS_OK;
	ResumeThread(pinfo.hThread);

	log_message("Entering main loop");

	while ( !terminated )
	{
		if ( WaitForDebugEvent(&de, interval) )
		{
			switch ( de.dwDebugEventCode )
			{
			case CREATE_THREAD_DEBUG_EVENT:
				CloseHandle(de.u.CreateThread.hThread);
				break;

			case LOAD_DLL_DEBUG_EVENT:
				CloseHandle(de.u.LoadDll.hFile);
				break;

			case EXCEPTION_DEBUG_EVENT:
				if ( de.u.Exception.ExceptionRecord.ExceptionCode != EXCEPTION_BREAKPOINT &&
					de.u.Exception.ExceptionRecord.ExceptionCode != STATUS_SEGMENT_NOTIFICATION &&
					de.u.Exception.ExceptionRecord.ExceptionCode != STATUS_INVALID_HANDLE )
				{
					res->status = RUN_STATUS_RUNTIME_ERROR;
					format_crash_reason(comment, de.u.Exception.ExceptionRecord.ExceptionCode);
					termination_needed = 1;
				}
				break;

			case CREATE_PROCESS_DEBUG_EVENT:
				CloseHandle(de.u.CreateProcessInfo.hFile);
				proc_cnt++;
				if ( proc_cnt > 1 && !prc->allow_process_creation )
				{
					termination_needed = 1;
					res->status = RUN_STATUS_SECURITY_VIOLATION;
					format_comment(comment, "Child process was created", -1);
				}
				break;

			case EXIT_PROCESS_DEBUG_EVENT:
				proc_cnt--;
				if ( proc_cnt <= 0 )
					terminated = 1;
				break;
			}
			ContinueDebugEvent(de.dwProcessId, de.dwThreadId, DBG_CONTINUE);
		}

		old_time_passed = time_passed;
		old_time_worked = time_worked;

		get_process_info(pinfo.hProcess, &time_worked, &time_passed, &memory_used, &output_size);

		if ( prc->limits.idleness > 0 && old_time_worked == time_worked )
		{
			idle_period += time_passed - old_time_passed;
			if ( idle_period > prc->limits.idleness )
			{
				res->status = RUN_STATUS_SECURITY_VIOLATION;
				format_comment(comment, "Process became idle", -1);
				termination_needed = 1;
			}
		}

		if ( prc->limits.time > 0 && time_worked > prc->limits.time )
		{
			res->status = RUN_STATUS_TIME_LIMIT;
			termination_needed = 1;
		}
		else if ( prc->limits.memory > 0 && memory_used > prc->limits.memory )
		{
			res->status = RUN_STATUS_MEMORY_LIMIT;
			termination_needed = 1;
		}
		else if ( prc->limits.output > 0 && output_size > prc->limits.output )
		{
			res->status = RUN_STATUS_OUTPUT_LIMIT;
			termination_needed = 1;
		}

		if ( termination_needed )
			TerminateProcess(pinfo.hProcess, 0);
	}

	log_message("Exiting main loop");

	WaitForSingleObject(pinfo.hProcess, INFINITE);

	get_process_info(pinfo.hProcess, &res->time_worked, NULL, &res->mem_used, &res->output_size);
	GetExitCodeProcess(pinfo.hProcess, &res->exit_code);

	CloseHandle(pinfo.hProcess);
	CloseHandle(pinfo.hThread);

	if ( hin != STD_INPUT_HANDLE && hin != INVALID_HANDLE_VALUE )
		CloseHandle(hin);
	if ( hout != STD_OUTPUT_HANDLE && hout != INVALID_HANDLE_VALUE )
		CloseHandle(hout);
    if ( !prc->redirs.dup_out_to_err && herr != STD_ERROR_HANDLE && herr != INVALID_HANDLE_VALUE )
		CloseHandle(herr);
}
