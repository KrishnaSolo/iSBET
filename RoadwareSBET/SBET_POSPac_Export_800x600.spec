# -*- mode: python -*-
a = Analysis(['SBET_POSPac_Export_800x600.py'],
             pathex=['c:\\Repository\\Projects\\RoadwareSBET\\RoadwareSBET\\Data'],
             hiddenimports=[],
             hookspath=None,
             runtime_hooks=None)
pyz = PYZ(a.pure)
exe = EXE(pyz,
          a.scripts,
          exclude_binaries=True,
          name='SBET_POSPac_Export_800x600.exe',
          debug=False,
          strip=None,
          upx=True,
          console=True )
coll = COLLECT(exe,
               a.binaries,
               a.zipfiles,
               a.datas,
               strip=None,
               upx=True,
               name='SBET_POSPac_Export_800x600')
