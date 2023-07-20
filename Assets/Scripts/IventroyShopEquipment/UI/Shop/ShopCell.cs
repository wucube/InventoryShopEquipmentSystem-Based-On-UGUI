using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : BasePanel
{
    private ShopCellInfo info;

    /// <summary>
    /// 初始化商店物品中的单个道具信息
    /// </summary>
    /// <param name="info"></param>
    public void Initialize(ShopCellInfo info)
    {
        this.info = info;
        //根据售卖的道具id,得到道具表信息
        Item item = DataManager.Instance().GetItemInfo(info.itemInfo.id);

        //图标
        GetControl<Image>("imgIcon").sprite = ResMgr.Instance().Load<Sprite>("Icon/" + item.icon);
        //道具个数
        GetControl<Text>("txtNum").text = info.itemInfo.num.ToString();
        //道具名称
        GetControl<Text>("txtName").text = item.name;
        //购买道具所消耗的货币类型
        GetControl<Image>("imgPType").sprite = ResMgr.Instance().Load<Sprite>("Icon/" + (info.priceType==1?"5":"6"));
        //价格
        GetControl<Text>("txtPrice").text = info.price.ToString();
        //描述信息
        GetControl<Text>("txtTips").text = info.tips;

    }
}
