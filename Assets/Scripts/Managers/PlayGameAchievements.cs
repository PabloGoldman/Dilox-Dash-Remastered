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

    private bool Authenticated;


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

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    #endregion

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Authenticated = true;
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            Authenticated = false;
        }
    }

    public void WinLevelAchievement(int levelId)
    {
#if UNITY_ANDROID
        if (Authenticated)
        {
            switch (levelId)
            {
                case 0:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_welcome_to_dilox, 100f, success => { });
                    break;
                case 1:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_stone_age, 100f, success => { });
                    break;
                case 2:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_time_to_get_serious, 100f, success => { });
                    break;
                case 3:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_hot_time, 100f, success => { });
                    break;
                default:
                    break;
            }
        }
#endif
    }
}
