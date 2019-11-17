
// 
// This is a very simple sample to get started with AlphaVSS. The only thing it does
// is to enumerate any existing snapshots in the system and writing some basic
// information about them.
// 


using System;
using Alphaleonis.Win32.Vss;

namespace SnapshotQuery
{
	class Program
	{
		static void Main(string[] args)
		{
			IVssFactory vssImplementation = VssFactoryProvider.Default.GetVssFactory();
			using (IVssBackupComponents backup = vssImplementation.CreateVssBackupComponents())
			{
				backup.InitializeForBackup(null);

				backup.SetContext(VssSnapshotContext.All);
				
				foreach (VssSnapshotProperties prop in backup.QuerySnapshots())
				{
					Console.WriteLine("Snapshot ID: {0:B}", prop.SnapshotId);
					Console.WriteLine("Snapshot Set ID: {0:B}", prop.SnapshotSetId);
					Console.WriteLine("Original Volume Name: {0}", prop.OriginalVolumeName);
					Console.WriteLine();
				}
			}
		}
	}
}
