[http://paypal.github.io/PayPal-NET-SDK/](http://paypal.github.io/PayPal-NET-SDK/)


.NET Pizza Store App using PayPal REST APIs
===========================================


Overview
--------

The PizzaApp showcases the features of PayPal's REST APIs

   * Save Credit Card with PayPal for later Payments
   * Make Payments using the saved Credit Card ID
   * Make Payments using PayPal as the Payment Method


Pre-requisites
--------------

   * Visual Studio 2012 (MVC 3 ASPX targeting .NET Framework 4.5) 
   * [Note: Please check if MVC 3 templates are installed in Visual Studio 2012 if not download from MSDN]
		Or   
   * Visual Studio 2010 (MVC 3 ASPX targeting .NET Framework 4.0) 
   * [Note: Please check if MVC 3 templates are installed in Visual Studio 2010 if not download from MSDN]
		Or
   * Visual Studio 2012 (.NET Framework 4.5)
		Or
   * Visual Studio 2010 (.NET Framework 4.0)
		Or
   * Visual Studio 2008 (.NET Framework 3.5)
   * Nuget 2.2 or higher in case of NuGet Install
   * Note: NuGet 2.2 requires .NET Framework 4.0 or higher


Please note: bin and obj folders
---------------------------------------------

   * Please delete the bin and obj folders before switching between different versions of Visual Studio 


Please note: Web.config in Visual Studio 2008
---------------------------------------------

   * Please uncomment the following elements and their attributes in the Web.config file for Visual Studio 2008
	*	"pages"
	*	"httpHandlers"
	*	"httpModules"

Please note: SQLite
-------------------
   * Please ensure that the following folders with Interop dlls are added to your Visual Studio project solution 
	*	x64 - SQLite.Interop.dll
	*	x86 - SQLite.Interop.dll

Running the sample
------------------
   * Please use Visual Studio or IIS to run or host the samples 

Dependent library references
----------------------------
   * RestApiSDK.dll
   * PayPalCoreSDK.dll
   * Newtonsoft.Json.dll
   * log4net.dll
   * System.Data.SQLite.dll
   * System.Data.SQLite.Linq.dll
	
SDK Integration
---------------
   * Integrate PayPal REST API SDK with an ASP.NET Web Application
   * The NuGet package installs the dependencies to the solution and automatically updates the project in Visual Studio 2010 and 2012
   * Use NuGet.exe to install the dependencies in Visual Studio 2008

References
----------

   * REST API SDK repository - https://github.com/paypal/rest-api-sdk-dotnet.git


NuGet - Installing NuGet in Visual Studio 2010 and 2012
-------------------------------------------------------

Go to Visual Studio 2010 Menu --> Tools
Select Extension Manager
Enter NuGet in the search box and click Online Gallery
Let it Retrieve information
Select the retrieved NuGet Package Manager, click Download
Let it Download
Click Install on the Visual Studio Extension Installer NuGet Package Manager
Let it Install
Click Close and Restart Now

Go to Visual Studio 2010 Menu --> Tools, select Options
Verify the following on the Options popup
Click Package Manager --> Package Sources
Available package sources:
Check box (checked) NuGet official package source
https://nuget.org/api/v2/
Name: NuGet official package source
Source: https://nuget.org/api/v2/
And click OK
 
Go to Menu --> Tools --> Library Package Manage --> Package Manager Console
Select NuGet official package source from the Package source dropdown box in the Package Manager Console
Go to Solution Explorer and note the existing references
Enter at PM>
***************************************************

   * PM>Install-Package RestApiSDK
	*	RestApiSDK.dll
	* 	PayPalCoreSDK.dll
	* 	log4net.dll
	*	Newtonsoft.Json.dll

   * PM>Install-Package System.Data.SQLite
	*	System.Data.SQLite.dll
	*	System.Data.SQLite.Linq.dll

   * Note that the refrences get added automatically in Visual Studio 2012 and 2010
	
***************************************************

	
NuGet - Integrating NuGet with Visual Studio 2008
-------------------------------------------------

Prerequisites:
   * 	NuGet 2.2 or higher [Note: NuGet 2.2 or higher requires .NET Framework 4.0 or higher]
	
Check if .NET Framework 4.0 or higher is installed in the Computer from Control Panel -> Get programs

Or else

Run the following command from Windows Command Prompt:
wmic product where "Name like 'Microsoft .Net%'" get Name, Version
	
Please wait for the command to execute, it may take more than a minute to execute
*	Running the aforesaid command should list the .NET Framework versions installed as in this particular case 
*	[Please note the command might take a while to execute]

Name                                                Version
Microsoft .NET Compact Framework 1.0 SP3 Developer  1.0.4292
Microsoft .NET Framework 4.5                        4.5.50709
Microsoft .NET Framework 4.5 Multi-Targeting Pack   4.5.50709
Microsoft .NET Framework 2.0 SDK (x64) - ENU        2.0.50727
Microsoft .NET Framework 4 Multi-Targeting Pack     4.0.30319
Microsoft .NET Framework 4.5 SDK                    4.5.50709
Microsoft .NET Compact Framework 2.0 SP2            2.0.7045
Microsoft .NET Compact Framework 3.5                3.5.7283
Microsoft .NET Framework 1.1                        1.1.4322
Microsoft .NET Compact Framework 1.0 SP3            1.0.4294

Note: Most Windows machines may have .NET Framework 4.0 or higher installed as part of Windows (Recommended) Update

If V4.X is not installed, then download and install

*	.NET Framework 4 or higher (Standalone Installer) - (Free to download):
	http://www.microsoft.com/en-in/download/details.aspx?id=17718

Or else

*	.NET Framework 4 or higher (Web Installer) - (Free to download):
	http://www.microsoft.com/en-in/download/details.aspx?id=17851


Download NuGet.exe Command Line (free to download): http://nuget.codeplex.com/releases/view/58939

Save NuGet.exe to a folder viz., 'C:\NuGet' and add its path to the Environment Variables Path

Visual Studio 2008
Go to Visual Studio Menu --> Tools
Select External Tools
External Tools
External Tools having 5* default tools in the Menu contents, Click Add
   * Note: The number of default tools may differ depending on the particular Visual Studio installation
 
Enter the following:
Title: NuGet Install
Command (Having in Environment Variables Path): NuGet.exe
Arguments: Install your.package.name -OutputDirectory .\packages
Initial directory: $(SolutionDir)
Use Output window: Check
Prompt for arguments: Check
Click Apply
Click OK

On Clicking Apply and OK, NuGet Install will be added (as External Command 6*) to Menu --> Tools
   * Note : The External Command number may differ depending on the particular Visual Studio installation

Menu --> Tools, clicking NuGet Install will pop up for NuGet Install Arguments and Command Line
Also, NuGet Toolbar can be added, right-click on Visual Studio Menu and select
Customize by clicking New
Enter Toolbar name: NuGet and click OK
Check NuGet Checkbox in the Toolbars tab for NuGet Toolbar to pop up
Click Commands tab and select Tools and External Command 6 (Having added NuGet Install as External Command 6*) 
*Note: The External Command number may differ depending on the particular Visual Studio installation
Drag and drop External Command 6 to NuGet Toolbar
Right-click NuGet Toolbar
Enter Name: Install Package
Right-click Change Button Image and select an image
Close Customize
Drag and drop NuGet Toolbar to the Menu
Click the NuGet Toolbar Install Package
Clicking on the NuGet Toolbar Install Package will pop up for NuGet Install Arguments and Command Line
Example NuGet Install:
Enter Arguments:

***************************************************

Install RestApiSDK  -excludeversion -OutputDirectory .\packages
	
Install System.Data.SQLite  -excludeversion -OutputDirectory .\packages

***************************************************

	
