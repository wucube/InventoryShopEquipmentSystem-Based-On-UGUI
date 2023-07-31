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
            UIManager.Instance.ShowPanel<BagPanel>("BagPanel");
        });

        //监听商店按钮事件，点击后打开商店面板
        GetControl<Button>("btnShop").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<ShopPanel>("ShopPanel");
        });

        //监听增加金钱的按钮
        GetControl<Button>("btnAddMoney").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("MoneyChange", 1000);
        });

        //监听增加宝石数量的按钮
        GetControl<Button>("btnAddGem").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("GemChange", 1000);
        });


    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新名字 等级 钱等基础信息
        GetControl<Text>("txtName").text = DataManager.Instance.playerInfo.name;
        GetControl<Text>("txtLev").text = DataManager.Instance.playerInfo.lev.ToString();
        GetControl<Text>("txtMoney").text = DataManager.Instance.playerInfo.money.ToString();
        GetControl<Text>("txtGem").text = DataManager.Instance.playerInfo.gem.ToString();
        GetControl<Text>("txtPro").text = DataManager.Instance.playerInfo.pro.ToString();

        EventCenter.Instance.AddEventListener<int>("MoneyChange", UpdateMoneyInfo);
        EventCenter.Instance.AddEventListener<int>("GemChange", UpdateMoneyInfo);
    }


    public override void HideMe()
    {
        base.HideMe();

        EventCenter.Instance.RemoveEventListener<int>("MoneyChange", UpdateMoneyInfo);
        EventCenter.Instance.RemoveEventListener<int>("GemChange", UpdateMoneyInfo);
    }

    /// <summary>
    /// 更新玩家的金钱信息
    /// </summary>
    public void UpdateMoneyInfo(int money)
    {
        GetControl<Text>("txtMoney").text = DataManager.Instance.playerInfo.money.ToString();
        GetControl<Text>("txtGem").text = DataManager.Instance.playerInfo.gem.ToString();
        
    }
}
