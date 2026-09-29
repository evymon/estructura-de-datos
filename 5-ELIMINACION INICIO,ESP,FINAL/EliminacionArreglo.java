import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class EliminacionArreglo {
public static void main(String[]args){
    List<Integer> originalarr=Arrays.asList(11,21,31,41,51,61);


    //eliminaicon al inicio
    System.out.println("eliminacion al inicio");
    List<Integer> inputarr=new ArrayList<>(originalarr);
    int position=0;

    System.out.println("antes: " + inputarr);
    for(int k=position; k<inputarr.size()-1; k++){
        inputarr.set(k, inputarr.get(k+1));

    }
    inputarr.remove(inputarr.size()-1);
    System.out.println("despues: " + inputarr);
    System.out.println();


    //eliminacion especifico
    System.out.println("eliminaicon especifico");
    inputarr=new ArrayList<>(originalarr);
    position=3;
    System.out.println("antes: " + inputarr);

    for (int k= position; k<inputarr.size()-1;k++){
        inputarr.set(k,inputarr.get(k+1));

    }
    inputarr.remove(inputarr.size()-1);
    System.out.println("despues: " + position  + inputarr);
    System.out.println();
    
    System.out.println("eliminacion final: ");
    inputarr=new ArrayList<>(originalarr);

    System.out.println("antes: " + inputarr);
    inputarr.remove(inputarr.size() -1);
    System.out.println("despues: " + inputarr);
    }
}