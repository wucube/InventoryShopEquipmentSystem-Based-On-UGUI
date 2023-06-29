using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    // Start is called before the first frame update
    void Start()
    {
        GetControl<Button>("btnRole").onClick.AddListener(() =>
        {
            UIManager.Instance().ShowPanel<BagPanel>("BagPanel");
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新名字 等级 钱等基础信息
        GetControl<Text>("txtName").text = DataManager.Instance().playerInfo.name;
        GetControl<Text>("txtLev").text = DataManager.Instance().playerInfo.lev.ToString();
        GetControl<Text>("txtMoney").text = DataManager.Instance().playerInfo.money.ToString();
        GetControl<Text>("txtGem").text = DataManager.Instance().playerInfo.gem.ToString();
        GetControl<Text>("txtPro").text = DataManager.Instance().playerInfo.pro.ToString();

    }
}
