using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] Slider             m_healthUI;
    [SerializeField] TextMeshProUGUI    m_scoreUI;
    [SerializeField] Health             m_healthPlayer;
    [SerializeField]GameObject          m_player;
    ScoreKeeper                         m_scoreKeeper;

    private void Awake() 
    {
        m_scoreKeeper = FindObjectOfType<ScoreKeeper>();
        m_healthUI.maxValue = m_healthPlayer.getHealth();
        // m_healthUI.maxValue = m_player.GetComponent<Health>().getHealth();
    }


    void Update() 
    {
        m_healthUI.value = m_healthPlayer.getHealth();
        // m_healthUI.value = m_player.GetComponent<Health>().getHealth();
        m_scoreUI.text = m_scoreKeeper.getScore().ToString("00000000");
    }


}
