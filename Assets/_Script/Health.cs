using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int    m_heath = 100;
    [SerializeField] int    m_score = 0;
    ScoreKeeper             m_scoreKeeper;

    LevelManager            m_levelManager;

    [SerializeField] ParticleSystem          m_particleSystem;
    
    void Awake() 
    {
        m_levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine(IEWaitScoreKeeper());

        //m_particleSystem = FindObjectOfType<ParticleSystem>();
    }

    IEnumerator IEWaitScoreKeeper()
    {
        yield return new WaitWhile(() => ScoreKeeper.m_instance == null);
        m_scoreKeeper = ScoreKeeper.m_instance;

    }

    void takeDamage(int damage)
    {
        m_heath -= damage;

        if(m_heath < 1) 
        {
            //! If enemy is terminated, ++ score...
            if(this.gameObject.layer.CompareTo(LayerMask.NameToLayer("Player")) != 0)
            {
                this.gameObject.SetActive(false);
                if(m_scoreKeeper != null) m_scoreKeeper.collectScore(m_score);
                //Destroy(this.gameObject);
            }
            //! If player die, hide player.
            else 
            {
                this.gameObject.SetActive(false);
                Invoke("loadGameOverScene", 1.5f);
            }
        }
        
    }

    void playEffectDamage()
    {
        if(m_particleSystem != null)
        {
            ParticleSystem instance = Instantiate(m_particleSystem, this.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
        else Debug.Log("None playEffectDamage");
    }

    void loadGameOverScene()
    {
        m_levelManager.loadGameOverScene();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        DamageDealer dd = other.GetComponent<DamageDealer>();

        if(dd != null)
        {
            takeDamage(dd.getDamage());
            playEffectDamage();
            dd.killYourSelf();
        }
    }

    public int getHealth()
    {
        return m_heath;
    }

}
