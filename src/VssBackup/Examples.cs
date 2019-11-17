using Alphaleonis.Win32.Filesystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stream = System.IO.Stream;

namespace VssSample
{
   public class Examples
   {
      static void Example1()
      {
         /// <summary>
         /// This class encapsulates some simple VSS logic.  Its goal is to allow
         /// a user to backup a single file from a shadow copy (presumably because
         /// that file is otherwise unavailable on its home volume).
         /// </summary>
         /// <example>
         /// This code creates a shadow copy and copies a single file from
         /// the new snapshot to a location on the D drive.  Here we're
         /// using the AlphaFS library to make a full-file copy of the file.
         /// <code>
         string source_file = @"C:\Windows\system32\config\sam";
         string backup_root = @"D:\Backups";
         string backup_path = Path.Combine(backup_root, Path.GetFileName(source_file));

         // Initialize the shadow copy subsystem.
         using (VssBackup vss = new VssBackup())
         {
            vss.Setup(Path.GetPathRoot(source_file));
            string snap_path = vss.GetSnapshotPath(source_file);

            // Here we use the AlphaFS library to make the copy.
            Alphaleonis.Win32.Filesystem.File.Copy(snap_path, backup_path);
         }
      }

      static void Example2()
      {
          //This code creates a shadow copy and opens a stream over a file
          //on the new snapshot volume.
          
          string filename = @"C:\Windows\system32\config\sam";

         // Initialize the shadow copy subsystem.
         using (VssBackup vss = new VssBackup())
         {
            vss.Setup(Path.GetPathRoot(filename));

            // We can now access the shadow copy by either retrieving a stream:
            using (Stream s = vss.GetStream(filename))
            {
               Debug.Assert(s.CanRead == true);
               Debug.Assert(s.CanWrite == false);
            }
         }

      }
   }
}
