using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using SynchServiceNS;
using System;


public class SynchronizationProvider
{
    private static Logging m_Logger = new Logging();
    public SynchronizationProvider(Logging logger)
    {
        m_Logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceDirectory"></param>
    /// <param name="destinationDirectory"></param>
    /// <param name="excludeFilterExtensions">e.g. desktop.ini</param>
    /// <param name="includeFilterExtensions">*.*</param>
    public void StartSynchronization(string sourceDirectory, string destinationDirectory, string[] excludeFilterExtensions, string[] includeFilterExtensions, string[] excludeSubDirectories, bool synchronizeBothWays=false)
    {

        try
        {
            // Set options for the synchronization operation
            FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges | FileSyncOptions.RecycleDeletedFiles | FileSyncOptions.RecyclePreviousFileOnUpdates | FileSyncOptions.RecycleConflictLoserFiles;

            FileSyncScopeFilter filter = new FileSyncScopeFilter();

            if (excludeFilterExtensions != null)
            {
                foreach (string f in excludeFilterExtensions)
                {
                    filter.FileNameExcludes.Add(f); // Exclude all *.lnk files
                }
            }

            if (includeFilterExtensions != null)
            {
                foreach (string f in includeFilterExtensions)
                {
                    filter.FileNameIncludes.Add(f); // default include all *.* files
                }
            }

            if (excludeSubDirectories != null)
            {
                foreach (string d in excludeSubDirectories)
                {
                    filter.SubdirectoryExcludes.Add(d); // default include all *.* files
                }
            }

            // Explicitly detect changes on both replicas upfront, to avoid two change
            // detection passes for the two-way synchronization

            DetectChangesOnFileSystemReplica(sourceDirectory, filter, options);
            if (synchronizeBothWays) DetectChangesOnFileSystemReplica(destinationDirectory, filter, options);

            // Synchronization in both directions
            SyncFileSystemReplicasOneWay(sourceDirectory, destinationDirectory, filter, options);

            if(synchronizeBothWays) SyncFileSystemReplicasOneWay(destinationDirectory, sourceDirectory, filter, options);
        }
        catch (Exception e)
        {
            WriteLine(LOG.ERROR, "\nException from File Synchronization Provider:\n" + e.ToString());
        }
    }

   
    public void DetectChangesOnFileSystemReplica(string replicaRootPath, FileSyncScopeFilter filter, FileSyncOptions options)
    {
        FileSyncProvider provider = null;

        try
        {
            provider = new FileSyncProvider(replicaRootPath, filter, options);
            provider.DetectChanges();
        }
        finally
        {
            // Release resources
            if (provider != null)
                provider.Dispose();
        }
    }

    public void SyncFileSystemReplicasOneWay(string sourceReplicaRootPath, string destinationReplicaRootPath, FileSyncScopeFilter filter, FileSyncOptions options)
    {
        FileSyncProvider sourceProvider = null;
        FileSyncProvider destinationProvider = null;

        try
        {
            sourceProvider = new FileSyncProvider(sourceReplicaRootPath, filter, options);
            destinationProvider = new FileSyncProvider(destinationReplicaRootPath, filter, options);

            destinationProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
            destinationProvider.SkippedChange += new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

            SyncOrchestrator agent = new SyncOrchestrator();
            agent.LocalProvider = sourceProvider;
            agent.RemoteProvider = destinationProvider;
            agent.Direction = SyncDirectionOrder.Upload; // Sync source to destination

            WriteLine(LOG.INFORMATION, "Synchronizing changes to replica: " + destinationProvider.RootDirectoryPath);
            agent.Synchronize();
        }
        finally
        {
            // Release resources
            if (sourceProvider != null) sourceProvider.Dispose();
            if (destinationProvider != null) destinationProvider.Dispose();
        }
    }

    public void OnAppliedChange(object sender, AppliedChangeEventArgs args)
    {
        switch (args.ChangeType)
        {
            case ChangeType.Create:
                WriteLine(LOG.INFORMATION, "-- Applied CREATE for file " + args.NewFilePath);
                break;
            case ChangeType.Delete:
                WriteLine(LOG.INFORMATION, "-- Applied DELETE for file " + args.OldFilePath);
                break;
            case ChangeType.Update:
                WriteLine(LOG.INFORMATION, "-- Applied OVERWRITE for file " + args.OldFilePath);
                break;
            case ChangeType.Rename:
                WriteLine(LOG.INFORMATION, "-- Applied RENAME for file " + args.OldFilePath + " as " + args.NewFilePath);
                break;
        }
    }

    public void OnSkippedChange(object sender, SkippedChangeEventArgs args)
    {
        WriteLine(LOG.INFORMATION, "-- Skipped applying " + args.ChangeType.ToString().ToUpper() + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ? args.CurrentFilePath : args.NewFilePath) + " due to error");

        if (args.Exception != null) Console.WriteLine("   [" + args.Exception.Message + "]");
    }

    private void WriteLine(LOG Level, string Message, bool bIgnoreLevel)
    {
        try
        {
            m_Logger.WriteToLog(Level, Message, bIgnoreLevel);
        }
        catch (Exception) { }
    }
    private void WriteLine(LOG Level, string Message)
    {
        WriteLine(Level, Message, false);
    }
}


