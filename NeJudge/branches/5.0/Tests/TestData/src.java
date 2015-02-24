import java.io.*;
import java.util.Scanner;
import java.util.Stack;

public class Main {

    public static void main(String[] args) throws Exception{
        Scanner s = new Scanner(new File("input.txt"));
        PrintWriter out = new PrintWriter("output.txt");
        int t = Integer.parseInt(s.nextLine());
        if (t > 0 && t <= 100) {
            long a = 0;
            long d = 0;
            String a1 = s.nextLine();
            int indice = 0;
            for (indice = 0; indice < a1.length(); indice++) {
                if (a1.charAt(indice) == ' ') {
                    break;
                }
            }
            if (indice > 0) {
                d = Long.parseLong(a1.substring(0, indice));
                a = Long.parseLong(a1.substring(indice + 1));

            if (2 <= t && t <= 100 && 1 <= d && a <= 10000000) {
                int flag = 0;
                int[][] m = new int[t][t];
                for (int i = 0; i < t; i++) {
                    for (int j = 0; j < t; j++) {
                        m[i][j] = 0;
                    }
                }

                for (int i = 0; i < t; i++) {

                    String fila = s.nextLine();
                    if (fila.length() > t) {
                        flag = 1;
                        break;
                    }
                    for (int j = 0; j < t; j++) {
                        //m[i][j] = Integer.parseInt(fila.valueOf(fila.charAt(j)));
                        if (fila.charAt(j) == '1') {
                            m[i][j] = 1;

                        } else if (fila.charAt(j) == '0') {
                            m[i][j] = 0;

                        } else {
                            flag = 1;
                        }

                    }
                }
                if (flag == 0) {
                    Grafo g = new Grafo(m, d, a, out);
                    g.resolver();
                }}
            }
        }
        out.close();
    }

}

class Grafo {

    private Vertice[] vertices;
    private int[][] matriz;
    private int cantVertices;
    private long c1,  c2;

    PrintWriter out;
    public Grafo(int[][] m, long a, long d, PrintWriter out) {
        c1 = a;
        this.out = out;
        c2 = d;
        matriz = m;
        cantVertices = matriz.length;
        vertices = new Vertice[matriz.length];
        for (int i = 0; i < matriz.length; i++) {
            vertices[i] = new Vertice();
        }

    }

    public int esConexo() {
        int aux = 1;
        Stack<Vertice> p = new Stack();
        vertices[0].visitar();
        p.push(vertices[0]);
        while (!p.empty()) {
            Vertice v = getVertAdyNoVisitado(p.peek());
            if (v == null) {
                p.pop();
            } else {
                v.visitar();
                aux++;
                p.push(v);
            }
        }
        reiniciarVisitados();
        return aux;
    }

    private Vertice getVertAdyNoVisitado(Vertice v) {
        for (int i = 0; i < cantVertices; i++) {
            if (matriz[getPosicion(v)][i] >= 1 && vertices[i].fueVisitado() == false) {
                return vertices[i];
            }
        }
        return null;
    }

    int getPosicion(Vertice c) {
        for (int i = 0; i < cantVertices; i++) {
            if (vertices[i].equals(c)) {
                return i;
            }
        }
        return -1;
    }

    private void reiniciarVisitados() {
        for (int i = 0; i < cantVertices; i++) {
            vertices[i].reiniciarVisitado();
        }
    }

    void resolver() {
        int[][] matrizInicial = new int[cantVertices][cantVertices];
        for (int i = 0; i < cantVertices; i++) {
            for (int j = 0; j < cantVertices; j++) {
                matrizInicial[i][j] = matriz[i][j];
            }
        }
        int aux = esConexo();
        while (aux != cantVertices) {
            for (int i = 0; i < cantVertices; i++) {
                for (int j = 0; j < cantVertices; j++) {
                    if (matriz[i][j] != 1) {
                        matriz[i][j] = 1;
                        matriz[j][i] = 1;

                        int auxi = esConexo();
                        if (auxi > aux) {
                            aux = auxi;
                        } else {
                            matriz[i][j] = 0;
                            matriz[j][i] = 0;

                        }
                    }
                }
            }
        }
        for (int i = 0; i < cantVertices; i++) {
            for (int j = 0; j < cantVertices; j++) {
                if (matriz[i][j] == 1) {
                    matriz[i][j] = 0;
                    matriz[j][i] = 0;
                }
                if (esConexo() != cantVertices) {
                    matriz[i][j] = 1;
                    matriz[j][i] = 1;
                }
            }
        }
        char[][] matrizSolucion = new char[cantVertices][cantVertices];
        int a = 0;
        int d = 0;
        for (int i = 0; i < cantVertices; i++) {
            for (int j = i; j < cantVertices; j++) {
                if (matrizInicial[i][j] == matriz[i][j]) {
                    matrizSolucion[i][j] = '0';
                    matrizSolucion[j][i] = '0';

                } else if (matrizInicial[i][j] < matriz[i][j]) {
                    matrizSolucion[i][j] = 'a';
                    matrizSolucion[j][i] = 'a';
                    a++;

                } else {
                    matrizSolucion[i][j] = 'd';
                    matrizSolucion[j][i] = 'd';
                    d++;
                }
            }
        }
        long res = (a * c2) + (d * c1);

        out.println(res);
        mostrarMatriz(matrizSolucion);
    }

    public void mostrarMatriz(char[][] Matriz) {
        for (int i = 0; i < Matriz.length; i++) {
            for (int j = 0; j < Matriz.length; j++) {
                out.print(Matriz[i][j]);
            }
            if (i < Matriz.length) {
                out.println();
            }
        }
    }
}

class Vertice {

    private boolean fueVisitado;

    public Vertice() {
        fueVisitado = false;
    }

    public void visitar() {
        fueVisitado = true;
    }

    public boolean fueVisitado() {
        return fueVisitado;
    }

    public void reiniciarVisitado() {
        fueVisitado = false;
    }
}
