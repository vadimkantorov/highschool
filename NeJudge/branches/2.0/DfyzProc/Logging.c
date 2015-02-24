#include "Logging.h"

void log_message(const char *message)
{
#if LOGGING_ENABLED
	printf("[INFO](%d): %s\n", GetCurrentThreadId(), message);
#endif
}
