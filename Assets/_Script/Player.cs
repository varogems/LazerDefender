using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    [SerializeField] float  m_moveSpeed = 7.0f;

    Vector3                 m_vectorMove;

    Vector2                 m_minBound, m_maxBound;
    PlayerShooter           m_shooter;
    ShakingCamera           m_shakingCamera;
    AudioManager            m_audioManager;

    [Header("Margin")]
    [SerializeField] float  m_left;
    [SerializeField] float  m_right;    
    [SerializeField] float  m_top;
    [SerializeField] float  m_bottom;

    void Awake() 
    {
        FindObjectOfType<ScoreKeeper>().resetScore();
        m_shooter = this.GetComponent<PlayerShooter>();
        m_shakingCamera = FindObjectOfType<ShakingCamera>();
        m_audioManager = FindObjectOfType<AudioManager>();
    }


    void Start() 
    {
        InitBound();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(m_shakingCamera != null)
            m_shakingCamera.shakeScreen();

        if(m_audioManager != null)
            m_audioManager.playEffectAudio(AudioManager.EffectAudio.Explosion);

    }


    void Update()
    {
        Move();
    }

    void InitBound()
    {
        m_minBound = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        m_maxBound = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
    }

    void OnMove(InputValue value)
    {
        m_vectorMove = value.Get<Vector2>();
    }

    void Move()
    {
        Vector2 newPos = transform.position + m_vectorMove * m_moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, m_minBound.x + m_left, m_maxBound.x - m_right);
        newPos.y = Mathf.Clamp(newPos.y, m_minBound.y + m_bottom, m_maxBound.y - m_top);

        transform.position = newPos;
    }



    void OnFire(InputValue value)
    {
        if(m_shooter != null) m_shooter.setFire(value.isPressed);
    }


}
