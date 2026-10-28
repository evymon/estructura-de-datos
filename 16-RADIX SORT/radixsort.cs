using System;

class Program
{
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
        int exp = 1;
        while (max1 / exp >= 1)
        {
            countingSort(arr, exp);
            exp *= 10;
        }
    }

    static void Main()
    {
        int[] arr = { 171, 46, 76, 91, 803, 25, 3, 67 };
        Console.WriteLine("Arreglo antes de ordenar: ");
        for (int j = 0; j < arr.Length; j++)
        {
            Console.Write(arr[j] + " ");
        }
        radixSort(arr);
        Console.WriteLine("\nDespues de ordenar el arreglo: ");
        for (int j = 0; j < arr.Length; j++)
        {
            Console.Write(arr[j] + " ");
        }
    }
}
