using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyStat : MonoBehaviour
{
    [SerializeField] StageData[] myData;
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
    void Start()
    {
        maxHP = myData[StageSystem.Inst.stage].Enemy.maxHP;
        curHP = myData[StageSystem.Inst.stage].Enemy.maxHP;
        moveSpeed = myData[StageSystem.Inst.stage].Enemy.moveSpeed;
        attackDelay = myData[StageSystem.Inst.stage].Enemy.attackDelay;
        rotSpeed = 700.0f;
        damagedDelay = 0.0f;
        isdmgDelay = false;
    }
}