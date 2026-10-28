using System;

class Program
{
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
        if (l < r)
        {
            int m = l + (r - l) / 2;
            mergeSort(a, l, m);
            mergeSort(a, m + 1, r);
            merge(a, l, m, r);
        }
    }

    static void Main()
    {
        int[] a = { 39, 28, 44, 11 };
        int s = a.Length;
        Console.WriteLine("Antes de ordenar el arreglo: ");
        for (int j = 0; j < s; j++)
        {
            Console.Write(a[j] + " ");
        }
        mergeSort(a, 0, s - 1);
        Console.WriteLine("\nDespues de ordenar el arreglo: ");
        for (int j = 0; j < s; j++)
        {
            Console.Write(a[j] + " ");
        }
    }
}
