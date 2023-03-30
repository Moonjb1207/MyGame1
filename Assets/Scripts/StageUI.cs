using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public static StageUI Inst = null;

    [SerializeField] Slider player;
    [SerializeField] Slider boss;
    [SerializeField] Slider time;
    [SerializeField] TMPro.TMP_Text score;
    [SerializeField] TMPro.TMP_Text stage;
    [SerializeField] TMPro.TMP_Text explain;
    [SerializeField] Image playerImg;
    [SerializeField] Image bossImg;
    [SerializeField] Image aim;
    [SerializeField] Image lattack;
    [SerializeField] Image lattackDelay;
    [SerializeField] Image hattack;
    [SerializeField] Image hattackDelay;
    [SerializeField] RectTransform pfill;
    [SerializeField] RectTransform bfill;

    public GameObject Bossslider;

    public Slider Player
    {
        get => player;
    }

    public Slider Boss
    {
        get => boss;
    }

    public Slider Time
    {
        get => time;
    }

    public TMPro.TMP_Text Score
    {
        get => score;
    }

    public TMPro.TMP_Text Stage
    {
        get => stage;
    }

    public TMPro.TMP_Text Explain
    {
        get => explain;
    }

    public Image PlayerIMG
    {
        get => playerImg;
    }

    public Image BossIMG
    {
        get => bossImg;
    }

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player.fillRect = pfill;
        Boss.fillRect = bfill;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = StageSystem.Inst.Score.ToString();
    }
}
