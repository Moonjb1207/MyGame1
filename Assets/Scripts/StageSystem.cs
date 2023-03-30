using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    public enum StageState
    {
        Menu, Start, Enemy, Boss, Clear, GameOver
    }

    public static StageSystem Inst = null;

    public Transform[] spawnPos;
    public StageData[] stageList;
    public string[] explains;
    public GameObject GameOver;
    public GameObject Clear;
    public GameObject Stagenum;


    public TMPro.TMP_Text stageNum;

    public Sprite noneBoss;

    public List<Enemy> spawnEnemies = new List<Enemy>();

    public int Score = 0;
    public StageState myState = StageState.Menu;
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
                StageUI.Inst.Stage.text = "Stage " + (stage + 1);
                StageUI.Inst.Explain.text = explains[0];
                StageUI.Inst.BossIMG.sprite = noneBoss;
                StageUI.Inst.Boss.value = stageList[stage].Boss.maxHP / stageList[stage].Boss.maxHP;

                Stagenum.SetActive(true);
                stageNum.text = "Stage " + (stage + 1);
                fadeAway(Stagenum.GetComponentInChildren<Image>());
                fadeAway(stageNum);
                break;
            case StageState.Enemy:
                StartCoroutine(Spawning());
                stageTime = stageList[stage].StageTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].StageTime;
                StageUI.Inst.Explain.text = explains[1];
                Stagenum.SetActive(false);
                clearEnemy = 0;
                break;
            case StageState.Boss:
                spawnEnemies.Clear();
                spawnEnemy = 0;
                StopAllCoroutines();

                BossSpawn();
                stageTime = stageList[stage].BossTime;
                StageUI.Inst.Time.value = stageTime / stageList[stage].BossTime;
                StageUI.Inst.Explain.text = explains[2];
                StageUI.Inst.BossIMG.sprite = stageList[stage].Boss.myIMG;
                clearEnemy = 0;
                break;
            case StageState.Clear:
                restTime = clearTime;
                StageUI.Inst.Time.value = restTime / clearTime;
                StageUI.Inst.Explain.text = explains[3];
                stage++;
                
                if (stage > 1)
                {
                    ChangeState(StageState.GameOver);
                }
                break;
            case StageState.GameOver:
                StageUI.Inst.Explain.text = explains[4];
                GameOver.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
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

                if (clearEnemy == 1)
                {
                    ChangeState(StageState.Clear);
                }
                else
                {
                    if (stageTime <= 0.0f)
                    {
                        ChangeState(StageState.GameOver);
                    }
                }
                break;
            case StageState.Clear:
                Clear.SetActive(true);
                fadeAway(Clear.GetComponentInChildren<Image>());
                fadeAway(Clear.GetComponentInChildren<TMPro.TMP_Text>());
                restTime -= Time.deltaTime;
                StageUI.Inst.Time.value = restTime / clearTime;

                StageSaveData data = SaveManager.Inst.LoadFile<StageSaveData>(Application.dataPath + @"\Stage.data");
                data.isUnlock[stage] = true;
                SaveManager.Inst.SaveFile<StageSaveData>(Application.dataPath + @"\Stage.data", data);

                if (restTime <= 0.0f)
                {
                    ChangeState(StageState.Start);
                    Clear.SetActive(false);
                }
                break;
            case StageState.GameOver:
                break;
        }
    }

    private void Awake()
    {
        Inst = this;

        //for test
        stage = LoadManager.Inst.selectStage;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(StageState.Start);

        BGSoundManager.Inst.myBG.clip = BGSoundManager.Inst.PlayBG;
        BGSoundManager.Inst.myBG.Play();
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

            GameObject obj = Instantiate(Resources.Load("Prefabs/Stage/Enemy" + stage) as GameObject, 
                spawnPos[Random.Range(0,3)].position, spawnPos[Random.Range(0, 3)].rotation);

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
            if (spawnEnemies[i].IsLive)
                spawnEnemies[i].timetoRun();
        }
    }

    void fadeAway(Image x)
    {
        StartCoroutine(Fade(x));
    }

    IEnumerator Fade(Image x)
    {
        while (x.color.a > 0.0f)
        {
            x.color = new Color(x.color.r, x.color.g, x.color.b, x.color.a - Time.deltaTime / 4);

            yield return null;
        }
    }

    void fadeAway(TMPro.TMP_Text x)
    {
        StartCoroutine(Fade(x));
    }

    IEnumerator Fade(TMPro.TMP_Text x)
    {
        while (x.color.a > 0.0f)
        {
            x.color = new Color(x.color.r, x.color.g, x.color.b, x.color.a - Time.deltaTime / 4);

            yield return null;
        }
    }

    public void PlayerDead()
    {
        ChangeState(StageState.GameOver);
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
