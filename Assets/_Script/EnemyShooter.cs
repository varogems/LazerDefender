using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    [SerializeField] GameObject m_projectilePrefab;
    [SerializeField] float      m_speedProjectile = 10f;
    [SerializeField] float      m_lifeTimeProjectile = 5f;
    [SerializeField] float      m_timeBetwProjecttile = .2f;

    Coroutine                   m_CRTEnemyShoort = null;
    AudioManager                m_audioManager;

    GameObject                  m_gameObject;

    private void Awake() 
    {
        m_audioManager = FindObjectOfType<AudioManager>();
        m_gameObject = this.gameObject;
    }



    public void Fire()
    {
        if(m_CRTEnemyShoort != null)
        {
            StopCoroutine(m_CRTEnemyShoort);
            m_CRTEnemyShoort = null;
        }

        if(m_CRTEnemyShoort == null) 
            m_CRTEnemyShoort = StartCoroutine(CRTFireContiniously());
    }


    

    IEnumerator CRTFireContiniously()
    {
        // while(true)
        while(m_gameObject.activeSelf)
        {
            // GameObject instance = Instantiate(m_projectilePrefab, 
            //                     transform.position,
            //                     transform.rotation);

            // m_audioManager.playEffectAudio(AudioManager.EffectAudio.ShootingEnemy1);

            // Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            // if(rb != null) 
            //     rb.velocity = transform.up * m_speedProjectile;

            // Destroy(instance.gameObject, m_lifeTimeProjectile);

            // yield return new WaitForSeconds(m_timeBetwProjecttile);

            


            PoolManager.SpawnBulletEnemy(transform);
            m_audioManager.playEffectAudio(AudioManager.EffectAudio.ShootingEnemy1);
            yield return new WaitForSeconds(m_timeBetwProjecttile);
        }
    }
}
