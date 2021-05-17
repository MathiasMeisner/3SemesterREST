#from sense_hat import SenseHat
from time import sleep
import time
from socket import * 
from datetime import date
import random
import string
#sense = SenseHat()

colorlist = ["Pink","Crimson","Red", "Maroon","Brown","Misty-Rose","Salmon","Coral", "Orange-Red","Chocolate","Orange","Gold","Ivory",
"Yellow","Olive","Yellow-Green","Lawn-green","Chartreuse","Lime","Green","Spring-green","Aquamarine","Turquoise","Azure","Aqua/Cyan",
"Teal","Lavender","Blue","Navy","Blue-Violet","Indigo","Dark-Violet","Plum","Magenta","Purple","Red-Violet", 	 	 	 	 	 	 	 	 	 	 	 	 	 	 	 	 	 
"Tan","Beige","Slate-gray","Dark-Slate-Gray","White", "White-Smoke","Light-Gray","Silver","Dark-Gray","Gray","Dim-Gray","Black"]
# denne list er hentet fra https://simple.wikipedia.org/wiki/List_of_colors

letters = string.ascii_uppercase
number = string.digits

s = socket(AF_INET, SOCK_DGRAM)
BROADCAST_TO_PORT = 9999
s.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)
TotalCountCar = 1
TotalParking = 0
#sense.clear(0,255,0)
isEmpty = True
while isEmpty:
    randomcolor = random.choice(colorlist)
    
    letters_str = ''.join(random.choice(letters) for i in range(2))

    number_str = ''.join(random.choice(number) for i in range(5))

    LicensePlate = letters_str + number_str

    ISin = 1
    today = date.today()
    messageToUDP = randomcolor + " " + str(ISin) + " " + str(today) + " " + LicensePlate 
    s.sendto(bytes(messageToUDP,"UTF-8"), ('<broadcast>', BROADCAST_TO_PORT))
    
    TotalCountCar += 1
    TotalParking += 1
    #if TotalParking == 30:
        #sense.clear(255,0,0)
    if TotalCountCar > 30:
        isEmpty = False
    
    print(messageToUDP)   
    sleep(2)   
