function findEle (inputArr,s, targetEle){
    for (let k=0; k<s; k++){
        if (inputArr[k] == targetEle){
            return k;
        }
    }
    return -1
}

const inputArr=[12,34,10,6,89,40,98,57,69];
const targetElement = 40
const s =inputArr.length;

const idx =findEle(inputArr, s, targetElement);
if (idx !== -1){
    console.log("el elemento se encuentra en la posicion: " + (idx+1));
} else {
    console.log ("no se encontro ese numero: ");
}