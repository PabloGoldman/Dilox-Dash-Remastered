using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginTest2 : MonoBehaviour
{
    const string PACK_NAME = "Logger2022-release";

    const string LOGGER_CLASS_NAME = "JLogger";

    const string FILEMANAGER_CLASS_NAME = "FileManager";

    static AndroidJavaClass JLoggerClass = null;

    static AndroidJavaObject JLoggerInstance = null;

    static AndroidJavaClass FileManagerClass = null;

    static AndroidJavaObject FileManagerInstance = null;

    static void init()
    {
        JLoggerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
        JLoggerInstance = JLoggerClass.CallStatic<AndroidJavaObject>("GetInstance");
        FileManagerClass = new AndroidJavaClass(PACK_NAME + "." + FILEMANAGER_CLASS_NAME);
        FileManagerInstance = FileManagerClass.CallStatic<AndroidJavaObject>("GetInstance");

        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        FileManagerClass.SetStatic<AndroidJavaObject>("myActivity", activity);
    }
    public static void SendLog(string msg)
    {
        if (JLoggerInstance==null)
        {
            init();
        }
        JLoggerInstance.Call("SendLog", msg);
        WriteFile(msg);
    }
    public static string ReadFile()
    {
        if (FileManagerInstance == null)
        {
            init();
        }
        return FileManagerInstance.CallStatic<string>("ReadFile", " ");
    }

    private static void WriteFile(string msg)
    {
        if (FileManagerInstance == null)
        {
            init();
        }
        FileManagerInstance.CallStatic("WriteFile", msg);
    }


    public static void ClearFile()
    {
        if (FileManagerInstance == null)
        {
            init();
        }
        FileManagerInstance.CallStatic("ClearFile", " ");
    }
}
