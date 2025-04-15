using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGame : MonoBehaviour
{
    public enum ETypeResource
    {
        Prefab = 0,
        AudioEffect,
        BackgroundMusic
    }

    static List<KeyValuePair<int, List<KeyValuePair<string, UnityEngine.Object>>>> m_listResrc;
    public static ResourceGame m_instance {get; private set;}

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
            Init();
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

    static void Init()
    {
        
        LoadResource();
        
        Debug.Log("Init ResourceGame success!");
        
        
    }

    static void LoadResource()
    {
        //! Init m_listResrc
        m_listResrc = new List<KeyValuePair<int, List<KeyValuePair<string, UnityEngine.Object>>>>();

        //! Prefab for pool manager.
        List<KeyValuePair<string, UnityEngine.Object>> listPoolPrefab = new List<KeyValuePair<string, UnityEngine.Object>>();
        listPoolPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("Enemy",          Resources.Load("Prefab/Enemy 1")));
        listPoolPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("BulletEnemy",    Resources.Load("Prefab/Projectile Enemy 1")));
        listPoolPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("BulletPlayer",   Resources.Load("Prefab/Projectile Player")));

        m_listResrc.Add(new KeyValuePair<int, List<KeyValuePair<string, UnityEngine.Object>>>((int)ETypeResource.Prefab, listPoolPrefab));


        
        //! Prefab for audio effect.
        List<KeyValuePair<string, UnityEngine.Object>> listAudioPrefab = new List<KeyValuePair<string, UnityEngine.Object>>();
        // listAudioPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("Brick_Block", Resources.Load("Sounds/Effects/Brick_Block")));

        m_listResrc.Add(new KeyValuePair<int, List<KeyValuePair<string, UnityEngine.Object>>>((int)ETypeResource.AudioEffect, listAudioPrefab));




        
        //! Prefab for background music.
        List<KeyValuePair<string, UnityEngine.Object>> listBgMusicPrefab = new List<KeyValuePair<string, UnityEngine.Object>>();
        listBgMusicPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("SMAS_-_Game_Select",    Resources.Load("Sounds/Musics/SMAS_-_Game_Select")));
        listBgMusicPrefab.Add(new KeyValuePair<string, UnityEngine.Object>("SMBDX_Overworld_Theme", Resources.Load("Sounds/Musics/SMBDX_Overworld_Theme")));
        m_listResrc.Add(new KeyValuePair<int, List<KeyValuePair<string, UnityEngine.Object>>>((int)ETypeResource.BackgroundMusic, listBgMusicPrefab));

    }

    public static List<KeyValuePair<string, UnityEngine.Object>> GetListPrefabByType(ETypeResource type)
    {
        return m_listResrc[(int)type].Value;
    }

    public static UnityEngine.Object GetPrefab(string namePrefab)
    {
        foreach(KeyValuePair<string, UnityEngine.Object> _obj  in m_listResrc[(int)ETypeResource.Prefab].Value)
            if(_obj.Key.ToString() == namePrefab)
                return _obj.Value;
        return null;
    }

    public static int numberOfPool()
    {
        return m_listResrc.Count;
    }

    public static UnityEngine.Object GetPrefabByIndex(int index)
    {
        return m_listResrc[(int)ETypeResource.Prefab].Value[index].Value;
    }

    public static string GetNamePrefabByIndex(int index)
    {
        return m_listResrc[(int)ETypeResource.Prefab].Value[index].Key;
    }
}
