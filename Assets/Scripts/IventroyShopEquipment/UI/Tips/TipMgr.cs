using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipMgr : BaseManager<TipMgr>
{

    public void ShowOneBtnTip(string info)
    {
        UIManager.Instance.ShowPanel<OneBtnTipPanel>("OneBtnTipPanel", E_UI_Layer.System,(panel) =>
        {
            panel.InitInfo(info);
        });
    }
    
}
