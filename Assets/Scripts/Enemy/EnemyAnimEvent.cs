using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyAnimEvent : MonoBehaviour
{
    public UnityEvent Attack = null;
    public UnityEvent Dead = null;
    public UnityEvent WarnEnd = null;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnDead()
    {
        Dead?.Invoke();
    }

    public void OnWarnEnd()
    {
        WarnEnd?.Invoke();
    }
}