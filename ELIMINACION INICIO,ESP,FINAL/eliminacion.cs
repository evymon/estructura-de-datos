using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void imprimirlista(List<int>arr)
    {
        Console.WriteLine("[" + string.Join(", ", arr) + "]");

    }
    static void Main()
    {
        List<int>originalarr=new List<int> { 11,21,31,41,51,61};
        Console.WriteLine("eliminacion al inicio");
        List<int> inputarr=new List<int>(originalarr);
        int position= 0;

        Console.Write("antes: ");
        imprimirlista(inputarr);

        for (int k= position; k < inputarr.Count -1;k++)
        {
            inputarr[k]= inputarr[k+1];
        }
        inputarr.RemoveAt(inputarr.Count -1);

        Console.Write("despues: ");
        imprimirlista(inputarr);
        Console.WriteLine();



        Console.WriteLine("eliminacion especifico: ");
        inputarr= new List<int>(originalarr);
        position=3;

        Console.Write("antes: ");
        imprimirlista(inputarr);


        for (int k=position; k<inputarr.Count-1; k++)
        {
           inputarr[k]=inputarr[k+1];
        }
            inputarr.RemoveAt(inputarr.Count-1);
            
            Console.WriteLine("despues: ");
            imprimirlista(inputarr);
            Console.WriteLine();
                    
             
             Console.WriteLine("eliminacion final: ");
             inputarr=new List<int>(originalarr);

             Console.Write("antes: ");
             imprimirlista(inputarr);
             inputarr.RemoveAt(inputarr.Count-1);
             Console.Write("despues :");
             imprimirlista(inputarr);
    }
}