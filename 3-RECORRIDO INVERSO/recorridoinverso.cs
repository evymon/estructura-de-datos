using System;
class Program
{
    static void Main()
    {
        int[] arr= {30, 40, 50, 60, 70, 80};
        int size = arr.Length;
        Console.WriteLine("El recorrido inverso del array es: ");
        for (int g= size - 1; g >= 0; g--)
        {
            Console.Write(arr[g]+ " ");
        }
    }
 }  