using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : BasePanel
{
    private ShopCellInfo info;


    void Start()
    {
        GetControl<Button>("btnBuy").onClick.AddListener(() =>
        {
            BuyItem(DataManager.Instance.playerInfo);

        });
    }


    /// <summary>
    /// 初始化商店物品中的单个道具信息
    /// </summary>
    /// <param name="info"></param>
    public void Initialize(ShopCellInfo info)
    {
        this.info = info;
        //根据售卖的道具id,得到道具表信息
        Item item = DataManager.Instance.GetItemInfo(info.itemInfo.id);

        //图标
        GetControl<Image>("imgIcon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + item.icon);
        //道具个数
        GetControl<Text>("txtNum").text = info.itemInfo.num.ToString();
        //道具名称
        GetControl<Text>("txtName").text = item.name;
        //购买道具所消耗的货币类型
        GetControl<Image>("imgPType").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + (info.priceType==1?"7":"8"));
        //价格
        GetControl<Text>("txtPrice").text = info.price.ToString();
        //描述信息
        GetControl<Text>("txtTips").text = info.tips;

    }


    public void BuyItem(PlayerInfo playerInfo)
    {
        //是否为指定货币类型，并且玩家持有的货币足够消耗
        if(info.priceType == 1 && playerInfo.money >= info.price)
        {
            //添加物品给玩家
            DataManager.Instance.playerInfo.AddItem(info.itemInfo);

            //事件分发 玩家金钱改变
            EventCenter.Instance.EventTrigger<int>("MoneyChange", -info.price);

            //显示提示面板
            TipMgr.Instance.ShowOneBtnTip("购买成功");
        }

        else if (info.priceType == 2 && playerInfo.gem >= info.price)
        {
            //添加物品给玩家
            DataManager.Instance.playerInfo.AddItem(info.itemInfo);

            EventCenter.Instance.EventTrigger<int>("GemChange", -info.price);

            TipMgr.Instance.ShowOneBtnTip("购买成功");
        }

        else
        {
            TipMgr.Instance.ShowOneBtnTip("货币不足");
        }
    }
}
