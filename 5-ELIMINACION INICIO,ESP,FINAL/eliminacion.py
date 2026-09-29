original_arr=[11,21,31,41,51,61]

#liminacion al principio
print("eliminacion al inicio: ")
inputarr=list(original_arr)
position=0

print("antes: ",inputarr)
for f in range(position,len(inputarr)-1):
    inputarr[f]=inputarr[f+1]
inputarr.pop()
print("despues: ",inputarr)
print()
#eliminacion especifico
print("eliminacion especifico: ")
inputarr=list(original_arr)
position=3

print("antes: ",inputarr)
for f in range(position,len(inputarr)-1):
    inputarr[f]=inputarr[f+1]
inputarr.pop()
print("despues: ",inputarr)
print()


#eliminacion al final
print("eliminacion al final: ")
inputarr=list(original_arr)
print("antes: ",inputarr)
inputarr.pop()
print("despues: ",inputarr)
