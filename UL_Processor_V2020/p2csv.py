import pickle
import json
import sys
import os
#import pickle as pkl
import pandas # as pd
import csv
# dump information to that file
 


file = open(sys.argv[1], 'rb')
data = pickle.load(file)

# close the file
file.close()
print('hello')
for sub, sdata in data["classroom_observation_dataset"].items():
     print(sub)
     sdata.to_csv('{}_{}.csv'.format(sub, sub), index=False)

 