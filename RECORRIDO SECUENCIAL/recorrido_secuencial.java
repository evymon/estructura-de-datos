public class recorrido_secuencial

{
    public static void main(String[]args)
    {
        int arr[]={40,50,60,70,80,90};
        int size= arr.length;//calculando el tamaño del array
        System.out.println("Recorrido lineal(secuencial): ");
        System.out.print("los elementos del array son: ");
        for(int A=0; A<size;A++)
        {
            System.out.print(arr[A]+" ");
        }
    }

} 