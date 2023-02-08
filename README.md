## Task 4 details: ##

*Solution developed in Visual Studio 2022 v17.4.3 on Windows 10 (10.0.19045 Build 19045)

Please prepare your environment:
0. The latest versions of Chrome/Edge browsers
1. Visual Studio or Visual Studio Build Tools **Download/Install:** https://visualstudio.microsoft.com/downloads/ 
2. .NET 7 **Download/Install:** https://dotnet.microsoft.com/en-us/download/dotnet/7.0
3. NuGet.exe **Download/Install:** https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

[Tests run through IDE] Please use **'test.runsettings'** for run tests through IDE (e.g. in 'Visual Studio': menu "Test" -> "Configure Run Settings" -> "Select Solution Wide runsettings File" and select "test.runsettings" which is located in project folder.
Then please run tests through 'Test Explorer' ('Visual Studio': menu "Test" -> "Test Explorer")

[Tests run through console] Please specify **'test.runsettings'** for run tests using console (restore nuget packages and build solution before it):
In console: navigate to folder with artifacts after build (e.g. ...\SwapfietsTests\bin\x86\Debug\net7.0>) then use command for run: "vstest.console.exe SwapfietsTests.dll /Settings:"test.runsettings"

Test Automation Solution Description:
Framework based on/use: .NET 7 (C#), Selenium, WebDriverManager, Specflow, NUnit, ScreenRecorder
It has several layers: Feature/tests, Business objects (App pages, business logic aka step definitions), Utilities/Helpers.
Test run use test.runsettings (sensitive parameters values which should be written during run through CI pipeline, it leaved by default for local/manual run)
test.config has some kind of constant values - e.g. UR
Scenario described in Gherkin language and imitates each user action/behavior with the app
During TAF development I tried to follow principles: SOLID, KISS, YAGNI, DRY, AAA
Several design pattern used: Page Object, Singleton, Factory Method, Strategy, Facade, Wrapper, Builder...
In case of any questions please feel free to email me (andrei.shendrykau@gmail.com)