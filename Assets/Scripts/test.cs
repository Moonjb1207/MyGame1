using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : CharacterMovement
{
    public GameObject playercam = null;

    // Start is called before the first frame update
    void Start()
    {
        StarePlayer(playercam.transform, 700.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
