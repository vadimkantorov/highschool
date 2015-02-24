#include<iostream>
using namespace std;
long long way[1100][1100];
char map[1100][1100];
char nmap[1100][1100];
long long used[100000000];
long long  l[1100][1100];
long long  v[1100][1100];
long long n,a,b,k;
long long cnt;
void dfs(long long  a)
{
	long long  i;
	for(i = 0;i < n;i ++)
		if(a != i && way[a][i] == 1 && used[i] == 0)
		{
			used[i] = k;
			l[a][i] = 1;
			l[i][a] = 1;
			dfs(i);
		}

}

int main()
{
    	freopen("input.txt","r",stdin);
	freopen("output.txt","w",stdout);

    long long  i,j;
	
	while(3 == scanf("%llu %llu %llu",&n,&a,&b))
	{
		memset(way,0,sizeof(way));
		memset(nmap,'0',sizeof(nmap));
		memset(map,'0',sizeof(map));
		memset(used,0,sizeof(used));
		memset(l,0,sizeof(l));
		memset(v,0,sizeof(v));
		cnt = 0;
		for(i = 0;i < n;i ++)			
		{
			scanf("%s",map[i]);
			for(j = 0;j < n;j ++)
			   if(map[i][j] == '1')
				   way[i][j] = 1;
		}
		k = 1;
		for(i = 0;i < n;i ++)
			if(used[i] == 0)
			{
				used[i] = k;
				dfs(i);
				k ++;
			}
		int u;
		for(i = 1;i < k - 1;i ++)
		{
			int flag = 0;
			for(j = 0;j < n;j ++)
			{
				for(u = 0;u < n;u ++)
				   if(used[j] == i && used[u] == i + 1)
				   {
					   l[j][u] = 1;
					   l[u][j] = 1;
					   flag = 1;
					   //cnt += b;
					   break;
				   }
			   if(flag)
				   break;
			}
		}
		for(i = 0;i < n;i ++)
			for(j = 0;j < n;j ++)
			{
				if(map[i][j] == '1' && l[i][j] == 0 && v[i][j] == 0)
				{
					nmap[i][j] = 'd';
					nmap[j][i] = 'd';
					cnt += a;
					v[i][j] = 1;
					v[j][i] = 1;
				}
				else if(map[i][j] == '0' && l[i][j] == 1 && v[i][j] == 0)
				{
					nmap[i][j] = 'a';
					nmap[j][i] = 'a';
					cnt += b;
					v[i][j] = 1;
					v[j][i] = 1;
				}					
			}
		printf("%llu\n",cnt);
		for(i = 0;i < n;i ++)
		{
			for(j = 0;j < n;j ++)
			   printf("%c",nmap[i][j]);
			printf("\n");
		}
	}
	return 0;
}
