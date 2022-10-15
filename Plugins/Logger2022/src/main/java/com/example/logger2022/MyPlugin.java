package com.example.logger2022;
import android.util.Log;

public class MyPlugin {
    private static final MyPlugin instance = new MyPlugin();
    private static final String TAG = "Log22 => ";
    public static MyPlugin GetInstance()
    {
        Log.d("", "GetInstance()");
        return instance;
    }

    private MyPlugin() { MyLog("Logger created"); }

    public void MyLog(String str) { Log.d(TAG, str); }
}
