using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public enum eScene
    {
        Menu = 0,
        Game,
        GameOver
    } 

    [SerializeField] float m_delayBetwScene;
    EnemySpawner        m_spawnEnemy;

    void Awake()
    {
        StartCoroutine(IEWaitEnemySpawn());
    }

    IEnumerator IEWaitEnemySpawn()
    {
        // Debug.Log("LevelManager::IEWaitEnemySpawn");
        yield return new WaitUntil(() => (EnemySpawner.m_instance) != null);
        m_spawnEnemy  =  EnemySpawner.m_instance;


        
        if(SceneManager.GetActiveScene().buildIndex == (int)(eScene.Game))
        {
            m_spawnEnemy.StartSpawnEnemy();
            // Debug.Log("LevelManager::m_spawnEnemy.StartSpawnEnemy()");
        }
        else 
        {
            m_spawnEnemy.StopSpawnEnemy();
            // Debug.Log("LevelManager::m_spawnEnemy.StopSpawnEnemy()");
        }

    }

    public void loadMenuScene()
    {
        loadScene(eScene.Menu, m_delayBetwScene);
    }

    public void loadGameScene()
    {
        loadScene(eScene.Game, m_delayBetwScene);
    }


    public void loadGameOverScene()
    {
        loadScene(eScene.GameOver, m_delayBetwScene);
    }

    public void loadScene(eScene scene, float delay)
    {
        StartCoroutine(IELoadScene(scene,delay));
    }

    IEnumerator IELoadScene(eScene scene, float delay)
    {
        SceneManager.LoadScene((int)scene);
        yield return new WaitForSeconds(delay);

    }



    public void quitGame()
    {
        Application.Quit();
    }
    
}
