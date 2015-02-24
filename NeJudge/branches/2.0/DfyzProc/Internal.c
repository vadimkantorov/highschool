#include "Internal.h"

void terminate(const char *func_name)
{
	fprintf(stderr, "%s: out of memory", func_name);
	abort();
}

void *ecalloc(size_t num, size_t el_size)
{
	void *ret = calloc(num, el_size);
	if ( ret == NULL )
		terminate("ecalloc");
	return ret;
}

void efree(void *p)
{
	if ( p != NULL )
		free(p);
}

void *estrdup(const char *string)
{
	void *ret = _strdup(string);
	if ( ret == NULL )
		terminate("estrdup");
	return ret;
}

void *erealloc(void *pntr, size_t new_size)
{
	void *ret;
	if ( pntr != NULL && new_size != 0 )
	{
		ret = realloc(pntr, new_size);
		if ( ret == NULL )
			terminate("erealloc");
		return ret;
	}
	return pntr;
}
