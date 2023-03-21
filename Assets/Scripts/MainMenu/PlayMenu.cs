using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] List<Image> StageIMG = new List<Image>();
    public int Stage = 0;
    float moveSpeed = 1000.0f;
    [SerializeField] List<Vector3> targetPos = new List<Vector3>();
    [SerializeField] Vector2 minmaxStage;

    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] Button play;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void isCanPlay()
    {
        if (stageUnlock.isUnlock[Stage])
            play.interactable = true;
        else
            play.interactable = false;
    }

    public void NextStage()
    {
        if (Stage < minmaxStage.y)
            StartCoroutine(Next());
    }

    public void PrevStage()
    {
        if (Stage > minmaxStage.x)
            StartCoroutine(Prev());
    }

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

    public void selectStage()
    {
        LoadManager.Inst.selectStage = Stage;
    }
}
