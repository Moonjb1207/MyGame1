using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    enum StageState
    {
        Menu, Start, Enemy, Boss, Clear, GameOver
    }

    public static StageSystem Inst = null;

    public Transform[] spawnPos;
    public StageData[] stageList;
    public string[] explains;

    public List<Enemy> spawnEnemies = new List<Enemy>();
    
    StageState myState = StageState.Menu;
    public int stage;
    float stageTime;
    public int clearEnemy;
    public int spawnEnemy;
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
                StageUI.Inst.Time.value = restTime / startTime;
                StageUI.Inst.Stage.text = "Stage " + stage;
                StageUI.Inst.Explain.text = explains[0];
                break;
            case StageState.Enemy:
                StartCoroutine(Spawning());
                stageTime = stageList[stage].StageTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].StageTime;
                StageUI.Inst.Explain.text = explains[1];
                clearEnemy = 0;
                break;
            case StageState.Boss:
                BossSpawn();
                stageTime = stageList[stage].BossTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].BossTime;
                StageUI.Inst.Explain.text = explains[2];
                clearEnemy = 0;
                break;
            case StageState.Clear:
                restTime = clearTime;
                StageUI.Inst.Time.value = restTime / clearTime;
                StageUI.Inst.Explain.text = explains[3];
                stage++;
                break;
            case StageState.GameOver:
                StageUI.Inst.Explain.text = explains[4];
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case StageState.Start:
                restTime -= Time.deltaTime;
                StageUI.Inst.Time.value = restTime / startTime;
                if (restTime <= 0.0f)
                {
                    ChangeState(StageState.Enemy);
                }
                break;
            case StageState.Enemy:
                stageTime -= Time.deltaTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].StageTime;
                if (stageTime <= 0.0f || clearEnemy == stageList[stage].EnemyCount)
                {
                    Running();
                    ChangeState(StageState.Boss);
                }
                break;
            case StageState.Boss:
                stageTime -= Time.deltaTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].BossTime;
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
                StageUI.Inst.Time.value = restTime / clearTime;
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
            //true면 다음 구문 실행
            yield return new WaitUntil(IsFull);
            yield return new WaitForSeconds(stageList[stage].SpawnTime);

            spawnEnemy++;

            GameObject obj = Instantiate(Resources.Load("Prefabs/Stage/Enemy") as GameObject, spawnPos[Random.Range(0,4)].position, spawnPos[Random.Range(0, 4)].rotation);

            Enemy scp = obj.GetComponent<Enemy>();
            spawnEnemies.Add(scp);
        }
    }

    void BossSpawn()
    {
        string a = stageList[stage].Boss.bossName;
        Instantiate(Resources.Load("Prefabs/Boss/" + a) as GameObject, spawnPos[1].position, spawnPos[1].rotation);
    }

    bool IsFull()
    {
        if (spawnEnemy == stageList[stage].SpawnCount)
            return false;
        return true;
    }

    void Running()
    {
        for(int i = 0; i < spawnEnemies.Count; i++)
        {
            spawnEnemies[i].timetoRun();
        }
    }

    void UpdateScore()
    {

    }

    void UpdateHP()
    {

    }

    void UpdateText()
    {

    }

    void UpdateImage()
    {

    }
}
