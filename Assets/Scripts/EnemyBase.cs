using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected float StartHealth;

    protected float Health;
    protected bool  IsAlive = true;
}