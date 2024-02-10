<p><h1>Synchronization Client and Windows Service</h1></p>
<p>&nbsp;</p>
<p>The project is based of <a href="https://www.microsoft.com/en-us/download/details.aspx?id=23217">Microsoft Synchronization Provider Framework</a>. The basic purpose is to synchronize a source folder at one or multiple locations.</p>

[Management Client]([https://via.placeholder.com/](https://github.com/KashifMushtaq/FileSync/sync1.jpeg)200x150 "Sync Manager")

<p><h2>Sub projects:</h2></p>

* SynchService (Windows Service x64)
* SynchServiceManager (.Net Framework Windows Forms Application)
* SynchSetup (Visual Studio Setup Project)

<p>&nbsp;</p>
<p><h2>Dependencies:</h2></p>

* Microsoft Visual Studio (2019 or later)
* Minimum .Net Framework 3.5
* ProviderServices-v2.1-x64-ENU.msi
* Synchronization-v2.1-x64-ENU.msi

<p>&nbsp;</p>
<p><h2>Prebuilt Installer:</h2></p>
<p>MSI installer can be downloaded from <b>SyncSerup\Release</b> folder. It installs the required dependencies and on uninstall, it also removes them.</p>

<p>&nbsp;</p>
<p><h2>Manual Steps After Installation:</h2></p>
<p>After installation, you must configure the service to run under some local admin user who has access to all the folder which you may want to synchronize (source and destinations). If you synchronize folders between local and network destination, the service context must have read and write access to source and destination folders. Please access the network path using \\HOST\NetworkShare and when you are prompted by the Password Window, please choose to save the password. Then use can use the same user account to run the Synchronization Service.</p>

<p>&nbsp;</p>
<p><h2>Synchronization Capabilities:</h2></p>
<p>The service and client program use Microsoft Synchronization Provider for Files to synchronize single source to multiple destinations:</p>
<p>&nbsp;</p>

1.	Local to multiple local destinations
2.	Local to multiple local and network destinations
3.	Local to multiple network destinations
4.	Network location to single or multiple local destination
5.	Network location to single or multiple network destinations

<p>Infect, It can synchronize from any location to any location provided it has access and read and write permissions.</p>

<p>The SynchService, can be configured to run the synchronization at preset time or after a certain interval. Just configure and forget service. It will keep the source and destination synchronized by itself.</p>

<p>It produces a log file and log levels could be set via management interface as well as job level.</p>
<p>You can add as many jobs you want.</p>
<p>Project’s source code can be cloned and compiled under GNU license. I do not take any responsibility what so ever.</p>

