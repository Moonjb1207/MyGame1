using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AnimEvent : MonoBehaviour
{
    public UnityEvent lAttack = null;
    public UnityEvent hAttack = null;
    public UnityEvent jAttack = null;
    public UnityEvent Attack3 = null;
    public UnityEvent moveAttack = null;
    public UnityEvent specialAttack = null;
    public UnityEvent ComboCheckStart = null;
    public UnityEvent ComboCheckEnd = null;
    public UnityEvent Dead = null;

    public void OnDead()
    {
        Dead?.Invoke();
    }

    public void OnlAttack()
    {
        lAttack?.Invoke();
    }

    public void OnhAttack()
    {
        hAttack?.Invoke();
    }

    public void OnjAttack()
    {
        jAttack?.Invoke();
    }

    public void OnAttack3()
    {
        Attack3?.Invoke();
    }

    public void OnmoveAttack()
    {
        moveAttack?.Invoke();
    }
    public void OnspecialAttack()
    {
        specialAttack?.Invoke();
    }

    public void OnComboCheckStart()
    {
        ComboCheckStart?.Invoke();
    }

    public void OnComboCheckEnd()
    {
        ComboCheckEnd?.Invoke();
    }

}
