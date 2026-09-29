using System;

class Program
{
    static int findEle(int[] inputArr, int s, int targetEle)
    {
        for (int k = 0; k < s; k++)
        {
            if (inputArr[k] == targetEle)
            {
                return k;
            }
        }
        return -1;
    }

    static void Main()
    {
        int[] inputArr = { 12, 34, 10, 6, 40, 89, 98, 57, 19, 69 };
        int targetElement = 40;
        int s = inputArr.Length;

        int idx = findEle(inputArr, s, targetElement);
        if (idx != -1)
        {
            Console.WriteLine("el elemento se encuentra en la posicion: " + (idx + 1));
        }
        else
        {
            Console.WriteLine("no se encuentra el elemento");
        }
    }
}
