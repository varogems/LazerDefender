using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class AudioManager : MonoBehaviour
{


    [SerializeField] List<AudioClip>        m_listClips;
    [SerializeField][Range(0, 1)] float     volume;




    public enum EffectAudio 
    {
        ShootingPlayer = 0,
        ShootingEnemy1,
        Explosion,
    }
        
    private void Awake() 
    {
        if(FindObjectsOfType(this.GetType()).Count() > 1)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else DontDestroyOnLoad(this.gameObject);
    }

    public void playEffectAudio(EffectAudio effectAudio)
    {
        if(m_listClips[(int)effectAudio] != null)
            AudioSource.PlayClipAtPoint(m_listClips[(int)effectAudio],
                                        Camera.main.transform.position, 
                                        volume);

    }


    public void playEffectAudio(EffectAudio effectAudio, Transform transform)
    {
        if(m_listClips[(int)effectAudio] != null)
            AudioSource.PlayClipAtPoint(m_listClips[(int)effectAudio],
                                        transform.position, 
                                        volume);

    }





}
