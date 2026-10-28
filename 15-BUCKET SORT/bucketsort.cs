using System;
using System.Collections.Generic;

class Program
{
    static void insertion_sort(List<double> bukt)
    {
        for (int j = 1; j < bukt.Count; j++)
        {
            double val = bukt[j];
            int k = j - 1;
            while (k >= 0 && bukt[k] > val)
            {
                bukt[k + 1] = bukt[k];
                k -= 1;
            }
            bukt[k + 1] = val;
        }
    }

    static void bucket_sort(double[] inputArr)
    {
        int s = inputArr.Length;
        List<List<double>> bucketArr = new List<List<double>>();
        for (int i = 0; i < s; i++)
        {
            bucketArr.Add(new List<double>());
        }
        foreach (double j in inputArr)
        {
            int bi = (int)(s * j);
            bucketArr[bi].Add(j);
        }
        foreach (List<double> bukt in bucketArr)
        {
            insertion_sort(bukt);
        }
        int idx = 0;
        foreach (List<double> bukt in bucketArr)
        {
            foreach (double j in bukt)
            {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    static void Main()
    {
        double[] inputArr = { 0.77, 0.16, 0.38, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67 };
        Console.WriteLine("Arreglo antes de ordenar:");
        Console.WriteLine(string.Join(" ", inputArr));
        bucket_sort(inputArr);
        Console.WriteLine("Arreglo despues de ordenar:");
        Console.WriteLine(string.Join(" ", inputArr));
    }
}
