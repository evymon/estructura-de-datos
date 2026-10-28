import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;

public class pruebas {
    static final int LIMITE = 60; //segundos maximos que puede tardar un metodo
    static long inicio = 0; //aqui se guarda cuando empezo a ordenar
    static boolean detenido = false; //se pone en true si ya paso el minuto
    static double porcentaje = 0; //lo que alcanzo a ordenar antes de detenerse

    static boolean seAcaboElTiempo() {
        return (System.nanoTime() - inicio) / 1000000000.0 > LIMITE; //pa saber si ya paso el minuto
    }

    // 8 BURBUJA
    static void bubbleSort(int[] a) {
        int s = a.length;
        for (int i = 0; i < s; i++) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = i * 100.0 / s; //las vueltas que alcanzo a dar
                return;
            }
            boolean isSwapped = false;
            for (int j = 0; j < s - i - 1; j++) {
                if (a[j] > a[j + 1]) {
                    int temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;
                    isSwapped = true;
                }
            }
            if (isSwapped == false) {
                break;
            }
        }
    }

    // 9 INSERCION
    static void insertionSort(int[] a) {
        for (int i = 1; i < a.length; i++) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = i * 100.0 / a.length;
                return;
            }
            int temp = a[i];
            int j = i - 1;
            while (j >= 0 && temp < a[j]) {
                a[j + 1] = a[j];
                j = j - 1;
            }
            a[j + 1] = temp;
        }
    }

    // 10 SELECCION
    static void selection(int[] a) {
        for (int i = 0; i < a.length; i++) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = i * 100.0 / a.length;
                return;
            }
            int small = i;
            for (int j = i + 1; j < a.length; j++) {
                if (a[small] > a[j]) {
                    small = j;
                }
            }
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    // 11 QUICK SORT
    static int partition(int[] a, int l, int h) {
        int pvt = a[h];
        int j = l - 1;
        for (int k = l; k < h; k++) {
            if (a[k] < pvt) {
                j += 1;
                int temp = a[j];
                a[j] = a[k];
                a[k] = temp;
            }
        }
        int temp = a[j + 1];
        a[j + 1] = a[h];
        a[h] = temp;
        return j + 1;
    }

    static void qckSort(int[] a) {
        //se hace con una pila en vez de recursion porque con arreglos ordenados
        //o inversos la recursion se hace muy profunda y truena
        int n = a.length;
        ArrayList<Integer> pila = new ArrayList<>(); //se guardan de dos en dos, el inicio y el final
        pila.add(0);
        pila.add(n - 1);
        int listos = 0; //cuantos elementos ya quedaron en su lugar
        while (pila.size() > 0) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = listos * 100.0 / n;
                return;
            }
            int h = pila.remove(pila.size() - 1);
            int l = pila.remove(pila.size() - 1);
            if (l < h) {
                int pi = partition(a, l, h);
                listos += 1; //el pivote ya quedo en su lugar
                pila.add(l);
                pila.add(pi - 1);
                pila.add(pi + 1);
                pila.add(h);
            } else if (l == h) {
                listos += 1; //si es un solo elemento ya esta en su lugar
            }
        }
    }

    // 12 MERGE SORT
    static int mezclas = 0; //cuantas mezclas lleva, en total son n-1

    static void merge(int[] a, int l, int m, int r) {
        int a1 = m - l + 1;
        int a2 = r - m;
        int[] L = new int[a1];
        int[] R = new int[a2];
        for (int j = 0; j < a1; j++) {
            L[j] = a[l + j];
        }
        for (int k = 0; k < a2; k++) {
            R[k] = a[m + 1 + k];
        }
        int i = 0;
        int j = 0;
        int k = l;
        while (i < a1 && j < a2) {
            if (L[i] <= R[j]) {
                a[k] = L[i];
                i = i + 1;
            } else {
                a[k] = R[j];
                j = j + 1;
            }
            k = k + 1;
        }
        while (i < a1) {
            a[k] = L[i];
            i = i + 1;
            k = k + 1;
        }
        while (j < a2) {
            a[k] = R[j];
            j = j + 1;
            k = k + 1;
        }
    }

    static void mergeSort(int[] a, int l, int r) {
        if (detenido) {
            return; //si ya se detuvo ya no sigue
        }
        if (seAcaboElTiempo()) {
            detenido = true;
            porcentaje = mezclas * 100.0 / (a.length - 1);
            return;
        }
        if (l < r) {
            int m = l + (r - l) / 2;
            mergeSort(a, l, m);
            mergeSort(a, m + 1, r);
            if (detenido) {
                return;
            }
            merge(a, l, m, r);
            mezclas += 1;
        }
    }

    // 13 HEAP SORT
    static void heapify(int[] arr, int n, int i) {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < n && arr[left] > arr[largest]) {
            largest = left;
        }
        if (right < n && arr[right] > arr[largest]) {
            largest = right;
        }
        if (largest != i) {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;
            heapify(arr, n, largest);
        }
    }

    static void heapsort(int[] arr) {
        int n = arr.length;
        int total = n / 2 + n - 1; //lo que se tarda en armar el monticulo y en sacar
        int pasos = 0;
        for (int i = n / 2 - 1; i >= 0; i--) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = pasos * 100.0 / total;
                return;
            }
            heapify(arr, n, i);
            pasos += 1;
        }
        for (int i = n - 1; i > 0; i--) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = pasos * 100.0 / total;
                return;
            }
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            heapify(arr, i, 0);
            pasos += 1;
        }
    }

    // 14 SHELL SORT
    static void shellSort(int[] inputArr) {
        int size = inputArr.length;
        int totalGaps = 0; //pa saber cuantas vueltas de gap va a dar
        int g = size / 2;
        while (g > 0) {
            totalGaps += 1;
            g = g / 2;
        }
        int gapsHechos = 0;
        int gapSize = size / 2;
        while (gapSize > 0) {
            for (int j = gapSize; j < size; j++) {
                if (j % 1000 == 0 && seAcaboElTiempo()) { //cada 1000 pa no checar el reloj a cada rato
                    detenido = true;
                    porcentaje = (gapsHechos + (double) j / size) * 100.0 / totalGaps;
                    return;
                }
                int val = inputArr[j];
                int k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val) {
                    inputArr[k] = inputArr[k - gapSize];
                    k = k - gapSize;
                }
                inputArr[k] = val;
            }
            gapsHechos += 1;
            gapSize = gapSize / 2;
        }
    }

    // 15 BUCKET SORT
    static void insertion_sort(ArrayList<Integer> bukt) {
        for (int j = 1; j < bukt.size(); j++) {
            int val = bukt.get(j);
            int k = j - 1;
            while (k >= 0 && bukt.get(k) > val) {
                bukt.set(k + 1, bukt.get(k));
                k -= 1;
            }
            bukt.set(k + 1, val);
        }
    }

    static void bucket_sort(int[] inputArr) {
        int s = inputArr.length;
        int mayor = inputArr[0];
        for (int e : inputArr) {
            if (e > mayor) {
                mayor = e;
            }
        }
        ArrayList<ArrayList<Integer>> bucketArr = new ArrayList<>();
        for (int i = 0; i < s; i++) {
            bucketArr.add(new ArrayList<>());
        }
        for (int j : inputArr) {
            int bi = (int) ((long) j * s / (mayor + 1)); //como son enteros y no decimales se divide entre el mayor, va en long porque se pasa de int
            bucketArr.get(bi).add(j);
        }
        int hechas = 0;
        for (ArrayList<Integer> bukt : bucketArr) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = hechas * 100.0 / s;
                return;
            }
            insertion_sort(bukt);
            hechas += 1;
        }
        int idx = 0;
        for (ArrayList<Integer> bukt : bucketArr) {
            for (int j : bukt) {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    // 16 RADIX SORT
    static void countingSort(int[] arr, int exp) {
        int s = arr.length;
        int[] outputArray = new int[s];
        int[] countArray = new int[10];
        for (int j = 0; j < s; j++) {
            int idx = arr[j] / exp;
            countArray[idx % 10] += 1;
        }
        for (int j = 1; j < 10; j++) {
            countArray[j] += countArray[j - 1];
        }
        int j = s - 1;
        while (j >= 0) {
            int idx = arr[j] / exp;
            outputArray[countArray[idx % 10] - 1] = arr[j];
            countArray[idx % 10] -= 1;
            j -= 1;
        }
        for (j = 0; j < arr.length; j++) {
            arr[j] = outputArray[j];
        }
    }

    static void radixSort(int[] arr) {
        int max1 = arr[0];
        for (int e : arr) {
            if (e > max1) {
                max1 = e;
            }
        }
        int digitos = String.valueOf(max1).length(); //cuantas pasadas va a hacer
        int hechos = 0;
        int exp = 1;
        while (max1 / exp >= 1) {
            if (seAcaboElTiempo()) {
                detenido = true;
                porcentaje = hechos * 100.0 / digitos;
                return;
            }
            countingSort(arr, exp);
            hechos += 1;
            exp *= 10;
        }
    }

    static void ordenar(String metodo, int[] a) {
        if (metodo.equals("Burbuja")) {
            bubbleSort(a);
        } else if (metodo.equals("Insercion")) {
            insertionSort(a);
        } else if (metodo.equals("Seleccion")) {
            selection(a);
        } else if (metodo.equals("Quick Sort")) {
            qckSort(a);
        } else if (metodo.equals("Merge Sort")) {
            mergeSort(a, 0, a.length - 1);
        } else if (metodo.equals("Heap Sort")) {
            heapsort(a);
        } else if (metodo.equals("Shell Sort")) {
            shellSort(a);
        } else if (metodo.equals("Bucket Sort")) {
            bucket_sort(a);
        } else if (metodo.equals("Radix Sort")) {
            radixSort(a);
        }
    }

    public static void main(String[] args) throws IOException {
        String[] metodos = {"Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
                            "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort"};
        int[] tamanos = {100, 1000, 10000, 100000, 1000000};
        String[] tipos = {"Aleatorio", "Inverso", "Ordenado"};

        //se hacen los arreglos una sola vez pa que todos los metodos usen los mismos
        System.out.println("Creando los arreglos...");
        Random random = new Random();
        int[][][] arreglos = new int[tamanos.length][3][]; //[tamano][tipo]
        for (int x = 0; x < tamanos.length; x++) {
            int t = tamanos[x];
            int[] aleatorio = new int[t];
            int[] inverso = new int[t];
            int[] ordenado = new int[t];
            for (int i = 0; i < t; i++) {
                aleatorio[i] = random.nextInt(t + 1);
                inverso[i] = t - i;
                ordenado[i] = i + 1;
            }
            arreglos[x][0] = aleatorio;
            arreglos[x][1] = inverso;
            arreglos[x][2] = ordenado;
        }

        //el txt se guarda en la carpeta desde donde se corre el programa
        String ruta = "resultados.txt";
        FileWriter archivo = new FileWriter(ruta);
        archivo.write("PRUEBAS DE RENDIMIENTO\n");
        archivo.write("Lenguaje: Java\n");
        archivo.write("Limite por prueba: " + LIMITE + " segundos\n");
        archivo.write("==============================================\n");
        archivo.close();

        for (String metodo : metodos) {
            archivo = new FileWriter(ruta, true);
            archivo.write("\nMetodo: " + metodo + "\n");
            archivo.close();
            for (int x = 0; x < tamanos.length; x++) {
                int t = tamanos[x];
                for (int y = 0; y < tipos.length; y++) {
                    String tipo = tipos[y];
                    int[] a = arreglos[x][y].clone(); //una copia pa no ordenar el original
                    detenido = false;
                    porcentaje = 0;
                    mezclas = 0;
                    System.out.print(metodo + " - " + t + " - " + tipo + " ... ");
                    inicio = System.nanoTime();
                    ordenar(metodo, a);
                    double tiempo = (System.nanoTime() - inicio) / 1000000000.0;

                    String linea = "Lenguaje: Java | Metodo: " + metodo + " | Tamaño: " + t + " | Tipo: " + tipo;
                    if (detenido) {
                        linea = linea + " | Tiempo: mas de " + LIMITE + " s | Se detuvo al " + String.format("%.2f", porcentaje) + "%";
                    } else {
                        linea = linea + " | Tiempo: " + String.format("%.6f", tiempo) + " s";
                    }
                    System.out.println(linea);

                    //se guarda cada resultado luego luego por si se corta el programa
                    archivo = new FileWriter(ruta, true);
                    archivo.write(linea + "\n");
                    archivo.close();
                }
            }
        }

        System.out.println("\nListo, los resultados estan en: " + ruta);
    }
}
