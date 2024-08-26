/*
0.2.0 implementation of how the KAR Workshop folder structure works and how content should be installed

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

//defines a object for defining a how files and should be installed in the KW Structure
public struct KWInstallFile
{
    string filename = ""; //name of the file
    string installDirFP = ""; //the relative filepath for where it should be placed in the KW structure tree

    public KWInstallFile() {}

    //writes it to file
}

//defines a global for handling the KWStructure
public class KWStructure
{
    //------HIGH LEVEL DIRECTORIES------//

    //gets a string name for netplay client directory
    public static string GetStringDirectoryName_NetplayClients() {return "Clients";}

    //gets a string name for accounts directory
    public static string GetStringDirectoryName_Accounts() {return "Accounts";}

    //gets a string name for mods directory
    public static string GetStringDirectoryName_Mods() {return "Mods";}

    //gets a string name for tools directory
    public static string GetStringDirectoryName_Tools() {return "Tools";}

    //gets a string name for ROMs directory
    public static string GetStringDirectoryName_ROMs() {return "ROMs";}

    //gets a string name for KWQI file directory
    public static string GetStringDirectoryName_KWQI() {return "KWQI";}

    //gets a string name for Replays directory
    public static string GetStringDirectoryName_Replays() {return "Replays";}

    //------SUB DIRECTORIES-------//

     //gets a string name for mods sub-directory for Skin Packs
    public static string GetStringDirectoryName_Mods_SkinPacks() {return "SkinPacks";}

    //gets a string name for mods sub-directory for Homebrew
    public static string GetStringDirectoryName_Mods_Homebrew() {return "Homebrew";}

    //generates a netplay client directory
    public static string GenerateKWStructure_Directory_NetplayClients(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_NetplayClients();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates a accounts directory
    public static string GenerateKWStructure_Directory_Accounts(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_Accounts();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates a mods directory
    public static string GenerateKWStructure_Directory_Mods(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_Mods();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates a tools directory
    public static string GenerateKWStructure_Directory_Tools(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_Tools();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates a ROMs directory
    public static string GenerateKWStructure_Directory_ROMs(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_ROMs();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates KWQI file directory
    public static string GenerateKWStructure_Directory_KWQI(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_KWQI();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates Replays directory
    public static string GenerateKWStructure_Directory_Replays(string rootDir)
    {
        string dir = rootDir + "/" + GetStringDirectoryName_Replays();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates mod sub directory for Skin Packs
    public static string GenerateKWStructure_SubDirectory_Mod_SkinPacks(string rootDir)
    {
        string dir = GenerateKWStructure_Directory_Mods(rootDir) + "/" + GetStringDirectoryName_Mods_SkinPacks();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }

    //generates mod sub directory for Homebrew
    public static string GenerateKWStructure_SubDirectory_Mod_Hombrew(string rootDir)
    {
        string dir = GenerateKWStructure_Directory_Mods(rootDir) + "/" + GetStringDirectoryName_Mods_Homebrew();

        if (!Directory.Exists(dir))
			Directory.CreateDirectory(dir);

        return dir;
    }
}