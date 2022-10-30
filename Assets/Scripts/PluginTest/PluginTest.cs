using System;
using System.IO;
using TMPro;
using UnityEngine;

public class PluginTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TMP_InputField LogInput;
    private Logger logger;

    private void Awake()
    {
        logger = Logger.CreateLogger(text);
    }

    private void Start()
    {
        logger.ShowAllLogs();
    }

    public void SendLog()
    {
        logger.AddLog(LogInput.text);
        logger.WriteLog();
        LogInput.text = "";
        logger.ShowAllLogs();
    }

    public void ClearLog()
    {
        logger.Clear();
    }
}

public abstract class Logger
{
    private static string path = Application.persistentDataPath + "/Logs.txt";
    protected TextMeshProUGUI text;

    public abstract void AddLog(string log);

    public abstract void ShowAllLogs();

    public abstract void WriteLog();

    public abstract void Clear();

    public static Logger CreateLogger(TextMeshProUGUI text)
    {
        return new DefaultLogger(path, text);
//#if UNITY_ANDROID
//        return new AndroidLogger(path, text);
//#else
//        return new DefaultLogger(path, text);
//#endif
    }
}

public class AndroidLogger : Logger
{
    const string pluginName = "com.example.PluginTest";
    string path;

    AndroidJavaClass androidLoggerClass;
    AndroidJavaObject androidLoggerObject;

    public AndroidLogger(string path, TextMeshProUGUI text)
    {
        this.text = text;
        this.path = path;

        androidLoggerClass = new AndroidJavaClass(pluginName);
        androidLoggerObject = androidLoggerClass.CallStatic<AndroidJavaObject>("GetInstance", path);

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        androidLoggerObject.CallStatic("SetUnityActivity", jo);
    }

    private void ShowAlert(string title, string message, Action confirm = null, Action cancel = null)
    {
        Alert alert = new Alert();
        alert.positiveAction = confirm;
        alert.negativeAction = cancel;
        androidLoggerObject.Call("CreateAlert", new object[] { title, message, alert });
        androidLoggerObject.Call("ShowAlert");
    }

    public override void Clear()
    {
        ShowAlert("Deleting all records.", "Do you want to continue?", () => { androidLoggerObject.Call("ClearLogs"); text.text = ""; });
    }

    public override void AddLog(string log)
    {
        androidLoggerObject.Call("AddLog", log + "\n");
    }

    public override void ShowAllLogs()
    {
        text.text = androidLoggerObject.Call<string>("GetLogs");
    }

    public override void WriteLog()
    {
        androidLoggerObject.Call("WriteLog");
    }
}


public class DefaultLogger : Logger
{
    private string path;

    private string logs;

    public DefaultLogger(string path, TextMeshProUGUI text)
    {
        this.text = text;
        this.path = path;

        if (File.Exists(path))
            logs = File.ReadAllText(path);
    }

    public override void Clear()
    {
        logs = "";

        text.text = "";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public override void ShowAllLogs()
    {
        text.text = logs;
    }

    public override void AddLog(string log)
    {
        logs += log + "\n";
    }

    public override void WriteLog()
    {
        File.WriteAllText(path, logs);
    }
}

public class Alert : AndroidJavaProxy
{
    public Action positiveAction;
    public Action negativeAction;
    const string alertInterfaceName = "com.example.Logger.Alert";

    public Alert() : base(alertInterfaceName) { }

    public void OnPositive()
    {
        positiveAction?.Invoke();
    }

    public void OnNegative()
    {
        negativeAction?.Invoke();
    }
}