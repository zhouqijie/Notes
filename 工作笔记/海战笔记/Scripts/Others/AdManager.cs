﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public static AdManager inst;

    [HideInInspector] public static string strVideoAdId = "i1hgdk827f371bgf2a";

    [HideInInspector] public static string strInterAdId = "2dxkt54ak3u173da8a";

    private static float timer60 = 60f;
    private static float timer30 = 30f;
    private static float timer2 = 2f;

    private void Awake()
    {
        inst = this;
        DontDestroyOnLoad(inst);
    }


    void Start()
    {
        //广告模块启动
        StarkSDKSpace.StarkSDK.API.GetStarkAdManager();
        timer30 = 16f;
        timer60 = 16f;
    }
    


    void Update()
    {
        timer60 -= Time.deltaTime;
        timer30 -= Time.deltaTime;
        timer2 -= Time.deltaTime;
    }


    public static void VideoAd(UnityAction successCallback, UnityAction failCallback)
    {
        //successCallback();
        //failCallback();

        FindObjectOfType<MiliPay>().u3dToJava_msg("99", new Callback(successCallback), new Callback(failCallback));
    }

    public static void InterstitialOrBannerAd(UnityEngine.UI.Button.ButtonClickedEvent[] eventsToAddTo)
    {

        //插屏或者banner
        if (timer2 < 0f)
        {

            if (timer60 < 0f)
            {
                
                try
                {
                    StarkSDKSpace.StarkAdManager.IsShowLoadAdToast = false;
                    StarkSDKSpace.StarkAdManager.InterstitialAd ad = StarkSDKSpace.StarkSDK.API.GetStarkAdManager().CreateInterstitialAd(AdManager.strInterAdId);

                    inst.StartCoroutine(inst.CoShowIntAd(ad, eventsToAddTo)); 
                }
                catch
                {

                }
                finally
                {
                    timer60 = 60f;
                }
            }
            else
            {
                //try
                //{
                //    StarkSDKSpace.StarkAdManager.IsShowLoadAdToast = false;
                //    StarkSDKSpace.StarkAdManager.BannerStyle style = new StarkSDKSpace.StarkAdManager.BannerStyle();
                //    style.left = 0; style.top = 0; style.width = Screen.width;
                //    StarkSDKSpace.StarkAdManager.BannerAd ad = StarkSDKSpace.StarkSDK.API.GetStarkAdManager().CreateBannerAd(Game.strBannderId, style);

                //    inst.StartCoroutine(inst.CoShowBannerAd(ad, eventsToAddTo));
                //}
                //catch
                //{

                //}
            }

            timer2 = 2f;
        }

    }


    private IEnumerator CoShowIntAd(StarkSDKSpace.StarkAdManager.InterstitialAd ad, UnityEngine.UI.Button.ButtonClickedEvent[] eventsToAddTo)
    {
        yield return new WaitForSeconds(0.5f); 

        yield return new WaitUntil(() => { return ad.IsLoaded(); });

        ad.Show();

        if (eventsToAddTo != null)
        {
            foreach (var evt in eventsToAddTo)
            {
                evt.AddListener(() =>
                {
                    ad.Destory();
                });
            }
        }
    }

    //private IEnumerator CoShowBannerAd(StarkSDKSpace.StarkAdManager.BannerAd ad, UnityEngine.UI.Button.ButtonClickedEvent[] eventsToAddTo)
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    ad.Show();

    //    foreach (var evt in eventsToAddTo)
    //    {
    //        evt.AddListener(() => {
    //            ad.Destory();
    //        });
    //    }
    //}
}
