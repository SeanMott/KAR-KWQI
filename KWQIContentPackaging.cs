/*
0.1.0 implementation of how the KAR Workshop Quick Install format should be packaged and unpackaged after downloading.
On all platforms.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

public class KWQIPackaging
{
	//adds double quotes around string literals if needed
	/*static public string AddQuotesIfRequired(string path)
	{
    	return !string.IsNullOrWhiteSpace(path) ? 
        	path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ? 
            	"\"" + path + "\"" : path : 
            	string.Empty;
	}*/

	//copies the contents of a file into another directory
	static public void CopyDirectoryContents(string sourceDir, string destinationDir)
	{
        // Ensure the destination directory exists
        Directory.CreateDirectory(destinationDir);

        // Copy all the files from the source directory to the destination directory
        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destinationDir, fileName);
            File.Copy(file, destFile, true); // true to overwrite existing files
        }

        // Copy all the subdirectories from the source directory to the destination directory
        foreach (string subdir in Directory.GetDirectories(sourceDir))
        {
            string subdirName = Path.GetFileName(subdir);
            string destSubdir = Path.Combine(destinationDir, subdirName);
            CopyDirectoryContents(subdir, destSubdir); // Recursive call to copy subdirectories
        }
    }

	//unpacks a directory archive on Windows for KWQI
	//the first layer is a brotile
	//the second is a Tar
	static public bool UnpackArchive_Windows(string _archivePackageDir, string _archivePackageName, string _outputDir,
	bool shouldDeletePackedROMFileAfterUnpacking, string _brotilProgFilepath, string _sevenZipProgFilepath)
	{
		string brotilPackageFP = "\"" + _archivePackageDir + "/" + _archivePackageName + ".br\"";
		string tarPackageFP = "\"" + _archivePackageDir + "/" + _archivePackageName + ".tar\"";
		string extractedPackageFP = "\"" + _outputDir + "/" + _archivePackageName + "\"";
		string workingDir = "\"" + _archivePackageDir + "\"";

		string brotilProgFilepath = "\"" + _brotilProgFilepath + "\"";
		string sevenZipProgFilepath = "\"" + _sevenZipProgFilepath + "\"";

		//decompress the brotile
		var hp = new System.Diagnostics.Process();
		hp.StartInfo.UseShellExecute = true;
		hp.StartInfo.FileName = brotilProgFilepath;
		hp.StartInfo.Arguments = "--decompress -o " + tarPackageFP + " " + brotilPackageFP;
		hp.StartInfo.WorkingDirectory = workingDir;
		hp.Start();
		hp.WaitForExit();

		//extract the Tar ball
		hp = new System.Diagnostics.Process();
		hp.StartInfo.UseShellExecute = true;
		hp.StartInfo.FileName = sevenZipProgFilepath;
		hp.StartInfo.Arguments = "x " + tarPackageFP + " -o" + extractedPackageFP;
		hp.StartInfo.WorkingDirectory = workingDir;
		hp.Start();
		hp.WaitForExit();

		//clean up the brotile and the tar ball
		if(shouldDeletePackedROMFileAfterUnpacking)
		{
			if(System.IO.File.Exists(brotilPackageFP))
				System.IO.File.Delete(brotilPackageFP);
			if(System.IO.File.Exists(tarPackageFP))
				System.IO.File.Delete(tarPackageFP);
		}

		return true;
	}

	//downloads KWQI content Archive on Windows, regardless of the actual content
	static public bool DownloadContent_Archive_Windows(out System.Diagnostics.Process p, string _dumaProgFilepath,
	 string displayName, string URL, string outputDir)
	{
		//System.IO.Path outputDir = new System.IO.Path(_outputDir);

		string workingDir = "\"" + outputDir + "\"";
		string brotilPackageFP = "\"" + outputDir + "/" + displayName + ".br\"";

		string dumaProgFilepath = "\"" + _dumaProgFilepath + "\"";

		p = new System.Diagnostics.Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.FileName = dumaProgFilepath;
		p.StartInfo.Arguments = URL + " -O " + brotilPackageFP;
		p.StartInfo.WorkingDirectory = workingDir;
		p.Start();

		return true;
	}
}
