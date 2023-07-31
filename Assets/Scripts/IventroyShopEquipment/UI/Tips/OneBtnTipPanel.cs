using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一键提示面板
/// </summary>
public class OneBtnTipPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        GetControl<Button>("btnSure").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("OneBtnTipPanel");
        });
    }

    // Update is called once per frame
    public void InitInfo(string tipsInfo)
    {
        GetControl<Text>("txtInfo").text = tipsInfo;
    }
}
