#iSBET Copy module
#Author: Krishna Solanki
#Last Updated: 05/14/2019

#module
import sys
sys.path.append(r'\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\ironpython\Lib')
import os, glob ,fnmatch, shutil, datetime    

class Copier:
    def automate(self,batch, proj,aran, dfrom, dto):
        factor = False
        matchp = r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\match.txt"
        filter = []
        try:
            if(os.path.exists(matchp)):
                fopen = open(matchp,'r')
                fr = fopen.readlines()
                
                if (len(fr) != 0):
                    if (fr[0] == "Failed"):
                        factor = False
                    else:
                        for x in fr:
                            s = x.strip()
                            filter.append(s)
                        factor = True
                else:
                    factor = False
            else:
                factor = False
        except:
            factor = False
    
        start = "\\\\video-01\\Operations\\SBETs"
        
        n = 0
        n = len(aran)
        ar = aran[n-2] + aran[n-1]

        n = 0
        n = len(proj)
        testP = proj
        proj = ''
        for y in range(28,n):
            proj += testP[y]

        n = 0
        n = len(batch)
        if (n >= 10):#if it just has ctrl get last 3 numbers and you are good
            test = batch[0] + batch[1] + batch[2] + batch[3] + batch[4] + batch[5] + batch[6] + batch[7] + batch[8]
            end = batch[n-5] + batch[n-4] + batch[n-3] + batch[n-2] + batch[n-1];
        elif (n >= 4 & n<10): # rmr test2 is for ctrl-aran batch names so need to keep full
            test = batch[0] + batch[1] + batch[2] + batch[3]

        else:
            test = ""

        check = batch[n-1]
        fbtch = batch

        if (test == "CTRL-ARAN"):
            batch = fbtch

            if("Entry" in fbtch):
                path = "\\\\video-01\\" + proj + "\\"+fbtch
                try:
                   os.chdir(path)
                   batch = fbtch
                except:
                    batch = "Batch001" 

            elif("Exit" in fbtch):
                path = "\\\\video-01\\" + proj + "\\"+fbtch
                try:
                   os.chdir(path)
                   batch = fbtch
                except:
                    np = "\\\\video-01\\" + proj 
                    os.chdir(np)
                    it = glob.glob("Batch*")
                    max = 0
                    if (len(it) != 0):
                        for i in it:
                            try:
                                tp = i[5] + i[6] + i[7]
                                temp = int(tp)
                                if (max < temp):
                                    max = temp
                            except:
                                pass
                    maxs = str(max)
                    dig = len(maxs)
                    if (dig == 1):
                         batch ="Batch"+"0"+"0"+maxs 
                    elif(dig == 2):
                         batch ="Batch"+"0"+maxs   
            
        elif (check.isdigit()):
            batch = batch[n-3]+batch[n-2]+batch[n-1]
        else:
            batch = batch[n-4] + batch[n-3] + batch[n-2]

        

        
        yearf = dfrom[8]+dfrom[9]
        yeart = dto[8]+dto[9]
        if (dfrom[3] == 0):
            monthf = dfrom[3] + dfrom[4]
        else:
            monthf = dfrom[3] + dfrom[4]
        
        if (dto[3] == 0):
            monthT = dto[3] + dto[4]
        else:
            monthT = dto[3] + dto[4]

        if (dfrom[0] == 0):
            dayf = dfrom[0] + dfrom[1]
        else:
            dayf = dfrom[0] + dfrom[1]
        
        if (dto[0] == 0):
            dayT = dto[0]+dto[1]
        else:
            dayT = dto[0] + dto[1]
        
        filename = proj + "-"+fbtch +".txt"
        #fil1 = open(filename,"w")
        logp = testP +"\\"+proj+"-LOGS"
        if (os.path.isdir(logp)):
            pass
        else:
            try:
                os.mkdir(logp)
            except:
                logp = testP

        path = logp+"\\"+filename
        with open(path,'w') as fil1:
            #fil1.write("LOG: CHECK FOR DIRECTORY ISSUES \n")
            #stamp = "TimeStamp: "+str(datetime.datetime.now()) + " \n"
            #fil1.write(stamp)
            InfoProj = "ProjectName: " + proj + " \n"
            InfoARAN = "ARAN#: " + ar+ " \n"
            InfoBtch = "Batch#: " + batch+ " \n"
            Infofrm = "Date of Collecion From: "+dfrom+ " \n"
            Infoto = "Date of Collection To: "+dto+ " \n"
            #fil1.write("\n")
            #fil1.write(InfoProj)
            #fil1.write(InfoARAN)
            #fil1.write(InfoBtch)
            fil1.write(Infofrm)
            fil1.write(Infoto)
            basepath = "\\\\video-01\\" + proj

            try:
                 os.chdir(basepath)
            except:
                fil1.write("Could not enter directory. Exception- ", sys.exc_info() +" \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                return False

            fil1.write("Copying... \n")

            batch = "*" + batch #only care about if the batch is at end of directory name
            bach = glob.glob(batch) #search for matching batch
    
            if (len(bach)==0): #make sure a match is found
                fil1.write("Error: Bach not found \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                #sys.exit()
                return False
            
            else:
                 print (bach[0])
                 temp = bach[0]

            update = basepath + "\\" + temp #update path

            #go in to directory
            try:
                 os.chdir(update)
            except:
                fil1.write("Could not enter directory. Exception- ", sys.exc_info() +" \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                return False

            #fil1.write("You are now in "+ update + " \n")
            data = glob.glob('posdata')
    
            if (len(data)==0):
                fil1.write("Error: posdata not found \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                #sys.exit()
                return False
            
            else:
                 print (data[0])
                 temp = data[0]

            update = update + "\\" + temp

            try:
                 os.chdir(update)
            except:
                fil1.write("Could not enter directory. Exception- ", sys.exc_info() +" \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                return False
    
            #fil1.write("Source path is set to: "+ update + " \n")

            df = int(yearf + monthf +dayf)
            dt = int(yeart + monthT + dayT)
            valid = []
            for file in os.listdir(update):
               base,ext = os.path.splitext(file)
               try:
                   num = int(base)
               except ValueError:
                   fil1.write("Exception- ",sys.exc_info()+" \n")
                   fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                   return False
               
               if df <= num <= dt:
                   #fil1.write("Inside append file: "+str(num))
                   valid.append(base)

            Mvpath = testP
            try:
                 os.chdir(Mvpath)
            except:
                fil1.write("Could not enter directory. Exception- ", sys.exc_info() +" \n")
                fil1.write("Time: %s \n" % (str(datetime.datetime.now())))
                return False
        

            #fil1.write("Starting in "+ Mvpath + " \n")
        
            aranp =  "ARAN" + ar + "_Posdata" 
            #aranD = glob.glob(aranp)

            #once in the last folder will look for ARANXX_Posdata to see if it exists - if it does it deletes it and will create a new folder 
            #else if no folder found, later on it will create one for the user - the reason stems from the limited functionality of shutils copy tool
            #if (len(aranD) != 0):
             #   try:
              #      check = updateD +"\\"+ aranp
               #     shutil.rmtree(check) # here we remove existing directory if created
 
                #except:
                 #   print ("Could not Remove directory. Exception- ", sys.exc_info())
                  #  print ("Please ensure ARAN directory is empty")
                   # sys.exit()
         

            updateD = Mvpath + "\\" + aranp


            #fil1.write("Pre Destination is set to: "+ updateD+" \n")

            if ((updateD != Mvpath) & (update != basepath) & (factor == False)): #another err check to ensure source and dest are not the base strings we made initially
            
                #the following will actually do the copy process from both folders - note copy will create a directory with dest name
                #hence why we deleted an instance before
                fil1.write(" \n")
                fail =0
                for i in valid:
                    try:
                        print("Copying files in progress")
                        src = update + "\\" + i
                        dest = updateD + "\\" + i
                        fil1.write("Start Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Source: %s \n"%(src))
                        fil1.write("Destination: %s \n"%(dest))
                        os.system("robocopy %s %s /S /MT:32 /NFL /NDL /NJH /NJS /nc /ns /np " % (src, dest))
                        fil1.write("End Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Status: Completed \n")
                        fil1.write("\n")
                
                    except:
                        fil1.write("End Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Status: Failed - ", sys.exc_info() +" \n")
                        fil1.write("\n")
                        fail +=1

                fil1.write("Files Transfer Completed at %s \n" % (str(datetime.datetime.now())))
                fil1.close()
                if (fail == 0):
                    return True
                elif (fail >0):
                    return False

            elif ((updateD != Mvpath) & (update != basepath) & (factor == True)): #if factor is true that means filter was succesful so we can proceed

                fil1.write(" \n")
                fail =0
                for i in filter:
                    try:
                        print("Copying files in progress")
                        n=0
                        n = len(i)
                        id = ""
                        ip = ""
                        for x in range(n-18,n):
                            ip += i[x]
                        for x in range(n-25,n-19):
                            id += i[x]
                        src = update + "\\" + id
                        dest = updateD + "\\" + id
                        fil1.write("Start Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Source: %s \n"%(i))
                        fil1.write("Destination: %s \n"%(dest))
                        os.system("robocopy %s %s %s /NFL /NDL /NJH /NJS /nc /ns /np " % (src, dest, ip))
                        fil1.write("End Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Status: Completed \n")
                        fil1.write("\n")
                
                    except:
                        fil1.write("End Time: %s \n"%(str(datetime.datetime.now())))
                        fil1.write("Status: Failed - ", sys.exc_info() +" \n")
                        fil1.write("\n")
                        fail +=1

                fil1.write("Files Transfer Completed at %s \n" % (str(datetime.datetime.now())))
                fil1.close()
                if (fail == 0):
                    return True
                elif (fail >0):
                    return False