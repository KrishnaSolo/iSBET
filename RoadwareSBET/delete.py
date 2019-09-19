#iSBET Delete module
#Author: Krishna Solanki
#Last Updated: 05/14/2019

import sys 
sys.path.append(r'\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\ironpython\Lib')
import os,glob ,fnmatch, shutil, datetime 

class Remover:
    def remove(self, proj, aran, batch):
        n = 0
        n = len(batch)
        fbtch = batch
        if (n >= 4):
            test = batch[0] + batch[1] + batch[2] + batch[3]
        else:
            test = ""
        check = batch[n-1]
        if (test == "CTRL"):
            test = batch
            batch = fbatch
        elif (check.isdigit()):
            batch = batch[n-3]+batch[n-2]+batch[n-1]
        else:
            batch = batch[n-4] + batch[n-3] + batch[n-2]
        
        n = 0
        n = len(proj)
        testP = proj
        proj = ''
        for y in range(28,n):
            proj += testP[y]

        logp = testP +"\\"+proj+"-LOGS"
        filename = proj + "-"+fbtch +".txt"
        if (os.path.isdir(logp)):
            pass
        else:
            logp = testP

        path = logp+"\\"+filename
        with open(path,'a') as fil1:
        #fil1 = open(filename,"a")
            fil1.write(" \n")
            fil1.write("PosPAC process was succesful for batch: %s \n" % (fbtch))
            stamp = "TimeStamp: "+str(datetime.datetime.now()) + " \n"
            fil1.write(stamp)
            n = 0
            n = len(aran)
            ar = aran[n-2] + aran[n-1]
            aran =  "ARAN" + ar + "_Posdata"

            updateD = testP + "\\" + aran
            fil1.write(" \n")
            fil1.write("Deleting... \n")
            try:
                shutil.rmtree(updateD)
                fil1.write("Delete was Succesful at %s \n" % (str(datetime.datetime.now())))
                fil1.write("/n")
 
            except:
                fil1.write("Exception- ", sys.exc_info() + " \n")
                fil1.write("Delete failed at %s \n" % (str(datetime.datetime.now())))
                return False
            
            fil1.write("\n")
            fil1.write("Program is Complete \n")
            fil1.close()

            return True #exit when done        