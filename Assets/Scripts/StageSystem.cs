using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    enum StageState
    {
        Start, Enemy, Boss, Clear, GameOver
    }

    public Transform[] spawnPos;
    public StageData[] stageList;

    StageState myState = StageState.Start;
    int stage;
    float stageTime;
    float bossTime;

    void ChangeState(StageState s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case StageState.Enemy:
                stageTime = stageList[stage].StageTime;
                break;
            case StageState.Boss:
                bossTime = stageList[stage].BossTime;
                break;
            case StageState.Clear:
                break;
            case StageState.GameOver:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case StageState.Enemy:
                stageTime -= Time.deltaTime;

                if (stageTime <= 0.0f)
                {
                    ChangeState(StageState.Boss);
                }
                break;
            case StageState.Boss:
                bossTime -= Time.deltaTime;
                break;
            case StageState.Clear:
                break;
            case StageState.GameOver:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(StageState.Enemy);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator Spawning()
    {
        while(stageList[stage].EnemyCount != 0 && stageTime >= 0.0f)
        {


            yield return null;
        }
    }

    bool IsClear()
    {
        return true;
    }
}
