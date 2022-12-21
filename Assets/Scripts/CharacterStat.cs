using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct CharacterStat
{
    [SerializeField] float maxHP;
    [SerializeField] float curHP;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] float damagedDelay;
    [SerializeField] bool isdmgDelay;


    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
        }
    }

    public float CurHP
    {
        get { return curHP; }
        set
        {
            curHP = Math.Clamp(value, 0.0f, maxHP);
        }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
        }
    }

    public float RotSpeed
    {
        get { return rotSpeed; }
        set
        {
            rotSpeed = value;
        }
    }

    public float AttackDelay
    {
        get { return attackDelay; }
        set
        {
            attackDelay = value;
        }
    }

    public float DamagedDelay
    {
        get { return damagedDelay; }
        set
        {
            damagedDelay = value;
        }
    }

    public bool IsdmgDelay
    {
        get { return isdmgDelay; }
        set
        {
            isdmgDelay = value;
        }
    }

    public CharacterStat(float hp, float movs, float rots, float ad, float dmgd)
    {
        curHP = maxHP = hp;
        moveSpeed = movs;
        rotSpeed = rots;
        attackDelay = ad;
        damagedDelay = dmgd;
        isdmgDelay = false;
    }
}
