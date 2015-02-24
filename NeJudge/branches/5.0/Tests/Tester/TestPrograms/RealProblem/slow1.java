import java.io.*;
import java.util.Scanner;
import java.util.Stack;

public class Main {

    public static void main(String[] args) throws Exception{
        Scanner s = new Scanner(new File("input.txt"));
        int t = Integer.parseInt(s.nextLine());
        int a = 0;
        long d = 0;
        String a1 = s.nextLine();
        int indice = 0;
        for (indice = 0; indice < a1.length(); indice++) {
            if (a1.charAt(indice) == ' ') {
                break;
            }
        }
        d = Long.parseLong(a1.substring(0, indice));
        a = Integer.parseInt(a1.substring(indice + 1));
        Grafo g = new Grafo(t, d, a);

        if (2 <= t && t <= 100 && 1 <= d && a <= 10000000) {
            int ca = 0;
            for (int i = 0; i < t; i++) {
                String fila = s.nextLine();
                for (int j = i + 1; j < t; j++) {
                    if (fila.charAt(j) == '1') {
                        g.addArista(i, j);
                    }
                }
            }
            g.resolver();
        }

		g.out.close();
    }
}

class Grafo {

public static PrintWriter out;	

    private final int MAX_VERTICES;
    private Vertice[] vertices;
    private int[][] matriz;
    private int cantVertices;
    private int cantAristas;
    int c2;
    long c1;
    //private Array

    public Grafo(int tam, long c, int cc) throws Exception {
    	out = new PrintWriter("output.txt");
        MAX_VERTICES = tam;
        c1 = c;
        c2 = cc;
        vertices = new Vertice[MAX_VERTICES];
        matriz = new int[MAX_VERTICES][MAX_VERTICES];
        cantVertices = tam;
        cantAristas = 0;
        for (int i = 0; i < MAX_VERTICES; i++) {
            for (int j = i; j < MAX_VERTICES; j++) {
                matriz[i][j] = 0;
                matriz[j][i] = 0;
            }
            vertices[i] = new Vertice(i);
        }
    }

    public void addArista(int a, int b) {
        matriz[a][b] = 1;
        matriz[b][a] = 1;
        cantAristas++;

    }

    public void removeArista(int a, int b) {
        matriz[a][b] = 0;
        matriz[b][a] = 0;
        cantAristas--;

    }

    public int esConexo() {
        boolean res;
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

    void resolver() {
        int[][] matrizInicial = new int[cantVertices][cantVertices];
        for (int i = 0; i < cantVertices; i++) {
            for (int j = i; j < cantVertices; j++) {
                matrizInicial[i][j] = matriz[i][j];
                matrizInicial[j][i] = matriz[i][j];
            }
        }
        int aux = esConexo();
        while (aux != cantVertices) {
            for (int i = 0; i < cantVertices; i++) {
                for (int j = i + 1; j < cantVertices; j++) {
                    if (matriz[i][j] != 1) {
                        addArista(i, j);
                        int auxi = esConexo();
                        if (auxi > aux) {
                            aux = auxi;
                        } else {
                            removeArista(i, j);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < cantVertices; i++) {
            if (cantAristas == cantVertices - 1) {
                break;
            }
            for (int j = i + 1; j < cantVertices; j++) {
                if (matriz[i][j] == 1) {
                    removeArista(i, j);
                }
                if (esConexo() != cantVertices) {
                    addArista(i, j);
                }
            }
        }
        char[][] matrizSolucion = new char[cantVertices][cantVertices];
        int a = 0;
        int d = 0;
        for (int i = 0; i < cantVertices; i++) {
            for (int j = i; j < cantVertices; j++) {
                if (matrizInicial[i][j] == matriz[i][j] || i == j) {
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
            out.println();
        }

    }

    private Vertice getVertAdyNoVisitado(Vertice v) {
        for (int i = 0; i < cantVertices; i++) {
            if (matriz[v.posicion][i] == 1 && vertices[i].fueVisitado() == false) {
                return vertices[i];
            }
        }
        return null;
    }

    private void reiniciarVisitados() {
        for (int i = 0; i < cantVertices; i++) {
            vertices[i].reiniciarVisitado();
        }
    }
}

class Vertice {

    private boolean fueVisitado;
    int posicion;

    public Vertice(int pos) {
        fueVisitado = false;
        posicion = pos;
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

