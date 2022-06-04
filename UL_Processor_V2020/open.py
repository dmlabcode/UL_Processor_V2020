import pickle, json
print('HELLO')
# open a file, where you stored the pickled data
file = open("E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data_Denoised\\testyuLALA2.p", 'rb')\

# dump information to that file
data = pickle.load(file)

# close the file
file.close()

print('Showing the pickled data:')

cnt = 0
for item in data:
    print('The data ', cnt, ' is : ', item)
    cnt += 1

with open("E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data_Denoised\\testyuLALA2.p", 'rb') as fpick:
    with open("E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data_Denoised\\dd.json", 'w') as fjson:
        json.dump(pickle.load(fpick), fjson)