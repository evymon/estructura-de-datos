using System;
using System.Collections.Generic;

class Program{
    static void Main(){
    int ele=52;

    //insercion al principio
    List<int> arr1= new List<int>{11,21,31,41,51,61};
    Console.WriteLine("antes: "+string.Join(",",arr1));
    arr1.Insert(0,ele);
    Console.WriteLine("despues: "+string.Join(",",arr1));

    //insercion especifico
    List<int> arr2= new List<int>{11,21,31,41,51,61};
    arr2.Insert(3,ele);
    Console.WriteLine("ahora: "+string.Join(",",arr2));

    //insercion al final
    List<int> arr3= new List<int>{11,21,31,41,51,61};
    int eleFinal=72;
    arr3.Add(eleFinal);
    Console.WriteLine("final: "+string.Join(",",arr3));
    }
}