using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] bool               m_isLoop;
    [SerializeField] List<WaveConfigSO> m_listWave;
    [SerializeField] float              m_timeBetwWaves;
    WaveConfigSO                        m_curWave;
    Coroutine                           m_crtSpawnEnemy;
    public static EnemySpawner          m_instance = null;

    void Awake()
    {

        if(FindObjectsOfType(this.GetType()).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            
            Debug.Log("Destroy EnemySpawner this");
            return;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            
            m_crtSpawnEnemy = null;
            m_instance      = this;
            Debug.Log("EnemySpawner this");
            
        }
    }

    public WaveConfigSO getCurWave()
    {
        return m_curWave;
    }


    IEnumerator IESpawnEnemyWaves()
    {
        while(m_isLoop)
            foreach(WaveConfigSO currWave in m_listWave)
            {
                m_curWave = currWave;

                for(int i = 0; i < m_curWave.getEnemyCount(); i++)
                {
                    // Instantiate(m_curWave.getEnemy(0), 
                    //             m_curWave.getStartPoint().position, 
                    //             Quaternion.Euler(0, 0, 180), 
                    //             this.transform);

                    PoolManager.SpawnEnemy(m_curWave.getStartPoint());

                    yield return new WaitForSeconds(m_curWave.randomTimeEnemySpawn());
                }
                
                yield return new WaitForSeconds(m_timeBetwWaves);
            }
    }

    public void StartSpawnEnemy()
    {

        if(m_crtSpawnEnemy == null) 
            m_crtSpawnEnemy = StartCoroutine(IESpawnEnemyWaves());
    }

    public void StopSpawnEnemy()
    {
        
        if(m_crtSpawnEnemy != null) 
        {
            StopCoroutine(m_crtSpawnEnemy);
            m_crtSpawnEnemy = null;
        }
    }

}
