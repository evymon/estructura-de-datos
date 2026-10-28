using System;
class Program {
    static void Main(string[] args) {
        int[] arr = {30, 40, 50, 60, 70, 80};
        Console.WriteLine("recorrido secuencial:");
        for (int e = 0; e < arr.Length; e++) {
            Console.WriteLine(arr[e]);
        }
    }
}