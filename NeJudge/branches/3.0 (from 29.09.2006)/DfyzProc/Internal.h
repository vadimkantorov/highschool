#ifndef INTERNAL_HH
#define INTERNAL_HH

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void terminate(const char *func_name);
void *ecalloc(size_t num, size_t el_size);
void *erealloc(void *pntr, size_t new_size);
void efree(void *p);
void *estrdup(const char *string);

#endif // INTERNAL_HH
