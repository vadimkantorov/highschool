#include "DfyzProc.h"

const char *statuses[] = {
	"Ok",
	"Time limit",
	"Memory limit",
	"Output limit",
	"Security violation",
	"Runtime error",
	"Failure"
};

char buf[200];

int main(int argc, char **argv)
{
	DfProcess *prc;
	DfRunResult rr;
	int i;
	if ( argc != 2 )
	{
		fprintf(stderr, "Usage: test <exe>\n");
		exit(1);
	}
	
	prc = df_new(argv[1], ".");
	prc->limits.time = 1000;
	prc->limits.idleness = 3000;
	for ( i = 0; i < 1000; i++ )
	{
		sprintf(buf, "%d", i);
		df_add_arg(prc, buf);
	}
	df_run(prc, &rr, buf);
	df_free(prc);
	
	printf("Run status: %s\n", statuses[rr.status]);
	printf("Time: %d msec; Memory %d bytes; Output %d bytes\n", rr.time_worked, rr.mem_used, rr.output_size);
	printf("Exit code: %d\n", rr.exit_code);
	printf("Comment: %s\n", buf);
	return 0;
}
