using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst = null;

    public Player[] players = new Player[2];
    [SerializeField] int curIndex;
    public SpringArm myCam;

    [SerializeField]Animator myAnim;
    Rigidbody myRigid;
    LayerMask myGround;

    bool IsRunning;
    int[] playCombo = new int[2];
    int pressedButton = -1;
    bool IsRightCombo = false;
    string comboName = "";

    List<List<int>> combolAttackList = new List<List<int>>();
    List<List<int>> combohAttackList = new List<List<int>>();
    float JumpPower;
    float DownPower;

    Vector3 curPos = Vector3.zero;

    private void Awake()
    {
        Inst = this;

        for (int i = 0; i < 2; i++)
        {
            players[i].gameObject.SetActive(false);
        }

        players[curIndex].gameObject.SetActive(true);

        myCam.transform.SetParent(players[curIndex].transform);

        players[curIndex].InCharc();

        curIndex = 0;
        myAnim = players[curIndex].myAnim;
        myRigid = players[curIndex].myRigid;
        myGround = players[curIndex].myGround;
        combolAttackList = players[curIndex].combolAttackList;
        combohAttackList = players[curIndex].combohAttackList;
        JumpPower = players[curIndex].JumpPower;
        DownPower = players[curIndex].DownPower;

        StageUI.Inst.PlayerIMG.sprite = players[curIndex].myIMG;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    void ChangeCharc()
    {
        players[curIndex].OutCharc();

        myCam.transform.SetParent(this.transform);

        curPos = players[curIndex].transform.position;

        players[curIndex].gameObject.SetActive(false);

        curIndex++;

        if (curIndex > 1)
            curIndex = 0;

        myAnim = players[curIndex].myAnim;
        myRigid = players[curIndex].myRigid;
        myGround = players[curIndex].myGround;
        combolAttackList = players[curIndex].combolAttackList;
        combohAttackList = players[curIndex].combohAttackList;
        JumpPower = players[curIndex].JumpPower;
        DownPower = players[curIndex].DownPower;

        players[curIndex].transform.position = curPos;

        players[curIndex].gameObject.SetActive(true);

        myCam.transform.SetParent(players[curIndex].transform);

        players[curIndex].InCharc();
        ChangeCharcIMG();
    }

    void ChangeCharcIMG()
    {
        StageUI.Inst.PlayerIMG.sprite = players[curIndex].myIMG;
    }

    
    void Controller()
    {
        //Moving
        if (!myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                IsRunning = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsRunning = false;
            }

            if (!IsRunning)
            {
                myAnim.SetFloat("x", Input.GetAxis("Horizontal") * 0.5f);
                myAnim.SetFloat("y", Input.GetAxis("Vertical") * 0.5f);
            }
            else
            {
                myAnim.SetFloat("x", Input.GetAxis("Horizontal") * 0.5f);
                myAnim.SetFloat("y", Input.GetAxis("Vertical"));
            }
        }

        //Attack
        if (!myAnim.GetBool("IsAir"))
        {
            //=========lAttack Combo==========
            if (!myAnim.GetBool("IsAttacking"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    players[curIndex].lAttack();

                    for (int i = 0; i < 2; i++)
                    {
                        playCombo[i] = i;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        pressedButton = 0;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        pressedButton = 1;
                    }

                    if (players[curIndex].IsComboable)
                    {
                        IsRightCombo = false;

                        int n = myAnim.GetInteger("ComboIndex");

                        if (++n > 3)
                        {
                            n = 0;

                            for (int i = 0; i < 2; i++)
                            {
                                playCombo[i] = i;
                            }
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            if (playCombo[i] != -1)
                            {
                                if (combolAttackList[i][n] != pressedButton)
                                {
                                    playCombo[i] = -1;
                                }
                                else if (combolAttackList[i][n] == pressedButton)
                                {
                                    comboName = "lAttack" + i;
                                    IsRightCombo = true;
                                }
                            }
                        }

                        if (!IsRightCombo)
                        {
                            comboName = "";
                            myAnim.SetInteger("ComboIndex", 0);
                        }
                        else
                        {
                            myAnim.SetInteger("ComboIndex", n);
                            myAnim.SetTrigger(comboName);
                        }
                    }
                }
            }
            //=========lAttack Combo==========

            //=========hAttack Combo==========
            if (!myAnim.GetBool("IsAttacking"))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    players[curIndex].hAttack();
                }
            }
            else
            {

            }
            //========hAttack Combo===========
        }

        //Jump
        if (!myAnim.GetBool("IsAir") && !myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnim.SetTrigger("Jump");
                myRigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }

        //Check Air
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f, myGround) && myAnim.GetBool("IsAir"))
        {
            myAnim.SetBool("IsAir", false);
            myAnim.SetTrigger("Landing");
        }

        //JumpAttack
        if (myAnim.GetBool("IsJumping") && players[curIndex].IsComboable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("jAttack");
                myRigid.AddForce(Vector3.down * DownPower, ForceMode.Impulse);
            }
        }

        //Change Char
        if (!myAnim.GetBool("IsJumping") && !myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ChangeCharc();
            }
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0.0f;
        }
    }
}
