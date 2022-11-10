using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

public class PlayGameAchievements : MonoBehaviour
{
    #region Singleton

    public static PlayGameAchievements instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        try
        {
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            //PlayGamesPlatform.Activate();
            //Social.localUser.Authenticate(ProcessAuthentication);
#endif
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }
    #endregion

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }

    public void WinLevelAchievement(int levelId)
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
        {
            switch (levelId)
            {
                case 0:
                    Social.ReportProgress("CgkIio7pi7oMEAIQAQ", 100f, success => { });
                    break;
                case 1:
                    Social.ReportProgress("CgkIio7pi7oMEAIQAg", 100f, success => { });
                    break;
                case 2:
                    Social.ReportProgress("CgkIio7pi7oMEAIQAw", 100f, success => { });
                    break;
                case 3:
                    Social.ReportProgress("CgkIio7pi7oMEAIQBA", 100f, success => { });
                    break;
                default:
                    break;
            }
        }
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
