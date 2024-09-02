/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

//defines a implementation for installing content to specific parts of the KARNetplay
class KWInstaller
{
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

    //installs all the content in the uncompressed content folder to the Netplay Clients folder
    public static bool NetplayClients_AllContent(DirectoryInfo contentFolder, DirectoryInfo rootDir)
    {
        CopyAllDirContents(contentFolder,
        KWStructure.GenerateKWStructure_Directory_NetplayClients(rootDir));

        return true;
    }
}