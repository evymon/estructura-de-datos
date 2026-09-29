#insercion al principio
inputarr=[11,21,31,41,51,61]
ele=52
print("antes de la insercion el array es: ");
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")
inputarr.insert(0, ele)
print("\ndespues de insertar al principio:")
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")


#insercion especifico
inputarr=[11,21,31,41,51,61]
print("\nantes de la insercion el array es: ");
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")
inputarr.insert(3, ele)
print("\ndespues de insertar en la posicion 3:")
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")


#insercion al final
inputarr=[11,21,31,41,51,61]
ele=72
print("\nantes de la insercion el array es: ");
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")
inputarr.append(ele)
print("\ndespues de insertar al final:")
for k in range(len(inputarr)):
    print(inputarr[k],end=" ")