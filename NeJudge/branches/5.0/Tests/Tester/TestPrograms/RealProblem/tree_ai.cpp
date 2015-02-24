#include <stdio.h>
#include <string.h>

const int MN = 100;

char matr[MN][MN+1];
char ans[MN][MN+1];
int n,d,a;
long long cost;
int color[MN];
int was[MN];

typedef long long LL;

inline void outLL (LL x) {
	const int u = x / (int)1E9;
	const int d = x % (int)1E9;
	if (u)
		printf ("%d%09d\n", u, d);
	else
		printf ("%d\n", d);
}
int main()
{
	freopen("input.txt","r",stdin);
	freopen("output.txt","w",stdout);
	scanf("%d%d%d",&n,&d,&a);
	for (int i=0;i<n;i++)
	{
		scanf("%s",matr[i]);
		for (int j=0;j<n;j++)
			if (matr[i][j]=='1')
			{	
				ans[i][j]='d';
				cost+=d;
			}
			else
				ans[i][j]='0';
	}
	cost/=2;
	for (int i=0;i<n;i++)
		color[i]=i;
	for (int i=0;i<n;i++)
		for (int j=0;j<n;j++)
        	if (matr[i][j]=='1' && color[i]!=color[j])
		{
			ans[i][j]=ans[j][i]='0';
			cost-=d;
			int c=color[j];
			for (int k=0;k<n;k++)
			if (color[k]==c)
				color[k]=color[i];
		}
	for (int i=0;i<n;i++)
	{
		if (color[i]!=color[0] && !was[color[i]])
		{
	        	was[color[i]]=1;
			ans[i][0]=ans[0][i]='a';
			cost+=a;
		}
	}
	outLL (cost);
	for (int i=0;i<n;i++)
		puts(ans[i]);
	return 0;
}
