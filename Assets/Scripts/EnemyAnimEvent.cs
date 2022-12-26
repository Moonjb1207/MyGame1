using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyAnimEvent : MonoBehaviour
{
    public UnityEvent Attack = null;

    public void OnAttack()
    {
        Attack?.Invoke();
    }
}