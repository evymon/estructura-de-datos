using System;

class Program
{
    static int findEle(int[] arr, int l, int h, int targetValue)
    {
        while (l <= h)
        {
            int mid = l + (h - l) / 2;
            if (arr[mid] == targetValue)
            {
                return mid;
            }
            else if (arr[mid] < targetValue)
            {
                l = mid + 1;
            }
            else
            {
                h = mid - 1;
            }
        }
        return -1;
    }

    static void Main()
    {
        int[] inputArr = { 12, 34, 10, 6, 40, 89, 98, 57, 19, 69 };
        int targetElement = 40;
        int s = inputArr.Length;

        int idx = findEle(inputArr, 0, s - 1, targetElement);
        if (idx != -1)
        {
            Console.WriteLine("el elemento se encuentra en posicion: " + (idx + 1));
        }
        else
        {
            Console.WriteLine("el elemento no se encuentra:");
        }
    }
}
