using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //0, 0 -> 포션 종류 / 0, 1 -> 포션 개수
    public int[,] inven = new int[3, 2];

    public SkillSlot[] mySlot = new SkillSlot[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            inven[i, 0] = -1;
            inven[i, 1] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void throwPotion(int i, int type)
    {
        for (int j = 0; j < inven[i, 1]; j++)
        {
            ItemManager.Inst.DropPotion(gameObject.GetComponentInChildren<Player>().gameObject.transform.position, type);
        }

        inven[i, 0] = -1;
        inven[i, 1] = 0;
    }

    public void use(int num)
    {
        inven[num, 1]--;
        if (inven[num, 1] == 0)
        {
            mySlot[num].GetComponentInChildren<SkillIcon>().countZero();
            inven[num, 0] = -1;
        }
    }

    public void changeSlot(int pre, int cur)
    {
        int temp1 = inven[pre, 0];
        int temp2 = inven[pre, 1];

        inven[pre, 0] = inven[cur, 0];
        inven[pre, 1] = inven[cur, 1];

        inven[cur, 0] = inven[pre, 0];
        inven[cur, 1] = inven[pre, 1];
    }

    public void get(int type)
    {
        for (int i = 0; i < 3; i++)
        {
            if (inven[i, 0] == -1)
            {
                inven[i, 0] = type;
                inven[i, 1]++;

                GameObject obj = Instantiate(Resources.Load("Prefabs/Potions/Potion" + type + "Icon") as GameObject);
                obj.GetComponent<SkillIcon>().SetParent(mySlot[i]);
                obj.GetComponent<SkillIcon>().type = type;
                obj.GetComponent<SkillIcon>().seat = i;

                break;
            }
            else
            {
                if (inven[i, 0] == type)
                {
                    inven[i, 1]++;
                    if (inven[i, 1] > 9)
                    {
                        inven[i, 1] = 9;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
