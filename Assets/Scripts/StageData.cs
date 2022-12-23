using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct StageEnemy
{
    public float maxHP;
    public float moveSpeed;
    public float attackDelay;

    public StageEnemy(float mh, float ms, float ad)
    {
        maxHP = mh;
        moveSpeed = ms;
        attackDelay = ad;
    }
}

[Serializable]
public struct StageBoss
{
    public float maxHP;
    public float moveSpeed;
    public float attackDelay;

    public StageBoss(float mh, float ms, float ad)
    {
        maxHP = mh;
        moveSpeed = ms;
        attackDelay = ad;
    }
}

[CreateAssetMenu(fileName = "Stage Data", menuName = "ScriptableObject/Stage Data", order = -1)]
public class StageData : ScriptableObject
{
    [SerializeField] StageEnemy enemy;
    [SerializeField] StageBoss boss;
    [SerializeField] float stageTime;
    [SerializeField] float bossTime;
    [SerializeField] int enemyCount;
    [SerializeField] int stageLevel;

    public float StageTime
    {
        get => stageTime;
    }

    public float BossTime
    {
        get => bossTime;
    }

    public int EnemyCount
    {
        get => enemyCount;
    }

    public int StageLevel
    {
        get => stageLevel;
    }

    public StageEnemy Enemy
    {
        get => enemy;
    }
}
