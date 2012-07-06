rd /S /Q TestResults
msbuild SampleReport/SampleReport.csproj
mstest /testcontainer:SampleReport\bin\debug\SampleReport.dll 
copy TestResults\*.trx SampleReport