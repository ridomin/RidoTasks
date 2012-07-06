msbuild /p:OutDir=.\..\tools\ RidoTasks.sln
del /Q trx2html.Test\TestFiles\*.htm
msbuild RidoTasks.targets.testproj
pause