#include <cstdio>
#include <iostream>
#include <string>
using namespace std;

bool is_correct(string in)
{
     if(in.length()==0)
          return true;
     string str;
     char c_close, c_open;
     int opened;
     int i=0;
     while(i < in.length())
     {
          if(str.length() == 0)
          {
               c_open = in[i];
               opened = 0;
               switch(c_open)
               {
               case '(':
                    c_close = ')';
                    break;
               case '[':
                    c_close = ']';
                    break;
               default:
                    return false;
               }
               i++;
          }
          if(in[i] != c_close)
          {
               str+=in[i];
               if(in[i] == c_open)
                    opened++;
          }
          else
          {
               if(opened != 0)
                    str+=in[i];
               opened--;
          }
          if(opened == -1)
               if(str.length() == 0 && i == in.length()-1)
                    return true;
               else
               if(is_correct(str))
                    str.erase();
               else
                    return false;
          i++;
     }
     if(str.length() == 0)     
          return true;
     else
          return false;
}

int main()
{
     //freopen("OUTPUT.TXT", "w", stdout);
     //freopen("input.txt", "r", stdin);
     string in;
     cin>>in;
     if(is_correct(in))
          cout<<"YES";
     else
          cout<<"NO";
     return 0;
}