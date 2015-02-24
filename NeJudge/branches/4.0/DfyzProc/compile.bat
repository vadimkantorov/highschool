gcc -g test.c DfyzProc.c DfStrArr.c Internal.c Logging.c -lpsapi -o test.exe
cd Tests\
call compile.bat
cd ..
move test.exe Tests
cd Tests\
