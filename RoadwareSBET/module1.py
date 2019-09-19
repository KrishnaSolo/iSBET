import os, sys,datetime,glob,shutil, time

print "This is name of script: ",sys.argv[0]   
print "The arguments are: " , str(sys.argv)
time.sleep(122)
update = r"C:\Users\ksolanki\Documents\video-01\beta\bach002\posdata\20190101"
dest = r"C:\Users\ksolanki\Documents\video-01\Operations\SBETs\beta\ARAN83_Posdata\20190101"
dests = "C:\\Users\\ksolanki\\Documents\\video-01\\Operations\\SBETs\\beta\\"
df = int("20190101")
dt = int("20190120")
valid = []
valid = os.listdir(update)
logd = dests + "logs"
path =logd+"\\"+"copylog.txt"
with open(path,"w") as fil1:
    fil1.write("iSBET STATUS LOG: \n")
    fil1.write("\n")
    fil1.write("Copying...")
    fil1.write("\n")
    for i in valid:
        src = update 
        d = dest 
        fil1.write("Start Time: %s \n"%(str(datetime.datetime.now())))
        fil1.write("Source: %s \n"%(src))
        fil1.write("Destination: %s \n"%(d))
        os.system("robocopy %s %s %s /NFL /NDL /NJH /NJS /nc /ns /np " % (src, d, i))
        fil1.write("End Time: %s \n"%(str(datetime.datetime.now())))
        fil1.write("Status: Completed \n")
    fil1.write(" \n")
    fil1.write("Program complete")
    fil1.close()