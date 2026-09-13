import java.util.ArrayList;
import java.util.Arrays;

public class insercion {
    public static void main(String[] args) {
        int ele=52;

        //insercion al principio
        ArrayList<Integer> arr1 = new ArrayList<>(Arrays.asList(11,21,31,41,51));
        System.out.println("antes: " + arr1);
        arr1.add(0, ele);
        System.out.println("despues: " + arr1);

        //insercion especfica
        ArrayList<Integer> arr2 = new ArrayList<>(Arrays.asList(11,21,31,41,51));
        arr2.add(3, ele);
        System.out.println("despues: " + arr2);

        //insercion al final
        ArrayList<Integer> arr3 = new ArrayList<>(Arrays.asList(11,21,31,41,51));
        ele=72;
        arr3.add(ele);
        System.out.println("despues: " + arr3);
    }   
}