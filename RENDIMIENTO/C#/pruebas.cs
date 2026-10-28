using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
    const int LIMITE = 60; //segundos maximos que puede tardar un metodo
    static Stopwatch reloj = new Stopwatch(); //aqui se mide cuanto lleva ordenando
    static bool detenido = false; //se pone en true si ya paso el minuto
    static double porcentaje = 0; //lo que alcanzo a ordenar antes de detenerse

    static bool seAcaboElTiempo()
    {
        return reloj.Elapsed.TotalSeconds > LIMITE; //pa saber si ya paso el minuto
    }

    // 8 BURBUJA
    static void bubbleSort(int[] a)
    {
        int s = a.Length;
        for (int i = 0; i < s; i++)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = i * 100.0 / s; //las vueltas que alcanzo a dar
                return;
            }
            bool isSwapped = false;
            for (int j = 0; j < s - i - 1; j++)
            {
                if (a[j] > a[j + 1])
                {
                    int temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;
                    isSwapped = true;
                }
            }
            if (isSwapped == false)
            {
                break;
            }
        }
    }

    // 9 INSERCION
    static void insertionSort(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = i * 100.0 / a.Length;
                return;
            }
            int temp = a[i];
            int j = i - 1;
            while (j >= 0 && temp < a[j])
            {
                a[j + 1] = a[j];
                j = j - 1;
            }
            a[j + 1] = temp;
        }
    }

    // 10 SELECCION
    static void selection(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = i * 100.0 / a.Length;
                return;
            }
            int small = i;
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[small] > a[j])
                {
                    small = j;
                }
            }
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    // 11 QUICK SORT
    static int partition(int[] a, int l, int h)
    {
        int pvt = a[h];
        int j = l - 1;
        int temp;
        for (int k = l; k < h; k++)
        {
            if (a[k] < pvt)
            {
                j += 1;
                temp = a[j];
                a[j] = a[k];
                a[k] = temp;
            }
        }
        temp = a[j + 1];
        a[j + 1] = a[h];
        a[h] = temp;
        return j + 1;
    }

    static void qckSort(int[] a)
    {
        //se hace con una pila en vez de recursion porque con arreglos ordenados
        //o inversos la recursion se hace muy profunda y truena
        int n = a.Length;
        List<int> pila = new List<int>(); //se guardan de dos en dos, el inicio y el final
        pila.Add(0);
        pila.Add(n - 1);
        int listos = 0; //cuantos elementos ya quedaron en su lugar
        while (pila.Count > 0)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = listos * 100.0 / n;
                return;
            }
            int h = pila[pila.Count - 1];
            pila.RemoveAt(pila.Count - 1);
            int l = pila[pila.Count - 1];
            pila.RemoveAt(pila.Count - 1);
            if (l < h)
            {
                int pi = partition(a, l, h);
                listos += 1; //el pivote ya quedo en su lugar
                pila.Add(l);
                pila.Add(pi - 1);
                pila.Add(pi + 1);
                pila.Add(h);
            }
            else if (l == h)
            {
                listos += 1; //si es un solo elemento ya esta en su lugar
            }
        }
    }

    // 12 MERGE SORT
    static int mezclas = 0; //cuantas mezclas lleva, en total son n-1

    static void merge(int[] a, int l, int m, int r)
    {
        int a1 = m - l + 1;
        int a2 = r - m;
        int[] L = new int[a1];
        int[] R = new int[a2];
        for (int x = 0; x < a1; x++)
        {
            L[x] = a[l + x];
        }
        for (int y = 0; y < a2; y++)
        {
            R[y] = a[m + 1 + y];
        }
        int i = 0;
        int j = 0;
        int k = l;
        while (i < a1 && j < a2)
        {
            if (L[i] <= R[j])
            {
                a[k] = L[i];
                i = i + 1;
            }
            else
            {
                a[k] = R[j];
                j = j + 1;
            }
            k = k + 1;
        }
        while (i < a1)
        {
            a[k] = L[i];
            i = i + 1;
            k = k + 1;
        }
        while (j < a2)
        {
            a[k] = R[j];
            j = j + 1;
            k = k + 1;
        }
    }

    static void mergeSort(int[] a, int l, int r)
    {
        if (detenido)
        {
            return; //si ya se detuvo ya no sigue
        }
        if (seAcaboElTiempo())
        {
            detenido = true;
            porcentaje = mezclas * 100.0 / (a.Length - 1);
            return;
        }
        if (l < r)
        {
            int m = l + (r - l) / 2;
            mergeSort(a, l, m);
            mergeSort(a, m + 1, r);
            if (detenido)
            {
                return;
            }
            merge(a, l, m, r);
            mezclas += 1;
        }
    }

    // 13 HEAP SORT
    static void heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < n && arr[left] > arr[largest])
        {
            largest = left;
        }
        if (right < n && arr[right] > arr[largest])
        {
            largest = right;
        }
        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;
            heapify(arr, n, largest);
        }
    }

    static void heapsort(int[] arr)
    {
        int n = arr.Length;
        int total = n / 2 + n - 1; //lo que se tarda en armar el monticulo y en sacar
        int pasos = 0;
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = pasos * 100.0 / total;
                return;
            }
            heapify(arr, n, i);
            pasos += 1;
        }
        for (int i = n - 1; i > 0; i--)
        {
            if (seAcaboElTiempo())
            {
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
    static void shellSort(int[] inputArr)
    {
        int size = inputArr.Length;
        int totalGaps = 0; //pa saber cuantas vueltas de gap va a dar
        int g = size / 2;
        while (g > 0)
        {
            totalGaps += 1;
            g = g / 2;
        }
        int gapsHechos = 0;
        int gapSize = size / 2;
        while (gapSize > 0)
        {
            for (int j = gapSize; j < size; j++)
            {
                if (j % 1000 == 0 && seAcaboElTiempo()) //cada 1000 pa no checar el reloj a cada rato
                {
                    detenido = true;
                    porcentaje = (gapsHechos + (double)j / size) * 100.0 / totalGaps;
                    return;
                }
                int val = inputArr[j];
                int k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val)
                {
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
    static void insertion_sort(List<int> bukt)
    {
        for (int j = 1; j < bukt.Count; j++)
        {
            int val = bukt[j];
            int k = j - 1;
            while (k >= 0 && bukt[k] > val)
            {
                bukt[k + 1] = bukt[k];
                k -= 1;
            }
            bukt[k + 1] = val;
        }
    }

    static void bucket_sort(int[] inputArr)
    {
        int s = inputArr.Length;
        int mayor = inputArr[0];
        foreach (int e in inputArr)
        {
            if (e > mayor)
            {
                mayor = e;
            }
        }
        List<List<int>> bucketArr = new List<List<int>>();
        for (int i = 0; i < s; i++)
        {
            bucketArr.Add(new List<int>());
        }
        foreach (int j in inputArr)
        {
            int bi = (int)((long)j * s / (mayor + 1)); //como son enteros y no decimales se divide entre el mayor, va en long porque se pasa de int
            bucketArr[bi].Add(j);
        }
        int hechas = 0;
        foreach (List<int> bukt in bucketArr)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = hechas * 100.0 / s;
                return;
            }
            insertion_sort(bukt);
            hechas += 1;
        }
        int idx = 0;
        foreach (List<int> bukt in bucketArr)
        {
            foreach (int j in bukt)
            {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    // 16 RADIX SORT
    static void countingSort(int[] arr, int exp)
    {
        int s = arr.Length;
        int[] outputArray = new int[s];
        int[] countArray = new int[10];
        for (int x = 0; x < s; x++)
        {
            int idx = arr[x] / exp;
            countArray[idx % 10] += 1;
        }
        for (int y = 1; y < 10; y++)
        {
            countArray[y] += countArray[y - 1];
        }
        int j = s - 1;
        while (j >= 0)
        {
            int idx = arr[j] / exp;
            outputArray[countArray[idx % 10] - 1] = arr[j];
            countArray[idx % 10] -= 1;
            j -= 1;
        }
        for (j = 0; j < arr.Length; j++)
        {
            arr[j] = outputArray[j];
        }
    }

    static void radixSort(int[] arr)
    {
        int max1 = arr[0];
        foreach (int e in arr)
        {
            if (e > max1)
            {
                max1 = e;
            }
        }
        int digitos = max1.ToString().Length; //cuantas pasadas va a hacer
        int hechos = 0;
        int exp = 1;
        while (max1 / exp >= 1)
        {
            if (seAcaboElTiempo())
            {
                detenido = true;
                porcentaje = hechos * 100.0 / digitos;
                return;
            }
            countingSort(arr, exp);
            hechos += 1;
            exp *= 10;
        }
    }

    static void ordenar(string metodo, int[] a)
    {
        if (metodo == "Burbuja")
        {
            bubbleSort(a);
        }
        else if (metodo == "Insercion")
        {
            insertionSort(a);
        }
        else if (metodo == "Seleccion")
        {
            selection(a);
        }
        else if (metodo == "Quick Sort")
        {
            qckSort(a);
        }
        else if (metodo == "Merge Sort")
        {
            mergeSort(a, 0, a.Length - 1);
        }
        else if (metodo == "Heap Sort")
        {
            heapsort(a);
        }
        else if (metodo == "Shell Sort")
        {
            shellSort(a);
        }
        else if (metodo == "Bucket Sort")
        {
            bucket_sort(a);
        }
        else if (metodo == "Radix Sort")
        {
            radixSort(a);
        }
    }

    static void Main()
    {
        string[] metodos = { "Burbuja", "Insercion", "Seleccion", "Quick Sort", "Merge Sort",
                             "Heap Sort", "Shell Sort", "Bucket Sort", "Radix Sort" };
        int[] tamanos = { 100, 1000, 10000, 100000, 1000000 };
        string[] tipos = { "Aleatorio", "Inverso", "Ordenado" };

        //se hacen los arreglos una sola vez pa que todos los metodos usen los mismos
        Console.WriteLine("Creando los arreglos...");
        Random random = new Random();
        int[][][] arreglos = new int[tamanos.Length][][]; //[tamano][tipo]
        for (int x = 0; x < tamanos.Length; x++)
        {
            int t = tamanos[x];
            int[] aleatorio = new int[t];
            int[] inverso = new int[t];
            int[] ordenado = new int[t];
            for (int i = 0; i < t; i++)
            {
                aleatorio[i] = random.Next(0, t + 1);
                inverso[i] = t - i;
                ordenado[i] = i + 1;
            }
            arreglos[x] = new int[3][];
            arreglos[x][0] = aleatorio;
            arreglos[x][1] = inverso;
            arreglos[x][2] = ordenado;
        }

        //el txt se guarda en la carpeta desde donde se corre el programa
        string ruta = "resultados.txt";
        File.WriteAllText(ruta, "PRUEBAS DE RENDIMIENTO\n");
        File.AppendAllText(ruta, "Lenguaje: C#\n");
        File.AppendAllText(ruta, "Limite por prueba: " + LIMITE + " segundos\n");
        File.AppendAllText(ruta, "==============================================\n");

        foreach (string metodo in metodos)
        {
            File.AppendAllText(ruta, "\nMetodo: " + metodo + "\n");
            for (int x = 0; x < tamanos.Length; x++)
            {
                int t = tamanos[x];
                for (int y = 0; y < tipos.Length; y++)
                {
                    string tipo = tipos[y];
                    int[] a = (int[])arreglos[x][y].Clone(); //una copia pa no ordenar el original
                    detenido = false;
                    porcentaje = 0;
                    mezclas = 0;
                    Console.Write(metodo + " - " + t + " - " + tipo + " ... ");
                    reloj.Restart();
                    ordenar(metodo, a);
                    reloj.Stop();
                    double tiempo = reloj.Elapsed.TotalSeconds;

                    string linea = "Lenguaje: C# | Metodo: " + metodo + " | Tamaño: " + t + " | Tipo: " + tipo;
                    if (detenido)
                    {
                        linea = linea + " | Tiempo: mas de " + LIMITE + " s | Se detuvo al " + porcentaje.ToString("F2") + "%";
                    }
                    else
                    {
                        linea = linea + " | Tiempo: " + tiempo.ToString("F6") + " s";
                    }
                    Console.WriteLine(linea);

                    //se guarda cada resultado luego luego por si se corta el programa
                    File.AppendAllText(ruta, linea + "\n");
                }
            }
        }

        Console.WriteLine("\nListo, los resultados estan en: " + Path.GetFullPath(ruta));
    }
}
