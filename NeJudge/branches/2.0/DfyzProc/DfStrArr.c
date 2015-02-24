#include "DfStrArr.h"

DfStrArr *df_str_arr_new()
{
	DfStrArr *arr = (DfStrArr *)ecalloc(1, sizeof(DfStrArr));
	arr->strs = (char **)ecalloc(DF_ARR_DELTA, sizeof(char *));
	arr->alloc = DF_ARR_DELTA;
	arr->used = 0;
	return arr;
}

void df_str_arr_append(DfStrArr *arr, const char *string)
{
	if ( arr->used + 1 >= arr->alloc )
	{
		arr->alloc += DF_ARR_DELTA;
		arr->strs = erealloc(arr->strs, arr->alloc * sizeof(char *));
	}
	arr->strs[arr->used++] = estrdup(string);
}

void df_str_arr_free(DfStrArr *arr)
{
	int i;
	if ( arr != NULL )
		for ( i = 0; i < arr->used; ++i )
			efree(arr->strs[i]);
	efree(arr->strs);
	efree(arr);
}
