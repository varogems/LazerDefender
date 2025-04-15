using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int    m_damage = 20;
    AudioManager            m_audioManager;
    Coroutine               m_crtHideBullet;

    private void Awake() 
    {
        m_audioManager  = FindObjectOfType<AudioManager>();
        m_crtHideBullet = null;
    }

    public int getDamage()
    {
        return m_damage;
    }

    public void activeBullet()
    {
        this.gameObject.SetActive(true);

        
        if(m_crtHideBullet != null)
        {
            StopCoroutine(m_crtHideBullet);
            m_crtHideBullet = null;
        }

        if(m_crtHideBullet == null)
            m_crtHideBullet = StartCoroutine(IEHideBullet());
    }

    public void killYourSelf()
    {
        if(m_audioManager != null)
            m_audioManager.playEffectAudio(AudioManager.EffectAudio.Explosion);

        if(m_crtHideBullet != null)
        {
            StopCoroutine(m_crtHideBullet);
            m_crtHideBullet = null;
        }
            
        // Destroy(this.gameObject);
        this.gameObject.SetActive(false);   
    }

    IEnumerator IEHideBullet()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

}
