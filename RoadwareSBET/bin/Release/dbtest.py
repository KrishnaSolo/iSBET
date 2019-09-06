import sys,os
sys.path.append(r'\\video-01\Operations\SBETs\Apps\Python27\Lib\site-packages')
import pyodbc
import csv,glob,time

def main():
    
    #get data from main program 
    filterp = r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\filter.txt";
    try:
        if(os.path.exists(filterp)):
            pass
        else:
            return False
    except:
        return False
    
    #open up file and get data perline 
    f = open(filterp,'r')
    filelines = f.readlines()
    db = filelines[0] # eg- CO19_85122_Controls or CO19_85122
    batch = filelines[1] #eg - 035A or CTRL-ARAN47-Exit
    proj = filelines[2] #eg - CO19_85122
    dfrom = filelines[3] #eg - 22/05/2019
    dto = filelines[4] #eg - 23/05/2019
    aran = filelines[5] #eg - ARAN 43
    db = db.strip()
    batch=batch.strip()
    proj=proj.strip()
    dfrom=dfrom.strip()
    dto=dto.strip()
    aran=aran.strip()
    #we need to clean up batch and datef and datet to stg useable
    #start with batch - find len to splice string into parts we want
    n = 0
    n = len(batch)
    if (n >= 10): #identify if ctr-aran has entry and exit
        test = batch[0] + batch[1] + batch[2] + batch[3] + batch[4] + batch[5] + batch[6] + batch[7] + batch[8]
        #end = batch[n-5] + batch[n-4] + batch[n-3] + batch[n-2] + batch[n-1];
    elif (n > 3): # rmr test2 is for ctrl-aran batch names so need to keep full
	test = batch[0] + batch[1] + batch[2] + batch[3]
    else:
         test = ""

    check = batch[n-1]
    fbtch = batch

    if (test == "CTRL-ARAN"):
        batch = fbtch #assign batch if no entry and exit exists
        #if entry in batch we can check if path exists and assign simply the path else choose batch001
        if("Entry" in fbtch):
            path = "\\\\video-01\\" + proj + "\\"+fbtch
            try:
                 os.chdir(path)
                 batch = fbtch
            except:
                 batch = "Batch001" 
        #if exit in batch see if path exists - if not we need to assign the biggest batch file in the folder - eg batch099
        elif("Exit" in fbtch):
            path = "\\\\video-01\\" + proj + "\\"+fbtch
            try:
                os.chdir(path)
                batch = fbtch
            except:
                np = "\\\\video-01\\" + proj 
                os.chdir(np)
                #change dir to the project directory and get a list of the files in directory using glob
                it = glob.glob("Batch*")
                max = 0
                if (len(it) != 0):
                    for i in it:
                        try:#convert the last 3 digits of batch to get nunber
                            #get first one as you iterate through list check if new position you reached has bigger number
                            #keep if it does else move on
                             tp = i[5] + i[6] + i[7]
                             temp = int(tp)
                             if (max < temp):
                                max = temp
                        except:
                            pass
                    #convert to string and check if string is 2 digit or 1 digit since leading 0's will be lost 
                    #cat with appropriate prefix depending on length to get the largest batch name
                    maxs = str(max)
                    dig = len(maxs)
                    if (dig == 1):
                         batch ="Batch"+"0"+"0"+maxs 
                    elif(dig == 2):
                         batch ="Batch"+"0"+maxs   
            
    elif (check.isdigit()): #if not ctrl-aran etc we need to check last digit is letter if not continue like normal
        batch ="Batch" + batch[n-3]+batch[n-2]+batch[n-1]
    else: #if letter exsist as end char for batch need to get its prefix
        batch = "Batch" + batch[n-4] + batch[n-3] + batch[n-2]
    
    #parse date-from and date-to strings to get date format used in folder name - need to convert to an int in order
    #to get valid foldernames using datefrom to dateto 
    yearf = dfrom[8]+dfrom[9]
    yeart = dto[8]+dto[9]
    monthf = dfrom[3] + dfrom[4]
    monthT = dto[3] + dto[4]
    dayf = dfrom[0] + dfrom[1]
    dayT = dto[0] + dto[1]
    df = int(yearf + monthf +dayf)
    dt = int(yeart + monthT + dayT)

    #use data to create credentials to access sql DB
    server = 'ds-dpsql05'
    database = db
    username = 'rwg_segmenter'
    password = 'rwgincorp'

    #connect to db using pyodbc - adjust driver for different versions, cursor is your access object to run scripts
    try:
        cnxn = pyodbc.connect('DRIVER={ODBC Driver 13 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+password)
    except:
        return False
    cursor = cnxn.cursor()
    
    #script designed to extract a single list from given DB  of the filenames of missions
    name = batch
    script1 = """\
	    select pl.filename
  	    from dbo.PoslvLogFilenames pl
  	    where pl.filename like \'%"""+name+"""%\'order by pl.filename"""	
    
    #execute script using cursor object and use row element to get all the data
    cursor.execute(script1)
    row = cursor.fetchall()

    #open a log file to write a formated csv data file
    with open(r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\log.txt", "w") as f:
        csv.writer(f).writerows(row)
    
    #need to filter out files for main filename and not extension i.e- LV20190603.001 we only care about LV20190603 and get rid of repeated files
    #use a set since it has an O(1) contains runtime - O(n) worst case
    seen = set()         
    with open(r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\log.txt",'r') as in_file:
        #read from one files and write to second txt
        for line in in_file:
            n = len(line)
            test = ""
            #crop out the entire name to only get filename we want "LVYYYYMMDD"
            for x in range(n-20,n-6):
                test +=line[x]
            if test in seen: continue #check it contained otherwise move on
            #add it to set if unqiue
            seen.add(test)


    #we need to get a list of all the filenames existing in the current salesforce DB
    #need to iterate over the folders valid in the batch date range
    update = "\\\\video-01\\"+proj+"\\"+name+"\\posdata"
    valid = []
    #get all the valid foldernames which line up within the date range of the batch 
    for file in os.listdir(update):
        base,ext = os.path.splitext(file)
        try:
            num = int(base)
        except ValueError:
            return False
        if df <= num <= dt: #check if withing the date from and date to and add it to the list
            #fil1.write("Inside append file: "+str(num))
            valid.append(base)
    
    it = []
    p = [] #p will hold path for each filename to make it easy for copy
    for x in valid: #for each of folder in the list we need to get all the files in the folder in order to copy
        path2 = "\\\\video-01\\"+proj+"\\"+name+"\\posdata\\"+x
        try:
            os.chdir(path2)
        except:
            return False
        #get all the files in the dir
        a=[]
        a = glob.glob("*") 
        count = 0
        for i in a:
            path3 = path2 +"\\"+a[count]
            p.append(path3)
            count +=1
        it += a #add all the filenames to list to create a superlist since set from database is not date seperated
    skip = "\\\\video-01\\Operations\\SBETs\\"+proj+"\\"+proj+"-LOGS"+"\\"+proj+"-"+fbtch+".txt"
    with open(r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\match.txt", "w") as f1, open(skip,"a") as f2:
        cnt = 0 #keep track of iteration for p
	f2.write("\n")
        for i in it:
            c = len(i)
            ns = ''
            fail = ''
            for x in range(0,c-4): #get the filename so we can check if it exists in the set or not
                ns += i[x]
            for x in range(4,c-8): #second cat is to see if it valid for the date range of the batch
                fail += i[x]
            if ns in seen: #use set which has all unqiue filenames to see if matched and if not give filtered status
                fail = int(fail)
                if (df <= fail <= dt):
                    f1.write(p[cnt])
                    f1.write("\n")
            else: #all filtered files placed - logged only if in valid date range
                fail = int(fail)
                if (df <= fail <= dt):
                    f2.write("Skipped: %s" % p[cnt])
                    f2.write("\n")
            cnt += 1

    #close connection and destroy cursor object
    cursor.close()
    cnxn.close()
    return True

if __name__ == '__main__':
    
    check = main()
    if (check == False):
         with open(r"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\match.txt", "w") as f1:
             f1.write("Failed")
