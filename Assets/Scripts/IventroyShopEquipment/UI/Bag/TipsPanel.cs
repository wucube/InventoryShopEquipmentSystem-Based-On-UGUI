using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    /// <summary>
    /// 初始化tips面板信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ItemInfo info)
    {
        //根据道具信息数量，更新格子对象
        Item itemData = DataManager.Instance.GetItemInfo(info.id);

        //通过道具ID得到道具表中的数据信息，加载对应的道具信息
        //图标
        GetControl<Image>("imgIcon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.icon);
        //数量
        GetControl<Text>("txtNum").text = "数量:" + info.num.ToString();
        //名字
        GetControl<Text>("txtName").text = itemData.name;
        //描述
        GetControl<Text>("txtTips").text = itemData.tips;
    }
}
