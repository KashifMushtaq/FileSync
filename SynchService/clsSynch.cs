using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Synchronization.Files;
using Microsoft.Synchronization;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SynchService
{
    public class clsSynch
    {

        private static Logging m_Logger = new Logging();
        private static clsArguments m_arg;
        public static string m_CurrentFile=string.Empty;
        public static int m_CurrentFilePercentage=0;
        public enum SynchType : int
        {
            SourceToDestinationSynch = 1,
            BothWaysSynch = 2,
            DestinationsToSourceSynch = 3
        }

        /*
         * Creates a thread for a job
         */
        public static void RunJob(clsArguments arg)
        {
            ParameterizedThreadStart pThread = new ParameterizedThreadStart(SynchThread);
            Thread thread = new Thread(pThread);
            thread.Name = arg.JobName;

            m_Logger.LogLevel = int.Parse(arg.LogLevel);

            thread.Start(arg);
        }
        public static void SyncFileSystemReplicasOneWay(string sourceReplicaRootPath, string destinationReplicaRootPath, FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destinationProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(sourceReplicaRootPath, filter, options);

                destinationProvider = new FileSyncProvider(destinationReplicaRootPath, filter, options);

                destinationProvider.DetectedChanges += new EventHandler<DetectedChangesEventArgs>(OnDetectedChanges);
                destinationProvider.CopyingFile += new EventHandler<CopyingFileEventArgs>(OnCopyingFile);
                destinationProvider.ApplyingChange += new EventHandler<ApplyingChangeEventArgs>(OnApplyingChangeDest);
                destinationProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
                destinationProvider.SkippedChange += new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destinationProvider;
                agent.Direction = SyncDirectionOrder.Upload; // Synchronize source to destination

                WriteLine(LOG.INFORMATION, "Synchronizing changes to replica: " + destinationProvider.RootDirectoryPath);
                agent.Synchronize();
            }
            catch(Exception ex)
            {
                WriteLine(LOG.ERROR, string.Format("SyncFileSystemReplicasOneWay ->{0}", ex.Message));
            }
            finally
            {
                // Release resources
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destinationProvider != null) destinationProvider.Dispose();
            }
        }

        public static void DetectChangesOnFileSystemReplica(string replicaRootPath, FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider provider = null;

            try
            {
                provider = new FileSyncProvider(replicaRootPath, filter, options);
                provider.DetectChanges();
            }
            catch (Exception ex)
            {

                WriteLine(LOG.ERROR, string.Format("DetectChangesOnFileSystemReplica -> {0}", ex.Message));
            }
            finally
            {
                // Release resources
                if (provider != null)
                    provider.Dispose();
            }
        }
        public static void OnDetectedChanges(object sender, DetectedChangesEventArgs args)
        {
            try
            {
                WriteLine(LOG.INFORMATION, string.Format("Detected Changes Dir [{0}], Files [{1}] Total File Size [{2}]", args.TotalDirectoriesFound, args.TotalFilesFound, args.TotalFileSize));
            }
            catch (Exception ex)
            {

                WriteLine(LOG.ERROR, string.Format("OnDetectedChanges ->{0}", ex.Message));
            }
        }
        public static void OnCopyingFile(object sender, CopyingFileEventArgs args)
        {
            try
            {
                //WriteLine(LOG.INFORMATION, string.Format("Copying File [{0}] Percentage [{1}]", args.FilePath, args.PercentCopied));
                m_CurrentFile = args.FilePath;
                m_CurrentFilePercentage = args.PercentCopied;
            }
            catch (Exception ex)
            {

                WriteLine(LOG.ERROR, string.Format("OnCopyingFile ->{0}", ex.Message));
            }
        }
        public static void OnApplyingChangeDest(object sender, ApplyingChangeEventArgs args)
        {
            try
            {
                if (m_arg.DirFiltersEx != null && args!=null)
                {
                    string[] subdirectoryExcludes = m_arg.DirFiltersEx;

                    switch (args.ChangeType)
                    {
                        case ChangeType.Create:
                            foreach (string sFilter in subdirectoryExcludes)
                            {
                                Regex regex = new Regex(sFilter, RegexOptions.IgnoreCase);
                                if (regex.IsMatch(args.NewFileData.RelativePath))
                                {
                                    WriteLine(LOG.INFORMATION, string.Format("Skipping Create Dir [{0}]. Sub dir in exclusion filter", args.NewFileData.RelativePath));
                                    args.SkipChange = true;
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                
                WriteLine(LOG.ERROR, string.Format("OnApplyingChangeDest ->{0}", ex.Message));
            }


        }
        public static void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    WriteLine(LOG.INFORMATION, "CREATE -> " + args.NewFilePath);
                    break;
                case ChangeType.Delete:
                    WriteLine(LOG.INFORMATION, "DELETE -> " + args.OldFilePath);
                    break;
                case ChangeType.Update:
                    WriteLine(LOG.INFORMATION, "UPDATE -> " + args.OldFilePath);
                    break;
                case ChangeType.Rename:
                    WriteLine(LOG.INFORMATION, "RENAME -> " + args.OldFilePath + " as " + args.NewFilePath);
                    break;
            }
        }

        public static void OnSkippedChange(object sender, SkippedChangeEventArgs args)
        {
            WriteLine(LOG.INFORMATION, "Skipped applying " + args.ChangeType.ToString().ToUpper()
                + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ? args.CurrentFilePath : args.NewFilePath));

            if (args.Exception != null)
                WriteLine(LOG.INFORMATION, "   [" + args.Exception.Message + "]");
        }

        private static void SynchThread(object arguments)
        {
            try
            {
                clsArguments arg = (clsArguments)arguments;
                m_arg = arg;

                // Set options for the synchronization operation
                FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                                            FileSyncOptions.RecycleDeletedFiles |
                                            FileSyncOptions.RecyclePreviousFileOnUpdates |
                                            FileSyncOptions.RecycleConflictLoserFiles;


                FileSyncScopeFilter filter = new FileSyncScopeFilter();

                if (arg.FileFiltersEx != null)
                {
                    //add filter for file must be excluded
                    foreach (string val in arg.FileFiltersEx)
                    {
                        filter.FileNameExcludes.Add(val);
                    }
                }

                if (arg.FileFiltersIn != null)
                {
                    //add filter for file must be included
                    foreach (string val in arg.FileFiltersIn)
                    {
                        filter.FileNameIncludes.Add(val);
                    }
                }

                if (arg.DirFiltersEx != null)
                {
                    //add filter for sub dir must be excluded
                    foreach (string val in arg.DirFiltersEx)
                    {
                        filter.SubdirectoryExcludes.Add(val);
                    }
                }

                string replica1RootPath = arg.JobSource;

                foreach (string replica2RootPath in arg.JobDestinations)
                {

                    if (int.Parse(arg.SynchType) == (int)SynchType.SourceToDestinationSynch) // source to destination
                    {
                        DetectChangesOnFileSystemReplica(replica1RootPath, filter, options);

                        SyncFileSystemReplicasOneWay(replica1RootPath, replica2RootPath, filter, options);
                    }
                    else if (int.Parse(arg.SynchType) == (int)SynchType.BothWaysSynch) // bothways
                    {
                        // Explicitly detect changes on both replicas upfront, to avoid two change
                        // detection passes for the two-way synchronization
                        DetectChangesOnFileSystemReplica(replica1RootPath, filter, options);
                        DetectChangesOnFileSystemReplica(replica2RootPath, filter, options);

                        // Synchronization in both directions
                        SyncFileSystemReplicasOneWay(replica1RootPath, replica2RootPath, filter, options);
                        SyncFileSystemReplicasOneWay(replica2RootPath, replica1RootPath, filter, options);
                    }
                    else if (int.Parse(arg.SynchType) == (int)SynchType.DestinationsToSourceSynch) // Destinations to source
                    {
                        DetectChangesOnFileSystemReplica(replica2RootPath, filter, options);

                        SyncFileSystemReplicasOneWay(replica2RootPath, replica1RootPath, filter, options);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine(LOG.ERROR, String.Format("SynchThread -> {0}", ex.Message));
            }
        }
        private static void WriteLine(LOG Level, string Message)
        {

            try
            {
                m_Logger.WriteToLog(Level, Message);
            }
            catch (Exception) { }
        }
    }
}
