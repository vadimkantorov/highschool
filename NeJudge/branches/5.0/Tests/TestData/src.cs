using System; 
using System.Collections.Generic; 

public class VotingBloc { 
  public int[] abstainers(string[] voter) { 
      List<int> v1 = new List<int>(); 
        List<int> v2 = new List<int>(); 
      int a = 0; 
        foreach (string s in voter) 
        { 
            if (s[0] == 'Y') 
            { 
                v1.Add(a); 
            }  
            else 
            { 
                v2.Add(a); 
            } 
            ++a; 
        } 

      int n1 = v1.Count; 
      int n2 = v2.Count; 
        if (n1 == 0 || n2 == 0) 
            return new int[0]; 

        bool[,] g = new bool[n1, n2]; 
      a = 0; 
        foreach (string s in voter) 
        { 
            foreach (string p in s.Substring(1).Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries)) 
            { 
                int b = int.Parse(p) - 1; 
                if (v1.IndexOf(a) >= 0 && v2.IndexOf(b) >= 0) 
                    g[v1.IndexOf(a), v2.IndexOf(b)] = true; 
                if (v1.IndexOf(b) >= 0 && v2.IndexOf(a) >= 0) 
                    g[v1.IndexOf(b), v2.IndexOf(a)] = true; 
            } 
            ++a; 
        } 

        bool[] resA = new bool[n1]; 
        bool[] resB = new bool[n2]; 

      int amount = evaluate(n1, n2, g, resA, resB); 

        List<int> res = new List<int>(); 
        for (int i = 0; i < voter.Length; ++i) 
        { 
            if (v1.IndexOf(i) >= 0) 
            { 
                int c = v1.IndexOf(i); 
                resA[c] = true; 
                if (evaluate(n1, n2, g, resA, resB) == amount - 1) 
                { 
                    --amount; 
                    res.Add(i + 1); 
                } 
                else 
                    resA[c] = false; 
            } else 
            { 
                int c = v2.IndexOf(i); 
                resB[c] = true; 
                if (evaluate(n1, n2, g, resA, resB) == amount - 1) 
                { 
                    --amount; 
                    res.Add(i + 1); 
                } 
                else 
                    resB[c] = false; 
            } 
        } 

      return res.ToArray(); 
  } 

    private int n1; 
    private int n2; 
    private bool[,] g; 
    private bool[] u1; 
    private bool[] u2; 
    private int[] pair; 
    private bool[] mark; 

    private int evaluate(int n1, int n2, bool[,] g, bool[] u1, bool[] u2) 
    { 
        this.n1 = n1; 
        this.n2 = n2; 
        this.g = g; 
        this.u1 = u1; 
        this.u2 = u2; 
        pair = new int[n2]; 
        for (int i = 0; i < n2; ++i) 
            pair[i] = -1; 
        mark = new bool[n1]; 
        int res = 0; 
        for (int i = 0; i < n1; ++i) 
        { 
            if (!u1[i] && dfs(i)) 
            { 
                ++res; 
                for (int j = 0; j < n1; ++j) 
                    mark[j] = false; 
            } 
        } 
        return res; 
    } 

    private bool dfs(int at) 
    { 
        if (u1[at] || mark[at]) 
            return false; 
        mark[at] = true; 
        for (int i = 0; i < n2; ++i) 
            if (!u2[i] && g[at, i]) 
            { 
                int p = pair[i]; 
                if (p < 0 || dfs(p)) 
                { 
                    pair[i] = at; 
                    return true; 
                } 
            } 
        return false; 
    } 


} 
