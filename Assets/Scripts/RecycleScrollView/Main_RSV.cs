using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_RSV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化道具信息
        BagMgr.Instance.InitItemInfo();
        //显示背包面板
        UIManager.Instance.ShowPanel<BagPanel_RSV>("BagPanel_RSV");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
