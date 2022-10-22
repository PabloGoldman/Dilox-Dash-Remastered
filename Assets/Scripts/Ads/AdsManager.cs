using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation.Samples;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    #region Singleton
    public static AdsManager instance;

    public InterstitialExample interstitialAd;

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

        interstitialAd = GetComponent<InterstitialExample>();
    }
    #endregion

    [SerializeField] float timePerAd;
    float adCounter;

    public float delayAfterReachingEndLevel;

    void Start()
    {
        adCounter = timePerAd;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inGameplay)
        {
            if (adCounter >= 0)
            {
                adCounter -= Time.deltaTime;
            }
        }
    }

    public void ShowDeathAd()
    {
        if (adCounter < 0)
        {
            adCounter = timePerAd;
            ShowInterstitial();
        }
    }

    public void ShowAdWithDelay()
    {
        Invoke(nameof(ShowInterstitial), delayAfterReachingEndLevel);
    }

    void ShowInterstitial()
    {
        interstitialAd.ShowInterstitial();
    }
}
