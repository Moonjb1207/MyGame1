using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Inst = null;

    public int selectStage = 0;

    private void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}