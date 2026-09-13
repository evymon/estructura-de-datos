const originalarr=[11,21,31,41,51,61];

console.log("eliminacion al inicio");
let inputarr=[...originalarr];
let position=0;

console.log("antes: ",inputarr);
for (let n=position;n<inputarr.length-1;n++){
    inputarr[n]=inputarr[n+1];
}
inputarr.pop();
console.log("despues: ",inputarr);
console.log("");

//eliminacion especifico
console.log("eliminacion especifico");
inputarr=[...originalarr];
position=3;
console.log("antes: ",inputarr);
for (let n=position;n<inputarr.length-1;n++){
    inputarr[n]=inputarr[n+1];
}   
inputarr.pop();
console.log("despues: ",inputarr);
console.log("");

//eliminacion al final
console.log("eliminacion al final");
inputarr=[...originalarr];
console.log("antes: ",inputarr);
inputarr.pop();
console.log("despues: ",inputarr);
