
"C:\Program Files\Microsoft SQL Server\150\DAC\bin\SqlPackage.exe" /Action:Publish /SourceFile:$Args[0]+"PersonalWebsite.Database.dacpac" /Profile:$Args[1] + "PersonalWebsite.Database.publish.xml";