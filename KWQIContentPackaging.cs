/*
0.1.0 implementation of how the KAR Workshop Quick Install format should be packaged and unpackaged after downloading.
On all platforms.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

public class KWQIPackaging
{
	//packs a ROM on windows for KWQI
	//Brotil is the go to program for doing so
	//static public bool PackROM_Windows(string brotilProg, string ROMFilepath)
	//{
	//    return true;
	//}

	//unpacks a ROM on windows for KWQI
	//Brotil is the go to program for doing so
	static public bool UnpackROM_Windows(string brotilProgFilepath, string workingDir, string ROMName,
	bool shouldDeletePackedROMFileAfterUnpacking)
	{
		//unpacks HP
		var hp = new System.Diagnostics.Process();
		hp.StartInfo.FileName = brotilProgFilepath;
		hp.StartInfo.Arguments = "--decompress -o " + ROMName + ".iso " + ROMName + ".br";
		hp.StartInfo.WorkingDirectory = workingDir;
		hp.Start();
		hp.WaitForExit();

		//deletes the brotil format
		if(shouldDeletePackedROMFileAfterUnpacking)
			System.IO.File.Delete(workingDir + "/" + ROMName + ".br");

		return true;
	}

	//unpacks a directory archive on Windows for KWQI
	//the first layer is a brotile
	//the second is a Tar
	static public bool UnpackArchive_Windows(string archivePackageDir, string archivePackageName, string outputDir,
	bool shouldDeletePackedROMFileAfterUnpacking, string brotilProgFilepath)
	{
		//decompress the brotile
		var hp = new System.Diagnostics.Process();
		hp.StartInfo.FileName = brotilProgFilepath;
		hp.StartInfo.Arguments = "--decompress -o " + archivePackageName + ".tar " + archivePackageName + ".br";
		hp.StartInfo.WorkingDirectory = archivePackageDir;
		hp.Start();
		hp.WaitForExit();

		//extract the Tar ball
		hp = new System.Diagnostics.Process();
		hp.StartInfo.FileName = "tar";
		hp.StartInfo.Arguments = "-xvf " + archivePackageName + ".tar";
		hp.StartInfo.WorkingDirectory = archivePackageDir;
		hp.Start();
		hp.WaitForExit();

		//clean up the brotile and the tar ball
		if(shouldDeletePackedROMFileAfterUnpacking)
		{
			if(System.IO.File.Exists(archivePackageDir + "/" + archivePackageName + ".br"))
				System.IO.File.Delete(archivePackageDir + "/" + archivePackageName + ".br");
			if(System.IO.File.Exists(archivePackageDir + "/" + archivePackageName + ".tar"))
				System.IO.File.Delete(archivePackageDir + "/" + archivePackageName + ".tar");
		}

		return true;
	}

	//downloads KWQI content Archive on Windows, regardless of the actual content
	static public bool DownloadContent_Archive_Windows(out System.Diagnostics.Process p, string dumaProgFilepath,
	 string displayName, string URL, string outputDir)
	{
		p = new System.Diagnostics.Process();
		p.StartInfo.FileName = dumaProgFilepath;
		p.StartInfo.Arguments = URL + 
		" -O " + outputDir + "/" + displayName + ".br";
		p.StartInfo.WorkingDirectory = outputDir;
		p.Start();

		return true;
	}
}
