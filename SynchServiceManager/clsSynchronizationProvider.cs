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

        FileSyncProvider sourceProvider = null;
        FileSyncProvider destinationProvider = null;
        SyncOrchestrator orchestrator = null;

        try
        {
            /*
             * CompareFileStreams	
             * If this value is set, the provider will compute a hash value for each file 
             * that is based on the contents of the whole file stream and use this value 
             * to compare files during change detection. This option is expensive and will 
             * slow synchronization, but provides more robust change detection. 
             * If this value is not set, an algorithm that compares modification times, 
             * file sizes, file names, and file attributes will be used to determine whether a file has changed.
             */

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


            sourceProvider = new FileSyncProvider(sourceDirectory, filter, options);
            destinationProvider = new FileSyncProvider(destinationDirectory, filter, options);


            sourceProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
            sourceProvider.SkippedChange += new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

            sourceProvider.DetectingChanges += new EventHandler<DetectingChangesEventArgs>(OnDetectingChanges);
            sourceProvider.DetectedChanges += new EventHandler<DetectedChangesEventArgs>(OnDetectedChanges);

            destinationProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
            destinationProvider.SkippedChange += new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

            destinationProvider.DetectingChanges += new EventHandler<DetectingChangesEventArgs>(OnDetectingChanges);
            destinationProvider.DetectedChanges += new EventHandler<DetectedChangesEventArgs>(OnDetectedChanges);


            orchestrator = new SyncOrchestrator();
            orchestrator.LocalProvider = sourceProvider;
            orchestrator.RemoteProvider = destinationProvider;


            if (synchronizeBothWays)
            {
                orchestrator.Direction = SyncDirectionOrder.DownloadAndUpload;
            }
            else
            {
                orchestrator.Direction = SyncDirectionOrder.Upload;
            }

            orchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(OnSessionProgress);
            orchestrator.StateChanged += new EventHandler<SyncOrchestratorStateChangedEventArgs>(OnStateChanged);

            var watch = System.Diagnostics.Stopwatch.StartNew();

            sourceProvider.DetectChanges();
            destinationProvider.DetectChanges();

            SyncOperationStatistics stats = orchestrator.Synchronize();

            // the code that you want to measure comes here
            watch.Stop();


            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  SyncStartTime [{0}]", stats.SyncStartTime), true);

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = watch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  Elapsed Time [{0}]", elapsedTime), true);

            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  UploadChangesTotal [{0}]", stats.UploadChangesTotal), true);
            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  UploadChangesFailed [{0}]", stats.UploadChangesFailed), true);
            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  UploadChangesApplied [{0}]", stats.UploadChangesApplied), true);
            
            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  DownloadChangesTotal [{0}]", stats.DownloadChangesTotal), true);
            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  DownloadChangesFailed [{0}]", stats.DownloadChangesFailed), true);
            WriteLine(LOG.INFORMATION, string.Format("SyncOperationStatistics -->  DownloadChangesApplied [{0}]", stats.DownloadChangesApplied), true);

        }
        catch (Exception e)
        {
            WriteLine(LOG.WARNING, "\nException from File Synchronization Provider:\n" + e.Message);
        }
        finally
        {
            // Release resources
            if (sourceProvider != null)
                sourceProvider.Dispose();

            if (destinationProvider != null)
                destinationProvider.Dispose();
        }
    }


    public void OnAppliedChange(object sender, AppliedChangeEventArgs args)
    {
        switch (args.ChangeType)
        {
            case ChangeType.Create:
                WriteLine(LOG.INFORMATION, "-- Applied CREATE for file " + args.NewFilePath, true);
                break;
            case ChangeType.Delete:
                WriteLine(LOG.INFORMATION, "-- Applied DELETE for file " + args.OldFilePath, true);
                break;
            case ChangeType.Update:
                WriteLine(LOG.INFORMATION, "-- Applied OVERWRITE for file " + args.OldFilePath, true);
                break;
            case ChangeType.Rename:
                WriteLine(LOG.INFORMATION, "-- Applied RENAME for file " + args.OldFilePath + " as " + args.NewFilePath, true);
                break;
        }
    }

    public void OnSkippedChange(object sender, SkippedChangeEventArgs args)
    {

        WriteLine(LOG.INFORMATION, string.Format("OnSkippedChange --> ChangeType [{0}]", args.ChangeType), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSkippedChange --> CurrentFilePath [{0}]", args.CurrentFilePath), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSkippedChange --> NewFilePath [{0}]", args.NewFilePath), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSkippedChange --> SkipReason [{0}]", args.SkipReason), true);

        WriteLine(LOG.INFORMATION, "-- Skipped applying " + args.ChangeType.ToString().ToUpper() + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ? args.CurrentFilePath : args.NewFilePath) + " due to ", true);

        if (args.Exception != null) WriteLine(LOG.INFORMATION, "   [" + args.Exception.Message + "]");
    }


    public void OnDetectingChanges(object sender, DetectingChangesEventArgs args)
    {
        WriteLine(LOG.INFORMATION, string.Format("OnDetectingChanges --> DirectoryPath [{0}]", args.DirectoryPath), true);
    }

    public void OnDetectedChanges(object sender, DetectedChangesEventArgs args)
    {
        WriteLine(LOG.INFORMATION, string.Format("OnDetectingChanges --> TotalDirectoriesFound [{0}]", args.TotalDirectoriesFound), true);
        WriteLine(LOG.INFORMATION, string.Format("OnDetectingChanges --> TotalFilesFound [{0}]", args.TotalFilesFound), true);
        WriteLine(LOG.INFORMATION, string.Format("OnDetectingChanges --> TotalFileSize [{0}]", args.TotalFileSize), true);
    }


    public void OnSessionProgress(object sender, SyncStagedProgressEventArgs args)
    {
        WriteLine(LOG.INFORMATION, string.Format("OnSessionProgress --> ReportingProvider [{0}]", args.ReportingProvider), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSessionProgress --> Stage [{0}]", args.Stage), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSessionProgress --> CompletedWork [{0}]", args.CompletedWork), true);
    }

    public void OnStateChanged(object sender, SyncOrchestratorStateChangedEventArgs args)
    {
        WriteLine(LOG.INFORMATION, string.Format("OnSessionProgress --> OldState [{0}]", args.OldState), true);
        WriteLine(LOG.INFORMATION, string.Format("OnSessionProgress --> NewState [{0}]", args.NewState), true);
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


