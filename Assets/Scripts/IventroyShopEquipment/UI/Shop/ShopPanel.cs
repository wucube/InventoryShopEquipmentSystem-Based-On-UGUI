using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public Transform content;

    private void Start()
    {
        //得到关闭按钮 监听点击事件， 点击后关闭商店页面
        GetControl<Button>("btnClose").onClick.AddListener(() =>
        {
            UIManager.Instance().HidePanel("ShopPanel");
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //显示商店面板时，初始化商业面板中的信息。根据商店数据初始化
        for (int i = 0; i < DataManager.Instance().shopInfos.Count; i++)
        {
            //实例化商店中的物品，并得到其脚本
            ShopCell shopCell = ResMgr.Instance().Load<GameObject>("UI/ShopCell").GetComponent<ShopCell>();
            shopCell.transform.parent = content;
            shopCell.transform.localScale = Vector3.one;
            //初始化售卖道具的信息
            shopCell.Initialize(DataManager.Instance().shopInfos[i]);
        }
    }
}
