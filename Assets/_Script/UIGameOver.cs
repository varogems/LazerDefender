using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI m_UIGame;
    ScoreKeeper                     m_scoreKeeper;

    private void Awake() 
    {
        m_scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() 
    {
        if(m_scoreKeeper != null)
            m_UIGame.text = "Your score:\n" + m_scoreKeeper.getScore().ToString("00000000");
    }

}
