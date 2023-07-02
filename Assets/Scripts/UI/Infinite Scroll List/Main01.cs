using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化道具信息
        BagMgr.Instance().InitItemInfo();
        //显示背包面板
        UIManager.Instance().ShowPanel<BagPanel01>("BagPanel01");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
