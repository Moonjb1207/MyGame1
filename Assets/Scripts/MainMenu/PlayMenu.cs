using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] List<Sprite> StageIMG = new List<Sprite>();

    public Image curStageIMG;
    public GameObject Lock;

    public int Stage = 0;
    //float moveSpeed = 1000.0f;
    //[SerializeField] List<Vector3> targetPos = new List<Vector3>();
    [SerializeField] Vector2 minmaxStage;

    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] Button play;
    [SerializeField] List<Button> StageButton = new List<Button>();

    StageSaveData stageUnlock = new StageSaveData();

    // Start is called before the first frame update
    void Start()
    {
        stageUnlock = SaveManager.Inst.LoadFile<StageSaveData>(SaveManager.Inst.StageSavefp);

        if (!SaveManager.Inst.IsExist)
        {
            StageSaveData data = new StageSaveData();
            data.isUnlock[0] = true;

            for (int i = 1; i < data.isUnlock.Length; i++)
            {
                data.isUnlock[i] = false;
            }

            SaveManager.Inst.SaveFile<StageSaveData>(SaveManager.Inst.StageSavefp, data);

            stageUnlock = SaveManager.Inst.LoadFile<StageSaveData>(SaveManager.Inst.StageSavefp);
        }
        isCanPlay_Button();

        Stage = 0;
        isCanPlay();
        ChangeStageIMG(Stage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void isCanPlay()
    {
        if (stageUnlock.isUnlock[Stage])
        {
            play.interactable = true;
            Lock.SetActive(false);
        }
        else
        {
            play.interactable = false;
            Lock.SetActive(true);
        }
    }

    void isCanPlay_Button()
    {
        for (int i = (int)minmaxStage.x; i < (int)minmaxStage.y; i++)
        {
            if (stageUnlock.isUnlock[i])
            {
                StageButton[i].GetComponent<PlayButtonLock>().LockIMG.SetActive(false);
            }
            else
            {
                StageButton[i].GetComponent<PlayButtonLock>().LockIMG.SetActive(true);
            }
        }
    }

    public void NextStage()
    {
        if (++Stage < (int)minmaxStage.y)
        {
            //StartCoroutine(Next());
            isCanPlay();
            ChangeStageIMG(Stage);
        }
        else
        {
            Stage--;
        }
    }

    public void PrevStage()
    {
        if (--Stage >= (int)minmaxStage.x)
        {
            //StartCoroutine(Prev());
            isCanPlay();
            ChangeStageIMG(Stage);
        }
        else
        {
            Stage++;
        }
    }

    /*
    IEnumerator Next()
    {
        next.interactable = false;
        prev.interactable = false;
        play.interactable = false;

        Vector3 QuitTarget = targetPos[0] - StageIMG[Stage].rectTransform.position;
        Vector3 InTarget = targetPos[1] - StageIMG[Stage + 1].rectTransform.position;

        float qdist = 0.0f;
        float idist = 0.0f;

        float qtdist = QuitTarget.magnitude;
        float itdist = InTarget.magnitude;

        QuitTarget.Normalize();
        InTarget.Normalize();

        StageIMG[Stage + 1].enabled = true;

        while (qtdist - qdist > 0.0f && itdist - idist > 0.0f)
        {
            move(StageIMG[Stage].rectTransform, qtdist, ref qdist, QuitTarget);
            move(StageIMG[Stage + 1].rectTransform, itdist, ref idist, InTarget);

            yield return null;
        }

        StageIMG[Stage].enabled = false;
        ++Stage;

        next.interactable = true;
        prev.interactable = true;
        isCanPlay();
    }

    IEnumerator Prev()
    {
        next.interactable = false;
        prev.interactable = false;
        play.interactable = false;

        Vector3 QuitTarget = targetPos[2] - StageIMG[Stage].rectTransform.position;
        Vector3 InTarget = targetPos[1] - StageIMG[Stage - 1].rectTransform.position;

        float qdist = 0.0f;
        float idist = 0.0f;

        float qtdist = QuitTarget.magnitude;
        float itdist = InTarget.magnitude;

        QuitTarget.Normalize();
        InTarget.Normalize();

        StageIMG[Stage - 1].enabled = true;

        while (qtdist - qdist > 0.0f && itdist - idist > 0.0f)
        {
            move(StageIMG[Stage].rectTransform, qtdist, ref qdist, QuitTarget);
            move(StageIMG[Stage - 1].rectTransform, itdist, ref idist, InTarget);

            yield return null;
        }

        StageIMG[Stage].enabled = false;
        --Stage;

        next.interactable = true;
        prev.interactable = true;
        isCanPlay();
    }

    void move(RectTransform t, float td, ref float cd, Vector3 dir)
    {
        float delta = moveSpeed * Time.deltaTime;

        if (delta > td - cd)
        {
            delta = td - cd;
        }

        cd += delta;

        t.anchoredPosition3D += dir * delta;
    }
    */

    public void selectStage()
    {
        LoadManager.Inst.selectStage = Stage;
    }

    public void ChangeStage(int num)
    {
        if (num >= minmaxStage.y || num < minmaxStage.x)
        {
            return;
        }
        Stage = num;

        isCanPlay();
        ChangeStageIMG(Stage);
    }

    void ChangeStageIMG(int stage)
    {
        curStageIMG.sprite = StageIMG[stage];

        if (stage > minmaxStage.y || stage < minmaxStage.x)
        {
            curStageIMG.sprite = Lock.GetComponentInChildren<Image>().sprite;
        }
    }
}
