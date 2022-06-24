using UnityEditor;

public class BuildAllPlatforms 
{
    [MenuItem("ProcWave/Build Win+Mac+Lnx")]
    public static void BuildGame ()
    {
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = {"Assets/Scenes/MainScene.unity"};

        BuildPipeline.BuildPlayer(levels, path + "/Win/Demo.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        BuildPipeline.BuildPlayer(levels, path + "/Mac/Demo.app", BuildTarget.StandaloneOSX, BuildOptions.None);
        BuildPipeline.BuildPlayer(levels, path + "/Linux/Demo.x86_64", BuildTarget.StandaloneLinux64, BuildOptions.None);
    }
}
