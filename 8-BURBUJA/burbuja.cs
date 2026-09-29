using System;

class Program
{
    static void bubbleSort(int[] a)
    {
        int s = a.Length;
        for (int i = 0; i < s; i++)
        {
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

    static void Main()
    {
        int[] a = { 15, 16, 11, 13, 14 };
        Console.WriteLine("antes de ordenar los elementos del array son: ");
        foreach (int e in a)
        {
            Console.Write(e + " ");
        }

        bubbleSort(a);
        Console.WriteLine("\ndespues de ordenar los elementos del array son: ");
        for (int e = 0; e < a.Length; e++)
        {
            Console.Write(a[e] + " ");
        }
    }
}
