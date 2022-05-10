using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private GamePlayVariables m_GamePlayVariables => GameConfig.Instance.m_GamePlayVariables;

    private int     m_Health;
    private Vector2 m_Input;

    [SerializeField] private FloatingJoystick m_Joystick;
    [SerializeField] private Rigidbody2D m_Rigidbody;
    [SerializeField] private Gun        m_Gun;
    
    [Button]
    private void SetRef()
    {
        m_Joystick  = FindObjectOfType<FloatingJoystick>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        m_Health = m_GamePlayVariables.Health;

        GameManager.Instance.OnGameReset += onReset;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= onReset;
    }

    private void Update()
    {
        m_Input = new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical);
    }

    private void FixedUpdate()
    {
        var pos = (Vector2) m_Rigidbody.position;
        pos += m_Input * m_GamePlayVariables.MoveSpeed;

        m_Rigidbody.MovePosition(pos);
    }

    private void OnTriggerEnter2D(Collider2D i_Col)
    {
        m_Health--;
        if (m_Health <= 0)
        {
            dead();
        }
    }

    private void dead()
    {
        GameManager.Instance.LevelFailed();
    }

    private void onReset()
    {
        m_Health = m_GamePlayVariables.Health;
    }
}