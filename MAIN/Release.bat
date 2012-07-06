msbuild /t:clean;rebuild /p:Configuration=Release "RidoTasks.sln"
md Releases
cd Releases
md 0.7.2
cd 0.7.2
copy ..\..\trx2html\bin\release\trx2html.exe .
copy ..\..\RidoTasks\bin\release\RidoTasks.dll .
copy ..\..\RidoTasks.targets .
copy ..\..\CHANGELOG.txt .
cd ..
start .
cd ..