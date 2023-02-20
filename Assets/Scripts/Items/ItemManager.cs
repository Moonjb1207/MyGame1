using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Inst = null;

    private void Awake()
    {
        if (Inst == null)
            Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPotion(Vector3 pos, int i = -1)
    {
        int rnd = Random.Range(0, 3);

        if (i == -1)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Potions/Potion" + rnd) as GameObject, pos, Quaternion.identity);
        }
        else
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Potions/Potion" + i) as GameObject, pos, Quaternion.identity);
        }
    }
}
