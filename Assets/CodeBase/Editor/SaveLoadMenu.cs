using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class SaveLoadMenu
    {
        [MenuItem("GameTool/Save/Open Directory")]
        static void OpenDirectory() => 
            Process.Start(Application.persistentDataPath);

        [MenuItem("GameTool/Save/Clear Save")]
        static void ClearSave() => 
            File.Delete($"{Application.persistentDataPath}/Save.json");
    }
}