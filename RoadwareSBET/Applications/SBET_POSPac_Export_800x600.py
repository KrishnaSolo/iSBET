#ARC GIS Automation Program
#Author: Andy Juram Lee
#Modified By: Frank Zhou
#Last Updated: 06/27/2014

import pywinauto, os, sys, time, easygui as eg, pdb

app = pywinauto.application.Application()

def main():
    SUCCESS_COUNT = 0
    nt =1
    print "SBET Export Automation"
    print "Author: Andy Juram Lee"
    print ""
    #Get file names and folder path from user
		#Changed -- file names and folder paths passed to application as parameters
    folder_path = sys.argv[1]
    #print folder_path
    #sys.exit()
    if len(sys.argv) == 3:
      (pospac_file_list) = find_pospac_files(folder_path)
    if len(sys.argv) == 4:
      pospac_file_list = [sys.argv[3] + '.pospac']

    #Open config.txt file to get the location of pospac.exe
    #try:
    #    f = open("Config.txt","r")
    #    POSPacLocation = f.readline()
    POSPacLocation = sys.argv[2]
    #except:
    #    eg.msgbox(msg = "Config.txt file cannot be found.\nPlease make sure you have config.exe file in same directory as automation executable with path to POSPac.exe", title = "Error!")
    #Check if POSPac.exe location is correct

    if os.path.exists(POSPacLocation)== False:
        #eg.msgbox(msg = "POSPac.exe file cannot be found. \nPlease check Config.txt to ensure correct location of POSPac.exe", title = "Error!")
        f.close()
        return
    #Output log file
    if folder_path[(len(folder_path))-1] == '\\':
        result_log_location = folder_path + 'Result_Log.txt'
    else:
        result_log_location = folder_path + '\Result_Log.txt'
    log = open(result_log_location,"w")
    log.writelines("POSPac SBET Automated export result log")
    log.writelines("\n")
    #app.Connect(title_re = "POSPac MMS")
    print "Total number of files in selected folder = ", len(pospac_file_list)
    for i in range (0,len(pospac_file_list)):
        print "Processing ", pospac_file_list[i]
        try:
            app.start_(POSPacLocation)
            time.sleep(7.5)
            app.POSPacMMS.MoveWindow(x=0,y=0,width=800,height=600)
            time.sleep(5)
            Auto_Export(pospac_file_list[i], folder_path)
            time.sleep(2)
            #app.kill_() #need change
            app.exit_()
            
            if app["Save Changes"].Exists():
                app["Save Changes"].Yes.ClickInput()
            else:
                pass

            SUCCESS_COUNT += 1
            print ("Successfully processed " + pospac_file_list[i])
            log.writelines("Successfully processed " + pospac_file_list[i])
            log.writelines("\n")
            time.sleep(5)
        except:
            print  ("****Unknown error occurred during processing " + pospac_file_list[i])
            log.writelines("****Unknown error occurred during processing " + pospac_file_list[i])
            log.writelines("\n")

            app.exit_() #need change
            time.sleep(10)
            #app.kill_()

            #if app["Save Changes"].Exists():
            #	app["Save Changes"].Yes.ClickInput()
            #else:
            #	pass

    print "Total number of files processed = ", SUCCESS_COUNT
    #f.close()
    log.close()
    #raw_input("Press Enter to exit...")
    sys.exit()
    
def find_pospac_files(folder_path):
    pospac_file_list = []
    file_list = []
    extention = ""
    #(folder_path) = eg.enterbox(msg = 'Please enter the location of POSPac files', title = 'VNAV & SBET Export Automation')
    if not os.path.exists(folder_path):
        #eg.msgbox(msg='Directory does not exist.',title='Warning')
        return pospac_file_list
    
    file_list = os.listdir(folder_path)

    for file_in_path in file_list:
        if file_in_path.split(".").count("pospac") == 1:
            pospac_file_list.append(file_in_path)
            #print file_in_path
        #elif file_in_path.split(".").count("POSPAC") == 1:
            #pospac_file_list.append(file_in_path)
            
    #return pospac_file_list, folder_path
    return pospac_file_list

def Auto_Export(filename, folder_path):
    
    filename_split = filename.split(".")
    #app.POSPacMMS.Wait(wait_for = "visible enabled ready", timeout = 10)
    #app.POSPacMMS.ClickInput(coords = (372,15))
    #time.sleep(2)
    app.POSPacMMS.Wait(wait_for = "visible enabled ready", timeout = 15)
    app.POSPacMMS.ClickInput(coords = (62,562))
    time.sleep(2)
    #Opening "file open" window in POSPac MMS
    app.POSPacMMS.ClickInput(coords = (52,67))
    #Input file location and name
    pospac_file = folder_path + "\\" + filename
    #print pospac_file
    #app.OpenFile.Edit1.TypeKeys(folder_path+"\\"+filename)
    app.OpenFile.Edit1.TypeKeys(pospac_file, with_spaces=True)
    app.OpenFile.Open.ClickInput()

    #Wait for file to open
    time.sleep(10)
    app["POSPac MMS"].WaitNot(wait_for_not = "exists", timeout = 45)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)

    #Click on the file and rename the mission name to the file name
    #app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (95,124),double = True)
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (84,121),double = True)
    time.sleep(1)
    #app["POSPac MMS - " + filename_split[0]].TypeKeys({F11})
    #time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (735,217),double = True)
    time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    app["POSPac MMS - " + filename_split[0]].TypeKeys("{BS 50}")
    time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].TypeKeys(filename_split[0])
    time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    #Click on close button after changing the mission name to file name
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (750,556),double = True)
    time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    #Click on save icon to save
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (72,68),double = True)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    time.sleep(1)
    
    #Click export icon
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords =(192,60), double = True)

    #**Export SBET
    time.sleep(2.5)
    
    #Click on Export format to change it to Custom Smoothed BET
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (759,218))
    time.sleep(1)
    #Choose Custom Smoothed BET by clicking
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (759,250))
    time.sleep(1)

    ##Click on Export format to change it to LACon
    #app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (759,218))
    #time.sleep(1)
    ##Choose LACon by clicking
    #app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (759,307))
    #time.sleep(1)

    #Click on export file name
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (638,161),double = True)
    time.sleep(1)
    #Go to the end of the file name
    app["POSPac MMS - " + filename_split[0]].TypeKeys("{END}")
    time.sleep(1)
    #Delete the export portion of the filename
    app["POSPac MMS - " + filename_split[0]].TypeKeys("{BS 170}")
    time.sleep(1)
    #Type sbet for file name
    app["POSPac MMS - " + filename_split[0]].TypeKeys(folder_path+"\\"+filename_split[0]+"\\"+filename_split[0]+"\\Export"+"\\sbet_"+filename_split[0]+".out", with_spaces=True)
    time.sleep(1)

#   section obsolete
#    #Click on Solution in use to change it to Real Time for vnav
#    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (680,341))
#    time.sleep(1)
#    #Choose real time by using up arrow key and enter
#    app["POSPac MMS - " + filename_split[0]].TypeKeys("{DOWN}")
#    time.sleep(1)
#    app["POSPac MMS - " + filename_split[0]].TypeKeys("{ENTER}")
#    time.sleep(1)

	#Click on save icon to save
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (72,68),double = True)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)

    #Click on Export icon to start export process for sbet

    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (667,556))
    time.sleep(1)
    if app["File Exists"].Exists():
        app["File Exists"].OK.ClickInput()
    else:
        pass
    time.sleep(1)
    if app["Orientation Not Transformed"].Exists():
        app["Orientation Not Transformed"].Yes.ClickInput()
    else:
        pass
    time.sleep(1)
    app.Export.WaitNot(wait_for_not = "visible ready", timeout = 1800) #used to be "visible enable ready exist" - wrong keywords
    time.sleep(1)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible ready", timeout = 850)
    time.sleep(1)
    #Click on save icon to save
    app["POSPac MMS - " + filename_split[0]].ClickInput(coords = (72,68),double = True)
    app["POSPac MMS - " + filename_split[0]].Wait(wait_for = "visible enabled ready exists", timeout = 45)
    time.sleep(1)

if __name__  == "__main__":
    main()
