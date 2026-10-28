using System;

class ShellSort
{
    static void displayArr(int[] inputArr)
    {
        foreach (int k in inputArr)
        {
            Console.Write(k + " ");
        }
        Console.WriteLine();
    }

    int sort(int[] inputArr)
    {
        int size = inputArr.Length;
        int gapSize = size / 2;
        while (gapSize > 0)
        {
            for (int j = gapSize; j < size; j++)
            {
                int val = inputArr[j];
                int k = j;
                while (k >= gapSize && inputArr[k - gapSize] > val)
                {
                    inputArr[k] = inputArr[k - gapSize];
                    k = k - gapSize;
                }
                inputArr[k] = val;
            }
            gapSize = gapSize / 2;
        }
        return 0;
    }

    static void Main()
    {
        int[] inputArr = { 36, 34, 43, 11, 15, 20, 28, 45 };
        Console.WriteLine("Arreglo antes de ser ordenado: ");
        ShellSort.displayArr(inputArr);
        ShellSort obj = new ShellSort();
        obj.sort(inputArr);
        Console.WriteLine("Arreglo despues de ser ordenado: ");
        ShellSort.displayArr(inputArr);
    }
}
