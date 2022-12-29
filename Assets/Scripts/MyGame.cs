using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGame : MonoBehaviour
{
    public static MyGame Inst = null;
    public StageSystem mySystem = null;
    public StageUI myUI = null;
    

    // Start is called before the first frame update
    void Start()
    {
        Inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
