#include <iostream>
#include <vector>

void imprimirvector(const std::vector<int>& arr) {
    std::cout<<"[";
    for (size_t u=0; u<arr.size();++u){
        std::cout<<arr[u];
        if(u<arr.size()-1) std::cout<<", ";
    }
    std::cout<<"]"<<std::endl;
}

int main() {
    std::vector<int> originalarr = {11,21,31,41,51,61};

    //eliminacion al inicio
    std::cout<<"eliminacion al inicio"<<std::endl;
    std::vector<int>inputarr=originalarr;
    int posicion=0;
    std::cout <<("antes: ");
    imprimirvector(inputarr);

    for(size_t i=posicion; i<inputarr.size()-1; ++i){
        inputarr[i]=inputarr[i+1];
    }
    inputarr.pop_back();
    std::cout <<("despues: ");
    imprimirvector(inputarr);
    std::cout<<std::endl;


    //eliminacion especifico
    std::cout<<"eliminacion especifico"<<std::endl;
    inputarr=originalarr;
    posicion=2; 
    std::cout <<("antes: ");
    imprimirvector(inputarr);
    for(size_t i=posicion; i<inputarr.size()-1; ++i){
        inputarr[i]=inputarr[i+1];
    }   
    inputarr.pop_back();
    std::cout <<("despues: ");
    imprimirvector(inputarr);
    std::cout<<std::endl;

    //eliminacion al final
    std::cout<<"eliminacion al final"<<std::endl;
    inputarr=originalarr;
    std::cout <<("antes: ");
    imprimirvector(inputarr);
    inputarr.pop_back();
    std::cout <<("despues: ");
    imprimirvector(inputarr);
    return 0;
}
