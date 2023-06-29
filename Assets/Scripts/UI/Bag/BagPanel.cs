using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        //GetControl<Button>("btnClose").onClick.AddListener(() =>
        //{
        //    UIManager.Instance().HidePanel("BagPanel");
        //});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //为背包面板的关闭按钮绑定事件
    protected override void OnClick(string bagPanelName)
    {
        bagPanelName = "BagPanel";
        UIManager.Instance().HidePanel(bagPanelName);
    }
}
