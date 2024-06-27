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
	//7Zip is the go to and assumes the archive is a SFX (auto unpacking EXE)
	static public bool UnpackArchive_Windows(string archivePackageDir, string archivePackageName, string outputDir,
	bool shouldDeletePackedROMFileAfterUnpacking)
	{
		//unpacks KARphin
		var p = new System.Diagnostics.Process();
		p.StartInfo.FileName = archivePackageDir + "/" + archivePackageName + ".exe";
		p.StartInfo.Arguments = "-o " + outputDir + " -y";
		p.StartInfo.WorkingDirectory = outputDir;
		p.Start();
		p.WaitForExit();

		//deletes the 7zip unpacking exe
		if(shouldDeletePackedROMFileAfterUnpacking)
			System.IO.File.Delete(archivePackageDir + "/" + archivePackageName + ".exe");

		return true;
	}

	//downloads KWQI content Archive on Windows
	//Duma is the go to program for handling the downloads
	//a 7Zip exe (auto extracting) Archive is assumed.
	static public bool DownloadContent_Archive_Windows(out System.Diagnostics.Process p, string dumaProgFilepath,
	 string displayName, string URL, string outputDir)
	{
		p = new System.Diagnostics.Process();
		p.StartInfo.FileName = dumaProgFilepath;
		p.StartInfo.Arguments = URL + 
		" -O " + outputDir + "/" + displayName + ".exe";
		p.StartInfo.WorkingDirectory = outputDir;
		p.Start();

		return true;
	}

	//downloads KWQI content ROM on Windows
	//Duma is the go to program for handling the downloads
	//a Brotil compressed file is assumed.
	static public bool DownloadContent_ROM_Windows(out System.Diagnostics.Process p, string dumaProgFilepath,
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
