using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    
    [SerializeField] Button m_buttonADS;
    string _adUnitId;
    [SerializeField] float m_delayTimeNextADS;
    [SerializeField] int    m_awardScore;
    ScoreKeeper m_scoreKeeper;

 
    void Awake()
    {
        m_awardScore        = 400;
        m_delayTimeNextADS  = 5f;
        m_scoreKeeper       = ScoreKeeper.m_instance;
        m_buttonADS.interactable = false;
        
        StartCoroutine(IEDelayNextToADS());

        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }
    
 
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
        Time.timeScale = 0f;
        // m_buttonADS.interactable = false;
    }
 
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }
 
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
 
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string _adUnitId) 
    { 
        Debug.Log("OnUnityAdsShowStart");

    }
    public void OnUnityAdsShowClick(string _adUnitId) 
    { 
        Debug.Log("OnUnityAdsShowClick");
    }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Time.timeScale = 1f;
        m_scoreKeeper.collectScore(m_awardScore);
        StartCoroutine(IEDelayNextToADS());
        Debug.Log("OnUnityAdsShowComplete");
    }

    IEnumerator IEDelayNextToADS()
    {
        m_buttonADS.interactable = false;
        yield return new WaitForSeconds(m_delayTimeNextADS);
        m_buttonADS.interactable = true;
    }
}