#ifndef LOGGING_HH
#define LOGGING_HH

#undef UNICODE
#include <windows.h>

#include "Internal.h"

#define LOGGING_ENABLED 1

void log_message(const char *message);

#endif
