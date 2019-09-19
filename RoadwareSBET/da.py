import sys,struct
sys.path.append(r'\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\ironpython\Lib')
import os, glob ,fnmatch, shutil, datetime    


outfile = open(r"C:\NY19_8512019\Data\Batch013\vrms_lv201905241020.out")

outdata = outfile.read()
print (type(outdata))
print (len(outdata))
field_names = ('time', 'latitude', 'longitude', 'altitude', \
          'x_vel', 'y_vel', 'z_vel', 
          'roll', 'pitch', 'platform_heading', 'wander_angle', 
          'x_acceleration', 'y_acceleration', 'z_acceleration',
          'x_angular_rate', 'y_angular_rate', 'z_angular')

values = struct.unpack('17d',outdata[0:8*17])
idk = dict(  zip(field_names, values) )
for k, v in idk.items():
    print (k, '-->', v)