#include "testlib.h"

const int MN = 100;

std::string matr[MN];
std::string pmatr[MN];
int label[MN];
int n,d,a;

typedef long long LL;

void dfs(int k)
{
	for (int i=0;i<n;i++)
		if (matr[k][i]=='1' && !label[i])
		{
			label[i]=1;
			dfs(i);
		}
}
int main(int argc, char * argv[])
{
	registerTestlibCmd(argc, argv);
	n=inf.readInt();
	d=inf.readInt();
	a=inf.readInt();
	for (int i=0;i<n;i++)
		matr[i] = inf.readWord();
	LL jans = ans.readLong();
	LL pans = ouf.readLong();
	for (int i=0;i<n;i++)
	{
		pmatr[i] = ouf.readWord();
		if (pmatr[i].length()!=n)
			quit(_pe,"Length of output line != n");
		for (int j=0;j<n;j++)
		{
			if (pmatr[i][j]!='0' && pmatr[i][j]!='d' && pmatr[i][j]!='a')
				quit(_pe,"Output must contain characters '0', 'd' and 'a' only");
			if (pmatr[i][j]=='d' && matr[i][j]=='0')
				quit(_wa,"You can't delete edge that doesn't exist");
			if (pmatr[i][j]=='a' && matr[i][j]=='1')
				quit(_wa,"You can't add edge that already exists");
		}
	}

	LL countA=0, countD=0, countOriginal=0;
	for (int i=0;i<n;i++)
		for (int j=i;j<n;j++)
		{
			if (pmatr[i][j]!=pmatr[j][i])
				quit(_pe,"Output matrix is not symmetric");
			if (pmatr[i][j]=='a')
				countA++;
			if (pmatr[i][j]=='d')
				countD++;
			if (matr[i][j]=='1')
				countOriginal++;
		}
	if (countA*a+countD*d != pans)
		quit(_wa,"Wrong total cost");
	
	for (int i=0;i<n;i++)
		for (int j=0;j<n;j++)
		{
			if (pmatr[i][j]=='a')
				matr[i][j]='1';
			if (pmatr[i][j]=='d')
				matr[i][j]='0';
		}
	countOriginal += countA-countD;
	if (countOriginal != n-1)
		quit(_wa,"Resulting graph has more than n-1 edges");

	label[0]=1;
	dfs(0);
	for (int i=0;i<n;i++)
		if (!label[i])
			quit(_wa,"Resulting graph is not connected");

	if (jans<pans)
		quit(_wa,"Non-optimal asnwer");
	else
	if (jans==pans)
		quit(_ok,"All OK!");
	else
		quit(_fail,"ACHTUNG! Jury has non-optimal solution!");
	return 0;
}
