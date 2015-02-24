#ifndef DF_STRARR_HH
#define DF_STRARR_HH

#include "Internal.h"

struct _DfStrArr
{
	char **strs;
	int alloc;
	int used;
};

typedef struct _DfStrArr DfStrArr;

#define DF_ARR_DELTA 16
DfStrArr *df_str_arr_new();
void df_str_arr_append(DfStrArr *arr, const char *string);
void df_str_arr_free(DfStrArr *arr);

#endif // DF_STRARR_HH
