using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public enum PoolType
    {

        Enemy  = 0,
        BulletEnemy,
        BulletPlayer,

        Count,
    };

    //!------------------------------------------------------------------------
    static List<KeyValuePair<int, GameObject>>        m_listObjectPool;
    static Transform m_transform;
    
    //!------------------------------------------------------------------------
    void Awake()
    {
        if(FindObjectsOfType(this.GetType()).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }
        else
        {
            
            m_transform = transform;
            StartCoroutine(IEWaitAwakeResource());

            DontDestroyOnLoad(this.gameObject);
            
        }
    }

    IEnumerator IEWaitAwakeResource()
    {
        // while(ResourceGame.m_instance == null)
        //     yield return new WaitForSeconds(0.1f);

        yield return new WaitWhile(() => ResourceGame.m_instance == null);

        Init();
        
    }

    public static void Init()
    {
        m_listObjectPool = new List<KeyValuePair<int, GameObject>>();

        GameObject gameObject;
        for(int i = 0; i < (int)PoolType.Count; i++)
        {
            //! Create Gameobject with component ObjectPool
            gameObject              = new GameObject();
            gameObject.name         = ResourceGame.GetNamePrefabByIndex(i);
            gameObject.AddComponent<ObjectPool>();
            gameObject.GetComponent<ObjectPool>().setPrefab(ResourceGame.GetPrefabByIndex(i));

            //!  Add this gameobject to gameobject with name "PoolManager"
            m_listObjectPool.Add(new KeyValuePair<int, GameObject>(i, gameObject));
            gameObject.transform.SetParent(m_transform);
        }
        gameObject = null;
        
        Debug.Log("Init PoolManager success!");
    }


    public static void SpawnEnemy(Transform _transform)
    {
        GameObject _enemy       = m_listObjectPool[(int)PoolType.Enemy].Value.GetComponent<ObjectPool>().GetPooledObject();
        
        if(_enemy != null)
        {
            Transform transform     = _enemy.GetComponent<Transform>();
        
            transform.position      = _transform.position;
            _enemy.GetComponent<PathFinding>().ActivePathFiding();
        }

    }

    public static void SpawnBulletPlayer(Transform _transformPlayer)
    {

        // GameObject _fireBullet = m_listObjectPool[(int)PoolType.BulletPlayer].Value.GetComponent<ObjectPool>().GetPooledObject();
        // Rigidbody2D rb = _fireBullet.GetComponent<Rigidbody2D>();

        // if(rb != null) rb.velocity = _transformPlayer.up * 10f;



        GameObject _fireBullet  = m_listObjectPool[(int)PoolType.BulletPlayer].Value.GetComponent<ObjectPool>().GetPooledObject();
        
        Transform transform     = _fireBullet.GetComponent<Transform>();
        transform.position      = _transformPlayer.position;
        
        _fireBullet.GetComponent<DamageDealer>().activeBullet();

        Rigidbody2D rb = _fireBullet.GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = _transformPlayer.up * 10f;

    }

    public static void SpawnBulletEnemy(Transform _transformEnemy)
    {
        GameObject _fireBullet  = m_listObjectPool[(int)PoolType.BulletEnemy].Value.GetComponent<ObjectPool>().GetPooledObject();
        
        Transform transform     = _fireBullet.GetComponent<Transform>();
        transform.position      = _transformEnemy.position;
        
        _fireBullet.GetComponent<DamageDealer>().activeBullet();

        Rigidbody2D rb = _fireBullet.GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = _transformEnemy.up * 10f;


    }






}
