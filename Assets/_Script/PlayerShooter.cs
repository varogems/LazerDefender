using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooter : MonoBehaviour
{

    [SerializeField] GameObject m_projectilePrefab;
    [SerializeField] float      m_speedProjectile = 10f;
    [SerializeField] float      m_lifeTimeProjectile = 5f;
    [SerializeField] float      m_timeBetwProjecttile = .2f;
    [SerializeField] bool       m_isFiring;

    Coroutine                   m_coroutineShooter = null;
    AudioManager                m_audioManager;
    

    private void Awake() 
    {
        
        m_audioManager      = FindObjectOfType<AudioManager>();
    }

    public void setFire(bool isFiring)
    {
        m_isFiring = isFiring;
    }


    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(m_isFiring && m_coroutineShooter == null)
        {
            m_coroutineShooter = StartCoroutine(CRTFireContiniously());
            
        }
        else if(!m_isFiring && m_coroutineShooter != null)
        {
                StopCoroutine(m_coroutineShooter);
                m_coroutineShooter = null;

        }
        
    }


    IEnumerator CRTFireContiniously()
    {
        while(true)
        {
            // GameObject instance = Instantiate(m_projectilePrefab, 
            //                     transform.position, 
            //                     //Quaternion.identity
            //                     transform.rotation);

            // m_audioManager.playEffectAudio(AudioManager.EffectAudio.ShootingPlayer);


            // Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            // if(rb != null) 
            //     rb.velocity = transform.up * m_speedProjectile;

            // Destroy(instance.gameObject, m_lifeTimeProjectile);

            // yield return new WaitForSeconds(m_timeBetwProjecttile);



            PoolManager.SpawnBulletPlayer(transform);
            m_audioManager.playEffectAudio(AudioManager.EffectAudio.ShootingPlayer);
            yield return new WaitForSeconds(m_timeBetwProjecttile);



        }
    }


}
