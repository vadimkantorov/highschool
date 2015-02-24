import java.io.*;
import java.util.*;

public class Main
{
        public static void main(String[] a) throws Exception
        {
                Scanner in = new Scanner(new File("input.txt"));
                int n = in.nextInt();
                PrintWriter out = new PrintWriter(new File("output.txt"));
                out.println(n + 3);
                out.close();
        }

        private class Inner
        {
        }
}
