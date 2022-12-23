using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    enum StageState
    {
        Menu, Start, Enemy, Boss, Clear, GameOver
    }

    public static StageSystem Inst = null;

    public Transform[] spawnPos;
    public StageData[] stageList;

    public List<Enemy> spawnEnemies = new List<Enemy>();
    
    StageState myState = StageState.Menu;
    public int stage;
    float stageTime;
    int clearEnemy;
    float startTime = 5.0f;
    float clearTime = 5.0f;
    float restTime;

    void ChangeState(StageState s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case StageState.Start:
                restTime = startTime;
                break;
            case StageState.Enemy:
                StartCoroutine(Spawning());
                stageTime = stageList[stage].StageTime;
                clearEnemy = 0;
                break;
            case StageState.Boss:
                stageTime = stageList[stage].BossTime;
                break;
            case StageState.Clear:
                restTime = clearTime;
                stage++;
                break;
            case StageState.GameOver:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case StageState.Start:
                restTime -= Time.deltaTime;
                if (restTime <= 0.0f)
                {
                    ChangeState(StageState.Enemy);
                }
                break;
            case StageState.Enemy:
                stageTime -= Time.deltaTime;

                if (stageTime <= 0.0f || clearEnemy == stageList[stage].EnemyCount)
                {
                    Running();
                    ChangeState(StageState.Boss);
                }
                break;
            case StageState.Boss:
                stageTime -= Time.deltaTime;

                if (stageTime <= 0.0f)
                {
                    if (clearEnemy == 1)
                    {
                        ChangeState(StageState.Clear);
                    }
                    else
                    {
                        ChangeState(StageState.GameOver);
                    }
                }
                break;
            case StageState.Clear:
                restTime -= Time.deltaTime;

                if (restTime <= 0.0f)
                {
                    ChangeState(StageState.Start);
                }
                break;
            case StageState.GameOver:
                break;
        }
    }

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(StageState.Start);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator Spawning()
    {
        while(spawnEnemies.Count != stageList[stage].EnemyCount && stageTime >= 0.0f)
        {
            

            yield return null;
        }
    }

    void Running()
    {
        for(int i = 0; i < spawnEnemies.Count; i++)
        {
            spawnEnemies[i].timetoRun();
        }
    }
}
