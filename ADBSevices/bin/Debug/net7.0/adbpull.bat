@echo off
adb shell ls /data/data/com.gimica.puzzlepopblaster / > filelist.txt
for /F "delims=" %%f in (filelist.txt) do (
    if not "%%f"=="frc_1:997874111989:android:f21130c4457215122bfcbc_firebase_settings.xml" (
        adb pull /data/data/com.gimica.puzzlepopblaster /%%f ./adb_pull/
    ) else (
        echo Skipping %%f due to invalid format
    )
)
echo Done!
