using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化数据
        DataManager.Instance().Initialize();

        //显示主面板
        UIManager.Instance().ShowPanel<MainPanel>("MainPanel", E_UI_Layer.Bot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
