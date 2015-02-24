@del *.exe
@for %%i in (Spawn OK TimeLimit MemoryLimit IdlenessLimit ZeroDiv AV OutputLimit ExitCode StdStreams Args) do g++ %%i.cpp -o %%i.exe
