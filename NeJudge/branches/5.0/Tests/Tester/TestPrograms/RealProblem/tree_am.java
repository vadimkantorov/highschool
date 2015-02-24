import java.io.*;
import java.util.*;


public class Main {
	public static void main(String[] args) throws Exception {
		Scanner in = new Scanner(new File("input.txt"));
		int	N = in.nextInt();
		long	D = in.nextInt(),
			A = in.nextInt(),
			R = 0;
		char M[][] = new char[N][];
		int C[] = new int[N];
		for (int i=0; i<N; i++) {
			M[i] = in.next().toCharArray();
			C[i] = i;
		}
		for (int i=0; i<N; i++)
			for (int j=i+1; j<N; j++)
				if (M[i][j]=='1') {
					if (C[i]==C[j]) {
						M[i][j] = M[j][i] = 'd';
						R += D;
					} else {
						int cold = C[j],
							cnew = C[i];
						for (int k=0; k<N; k++)
							if (C[k] == cold)
								C[k] = cnew;
					}
				}
		for (int i=1; i<N; i++) {
			int cold = C[i-1],
				cnew = C[i];
			if (cold != cnew) {
				for (int k=0; k<N; k++)
					if (C[k] == cold)
						C[k] = cnew;
				M[i-1][i] = M[i][i-1] = 'a';
				R += A;
			}
		}
		PrintWriter out = new PrintWriter("output.txt");
		out.println(R);
		for (int i=0; i<N; i++) {
			for (int j=0; j<N; j++)
				out.print( M[i][j]>'1' ? M[i][j] : '0' );
			out.println();
		}
		out.close();
	}
}
