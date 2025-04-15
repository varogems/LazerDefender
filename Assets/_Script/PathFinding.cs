using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    EnemySpawner                    m_enemySpawner;
    WaveConfigSO                    m_waveConfig;

    List<Transform>                 m_wayPoints;

    int                             m_wayPointIndex;
    Coroutine                       m_crtActivePathFiding = null;
    bool                            m_isActive = false;
    EnemyShooter                    m_enemyShooter;

    void Awake() 
    {
        m_enemyShooter = gameObject.GetComponent<EnemyShooter>();
    }



    IEnumerator IEActivePathFiding()
    {
        
        // Debug.Log("ActivePathFiding:EnemySpawner.m_instance " + EnemySpawner.m_instance);
        
        //! Get EnemySpawn.
        if(EnemySpawner.m_instance == null)
            yield return new WaitWhile(() => EnemySpawner.m_instance == null);

        // Debug.Log("...ActivePathFiding:EnemySpawner.m_instance " + EnemySpawner.m_instance);

        m_enemySpawner  = FindObjectOfType<EnemySpawner>();
        m_waveConfig    = m_enemySpawner.getCurWave();

        
        m_wayPoints         = m_waveConfig.getListWayPoints();
        m_wayPointIndex     = 0;
        transform.position  = m_wayPoints[m_wayPointIndex].position;
        m_isActive          = true;

        m_enemyShooter.Fire();
        
        // Debug.Log("Done ActivePathFiding");
    }

    

    public void ActivePathFiding()
    {
        // Debug.Log("ActivePathFiding");
        if(m_crtActivePathFiding != null)
        {
            
            StopCoroutine(m_crtActivePathFiding);
            m_crtActivePathFiding = null;
            // Debug.Log("StopCoroutine ActivePathFiding");
        }

        if(m_crtActivePathFiding == null)
        {
            m_crtActivePathFiding = StartCoroutine(IEActivePathFiding());
            // Debug.Log("StartCoroutine ActivePathFiding");
        }
    }

    void Update()
    {
        if(!m_isActive) 
            return;

        FollowPath();
    }


    void FollowPath()
    {
        if(m_enemySpawner == null) return;

        if(m_wayPointIndex < m_wayPoints.Count)
        {
            Vector3 nextPosition = m_wayPoints[m_wayPointIndex].position;

            float deltaDistance = m_waveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,nextPosition, deltaDistance);
            
            if(transform.position == nextPosition)
                m_wayPointIndex++;
        }
        else
        {
            if(m_crtActivePathFiding != null)
            {
                StopCoroutine(m_crtActivePathFiding);
                m_crtActivePathFiding = null;
            }
            
            this.gameObject.SetActive(false);
        }
    }
}
