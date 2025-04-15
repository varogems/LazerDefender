using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New WaveConfigSO", menuName = "WaveConfigSO")]
public class WaveConfigSO : ScriptableObject 
{
    [SerializeField] Transform  m_listWayPoints;
    [SerializeField] float      m_moveSpeed;
    [SerializeField] List<GameObject> m_listPrefabEnemies;
    [SerializeField] float      m_timeBetwEnemySpawn;
    [SerializeField] float      m_minimumTimeEnemySpawn;
    [SerializeField] float      m_varianceTimeEnemySpawn;



    public Transform getStartPoint()
    {
        return m_listWayPoints.GetChild(0);
    }

    public List<Transform> getListWayPoints()
    {
        List<Transform> list = new List<Transform>();
        foreach(Transform child in m_listWayPoints) list.Add(child);

        return list;
    }

    public float getMoveSpeed()
    {
        return m_moveSpeed;
    }


    public int getEnemyCount()
    {
        return m_listPrefabEnemies.Count;
    }


    public GameObject getEnemy(int index)
    {
        return  m_listPrefabEnemies[index];
    }


    public float randomTimeEnemySpawn()
    {
        float spawnTime = Random.Range(m_timeBetwEnemySpawn - m_varianceTimeEnemySpawn,
                                        m_timeBetwEnemySpawn + m_varianceTimeEnemySpawn);
        return Mathf.Clamp(spawnTime, m_minimumTimeEnemySpawn, float.MaxValue);
    }

}
