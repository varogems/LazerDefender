using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int m_score;
    public static ScoreKeeper m_instance = null;

    private void Awake() {
        if(FindObjectsOfType(this.GetType()).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else 
        {
            DontDestroyOnLoad(this.gameObject);
            m_instance = this;
        }
    }


    public int getScore()
    {
        return  m_score;
    }

    public void resetScore()
    {
        m_score = 0;
    }

    public void collectScore(int score)
    {
        m_score += score;
        Mathf.Clamp(m_score, 0 , int.MaxValue);
    }



}
