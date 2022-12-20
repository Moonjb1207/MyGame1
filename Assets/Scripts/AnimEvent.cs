using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AnimEvent : MonoBehaviour
{
    public UnityEvent lAttack = null;
    public UnityEvent hAttack = null;
    public UnityEvent ComboCheckStart = null;
    public UnityEvent ComboCheckEnd = null;

    public void OnComboCheckStart()
    {
        ComboCheckStart?.Invoke();
    }

    public void OnComboCheckEnd()
    {
        ComboCheckEnd?.Invoke();
    }

}
