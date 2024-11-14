using System;
using Game;
using UnityEngine;
using Gley.MobileAds;

// ReSharper disable once HollowTypeName
public partial class AdsManager : Singleton<AdsManager>
{




    public bool Initialized { get; private set; }

    // ReSharper disable once NotAccessedField.Local
    private Action<bool> _pendingCallback;

    public static int InterstitialCount { get; private set; } = 0;

    //public static bool HaveSetupConsent => PrefManager.HasKey(nameof(ConsentActive));

    //public static bool ConsentActive
    //{
    //    get => PrefManager.GetBool(nameof(ConsentActive));
    //    set => PrefManager.SetBool(nameof(ConsentActive), value);
    //}




    void Start()
    {
        API.Initialize(OnInitialized);

        //if (HaveSetupConsent)
        //    Init();
    }


    public void Init()
    {
        if (Initialized)
            return;



        Initialized = true;
    }


    private void OnInitialized()
    {
        API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);

        if (!API.GDPRConsentWasSet())
        {
            API.ShowBuiltInConsentPopup(PopupCloseds);
        }
    }

    private void PopupCloseds()
    {

    }

}



public partial class AdsManager
{
    private static int AdsPassLeftCount
    {
        get
        {
            if (!PlayerPrefs.HasKey(nameof(AdsPassLeftCount)))
            {
                SetForNextAds();
            }

            return PlayerPrefs.GetInt(nameof(AdsPassLeftCount));
        }
        set => PlayerPrefs.SetInt(nameof(AdsPassLeftCount), value);
    }


    public static void ShowOrPassAdsIfCan()
    {
        if (!ResourceManager.EnableAds)
            return;

        if (AdsPassLeftCount <= 0)
        {
            ShowAdsIfPassedIfCan();
        }
        else
        {
            PassAdsIfCan();
        }
    }

    public static void ShowAdsIfPassedIfCan()
    {
        if (!ResourceManager.EnableAds)
            return;
        if (AdsPassLeftCount <= 0)
        {

           API.ShowInterstitial();
            //ShowInterstitial();
            SetForNextAds();

        }
    }

    private static void SetForNextAds()
    {
        AdsPassLeftCount =
            UnityEngine.Random.Range(GameSettings.Default.AdsSettings.minAndMaxGameOversBetweenInterstitialAds.x,
                GameSettings.Default.AdsSettings.minAndMaxGameOversBetweenInterstitialAds.y + 1);
    }

    public static void PassAdsIfCan()
    {
        if (!ResourceManager.EnableAds)
            return;

        AdsPassLeftCount = Mathf.Max(AdsPassLeftCount - 1, 0);
    }
}

