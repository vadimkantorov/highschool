#include <string>
#include <vector>
#include <iostream>
#include <sstream>
#include <fstream>
#include <set>
#include <map>
#include <vector>
#include <bitset>
#include <algorithm>
#include <functional>
#include <cstdio>
#include <cstdlib>
#include <cmath>
#include <ctime>
#include <utility>
 
using namespace std;
 
typedef long long int64;
typedef unsigned long long uint64;
#define two(X) (1<<(X))
#define twoL(X) (((int64)(1))<<(X))
#define contain(S,X) ((S&two(X))>0)
#define containL(S,X) ((S&twoL(X))>0)
const double pi=acos(-1.0);
const double eps=1e-11;
template<class T> void checkmin(T &a,T b){if (b<a) a=b;}
template<class T> void checkmax(T &a,T b){if (b>a) a=b;}
template<class T> T sqr(T x) {return x*x;}
int countbit(int n) {return (n==0)?0:(1+countbit(n&(n-1)));}
int lowbit(int n) {return (n^(n-1))&n;}
typedef pair<int,int> ipair;
template<class T> void out(T A[],int n)
{
  for (int i=0;i<n;i++) cout<<A[i]<<" ";
  cout<<endl;
}
template<class T> void out(vector<T> A,int n=-1)
{
  if (n==-1) n=A.size();
  for (int i=0;i<n;i++) cout<<A[i]<<" ";
  cout<<endl;
}
template<class T> T gcd(T a,T b)
{
  if (a<0) return gcd(-a,b);
  if (b<0) return gcd(a,-b);
  return (b==0)?a:gcd(b,a%b);
}
template<class T> T lcm(T a,T b)
{
  return a*(b/gcd(a,b));
}
int64 toInt64(string s)
{
  istringstream sin(s);
  int64 t;
  sin>>t;
  return t;
}
int64 toInt(string s)
{
  istringstream sin(s);
  int t;
  sin>>t;
  return t;
}
string toString(int64 v)
{
  ostringstream sout;
  sout<<v;
  return sout.str();
}
string toString(int v)
{
  ostringstream sout;
  sout<<v;
  return sout.str();
}
 
const int maxn=30;
 
 
class ReliefMeasuring 
{
public:
  int n,m;
  bool A[maxn][maxn];
  int S[maxn][maxn];
  int Ox,Oy;
  int f[maxn][maxn][maxn];
  int ID(int x,int y)
  {
    return x*m+y;
  }  
  int getS(int x,int y)
  {
    return S[x][y];
  }
  int getS(int x1,int x2,int y1,int y2,int c)
  {
    int total=S[x2][y2]+S[x1-1][y1-1]-S[x1-1][y2]-S[x2][y1-1];
    if (c==0) return total;
    else return (x2-x1+1)*(y2-y1+1)-total;
  }
  void checkbetter(int &a,int b)
  {
    if (a==-1 || b<a) a=b;
  }
  int minValuesToFix(vector <string> heights) 
  {
    n=heights.size();
    m=heights[0].length();
    for (int i=1;i<=n;i++) for (int j=1;j<=m;j++) A[i][j]=(heights[i-1][j-1]=='1');
    memset(S,0,sizeof(S));
    for (int i=1;i<=n;i++) for (int j=1;j<=m;j++) S[i][j]=S[i-1][j]+S[i][j-1]-S[i-1][j-1]+(int)(A[i][j]);
    int result=10000000;
    checkmin(result,getS(1,n,1,m,1));
    checkmin(result,getS(1,n,1,m,0));
    for (Ox=1;Ox<=n;Ox++) for (Oy=1;Oy<=m;Oy++) 
    {
      memset(f,255,sizeof(f));
      for (int k=1;k<=n;k++)
      {
        if (k<=Ox) 
          for (int i=1;i<=Oy;i++) for (int j=Oy;j<=m;j++)
            checkbetter(f[k][i][j],getS(1,k-1,1,m,0)+getS(k,k,1,i-1,0)+getS(k,k,j+1,m,0)+getS(k,k,i,j,1));
//        if (Ox+Oy==2 && k==2)
  //        printf("ORZ %d\n",f[k][1][1]);
        for (int i=1;i<=Oy;i++) for (int j=Oy;j<=m;j++) if (f[k-1][i][j]!=-1)
          checkbetter(f[k][i][j],f[k-1][i][j]+getS(k,k,1,i-1,0)+getS(k,k,j+1,m,0)+getS(k,k,i,j,1));
    //    if (Ox+Oy==2 && k==2)
      //    printf("ORZ %d\n",f[k][1][1]);
        for (int i=1;i<=Oy;i++) for (int j=Oy;j<=m;j++) if (f[k][i][j]!=-1)
        {
          if (k<=Ox && i-1>=1)  checkbetter(f[k][i-1][j],f[k][i][j]-getS(k,k,i-1,i-1,0)+getS(k,k,i-1,i-1,1));
          if (k>Ox && i+1<=Oy)    checkbetter(f[k][i+1][j],f[k][i][j]-getS(k,k,i,i,1)+getS(k,k,i,i,0));
          if (k<=Ox && j+1<=m)   checkbetter(f[k][i][j+1],f[k][i][j]-getS(k,k,j+1,j+1,0)+getS(k,k,j+1,j+1,1));
          if (k>Ox && j-1>=Oy)   checkbetter(f[k][i][j-1],f[k][i][j]-getS(k,k,j,j,1)+getS(k,k,j,j,0));
        }        
        for (int i=Oy;i>=1;i--) for (int j=Oy;j<=m;j++) if (f[k][i][j]!=-1)
        {
          if (k<=Ox && i-1>=1)  checkbetter(f[k][i-1][j],f[k][i][j]-getS(k,k,i-1,i-1,0)+getS(k,k,i-1,i-1,1));
          if (k>Ox && i+1<=Oy)    checkbetter(f[k][i+1][j],f[k][i][j]-getS(k,k,i,i,1)+getS(k,k,i,i,0));
          if (k<=Ox && j+1<=m)   checkbetter(f[k][i][j+1],f[k][i][j]-getS(k,k,j+1,j+1,0)+getS(k,k,j+1,j+1,1));
          if (k>Ox && j-1>=Oy)   checkbetter(f[k][i][j-1],f[k][i][j]-getS(k,k,j,j,1)+getS(k,k,j,j,0));
        }        
        for (int i=1;i<=Oy;i++) for (int j=m;j>=Oy;j--) if (f[k][i][j]!=-1)
        {
          if (k<=Ox && i-1>=1)  checkbetter(f[k][i-1][j],f[k][i][j]-getS(k,k,i-1,i-1,0)+getS(k,k,i-1,i-1,1));
          if (k>Ox && i+1<=Oy)    checkbetter(f[k][i+1][j],f[k][i][j]-getS(k,k,i,i,1)+getS(k,k,i,i,0));
          if (k<=Ox && j+1<=m)   checkbetter(f[k][i][j+1],f[k][i][j]-getS(k,k,j+1,j+1,0)+getS(k,k,j+1,j+1,1));
          if (k>Ox && j-1>=Oy)   checkbetter(f[k][i][j-1],f[k][i][j]-getS(k,k,j,j,1)+getS(k,k,j,j,0));
        }        
        for (int i=Oy;i>=1;i--) for (int j=m;j>=Oy;j--) if (f[k][i][j]!=-1)
        {
          if (k<=Ox && i-1>=1)  checkbetter(f[k][i-1][j],f[k][i][j]-getS(k,k,i-1,i-1,0)+getS(k,k,i-1,i-1,1));
          if (k>Ox && i+1<=Oy)    checkbetter(f[k][i+1][j],f[k][i][j]-getS(k,k,i,i,1)+getS(k,k,i,i,0));
          if (k<=Ox && j+1<=m)   checkbetter(f[k][i][j+1],f[k][i][j]-getS(k,k,j+1,j+1,0)+getS(k,k,j+1,j+1,1));
          if (k>Ox && j-1>=Oy)   checkbetter(f[k][i][j-1],f[k][i][j]-getS(k,k,j,j,1)+getS(k,k,j,j,0));
        }        
        if (k>=Ox)
          for (int i=1;i<=Oy;i++) for (int j=Oy;j<=m;j++) if (f[k][i][j]!=-1)
          {
            checkmin(result,f[k][i][j]+getS(k+1,n,1,m,0));            
    //        if (f[k][i][j]+getS(k+1,n,1,m,0)==0) 
      //        printf("HERE %d %d %d %d %d\n",Ox,Oy,k,i,j);
          }
      }
//      for (int k=1;k<=n;k++) for (int i=1;i<=m;i++) for (int j=1;j<=m;j++) if (f[k][i][j]!=-1)
  //      printf("%d %d %d %d\n",k,i,j,f[k][i][j]);
    //  printf("\n");
    }
    return result;
  }
};