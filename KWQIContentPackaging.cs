/*
0.2.0 implementation of how the KAR Workshop Quick Install format should be packaged and unpackaged after downloading.
On all platforms.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

//defines a main class for handling the package
public class KWQIPackaging
{
	//adds double quotes around string literals if needed
	static public string AddQuotesIfRequired(string path)
	{
    	return !string.IsNullOrWhiteSpace(path) ? 
        	path.Contains(" ") && (!path.StartsWith("\"") && !path.EndsWith("\"")) ? 
            	"\"" + path + "\"" : path : 
            	string.Empty;
	}

	//copies files/folders from one directory into another
	public static void CopyAllDirContents(DirectoryInfo source, DirectoryInfo target)
    {
		if(!Directory.Exists(target.FullName))
        	Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory.
        foreach (FileInfo fi in source.GetFiles())
        {
            System.Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyAllDirContents(diSourceSubDir, nextTargetSubDir);
        }
    }

	//copies all the files/folders from one directory into another
	public static void CopyAllDirContents(string sourceDirectory, string targetDirectory)
    {
        DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
        DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

        CopyAllDirContents(diSource, diTarget);
    }

	//unpacks a directory archive on Windows for KWQI
	//the first layer is a brotile
	//the second is a Tar
	static public bool UnpackArchive_Windows(string archivePackageDir, string archivePackageName, string outputDir,
	bool shouldDeletePackedROMFileAfterUnpacking, string brotilProgFilepath)
	{
		string brotilPackageFP = $"{archivePackageDir}/{archivePackageName}.br";
		string tarPackageFP = $"{archivePackageDir}/{archivePackageName}.tar";

		//decompress the brotile
		var hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
		{
            FileName = brotilProgFilepath,
            Arguments = $"--decompress \"{brotilPackageFP}\" -o \"{tarPackageFP}\"",
			WorkingDirectory = archivePackageDir,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = true,
            CreateNoWindow = false
        };
		hp.Start();
		hp.WaitForExit();

		//extract the Tar ball
		hp = new System.Diagnostics.Process();
		hp.StartInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "tar",
            Arguments = $"-xvf \"{tarPackageFP}\" -C \"{outputDir}\"",
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = true,
            CreateNoWindow = false
        };
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
		string brotilPackageFP = $"{outputDir}/{displayName}.br\"";

		string dumaProgFilepath = $"{_dumaProgFilepath}";

		p = new System.Diagnostics.Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.FileName = dumaProgFilepath;
		p.StartInfo.Arguments = $"{URL} -O {brotilPackageFP}";
		p.StartInfo.WorkingDirectory = outputDir;
		p.Start();

		return true;
	}

	//downloads gekko codes on Windows
	static public bool DownloadContent_GekkoCodes_Windows(out System.Diagnostics.Process p, string _dumaProgFilepath,
	 string displayName, string URL, string outputDir)
	{
		string codeFP = $"{outputDir}/{displayName}.ini";

		p = new System.Diagnostics.Process();
		p.StartInfo.UseShellExecute = true;
		p.StartInfo.FileName = _dumaProgFilepath;
		p.StartInfo.Arguments = URL + " -O " + codeFP;
		p.StartInfo.WorkingDirectory = outputDir
		p.Start();

		return true;
	}
}
