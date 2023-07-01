using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_Bag_Type
{
    Item,
    Equip,
    Gem
}

public class BagPanel : BasePanel
{
    public Transform content;

    //存储显示出的格子对象
    private List<ItemCell> list = new List<ItemCell>();
    
    void Start()
    {
        //GetControl<Button>("btnClose").onClick.AddListener(() =>
        //{
        //    UIManager.Instance().HidePanel("BagPanel");
        //});

        //为Toggle添加事件监听，触发数据更新
        GetControl<Toggle>("togItem").onValueChanged.AddListener(ToggleValueChange);
        GetControl<Toggle>("togEquip").onValueChanged.AddListener(ToggleValueChange);
        GetControl<Toggle>("togGem").onValueChanged.AddListener(ToggleValueChange);
    }


    public override void ShowMe()
    {
        base.ShowMe();
        ChangeType(E_Bag_Type.Item);
    }

    //为背包面板的关闭按钮绑定事件
    protected override void OnClick(string bagPanelName)
    {
        bagPanelName = "BagPanel";
        UIManager.Instance().HidePanel(bagPanelName);
    }

    /// <summary>
    /// 单选框页签状态改变时的逻辑处理
    /// </summary>
    /// <param name="value"></param>
    private void ToggleValueChange(bool value)
    {
        if (GetControl<Toggle>("togItem").isOn)
        {
            ChangeType(E_Bag_Type.Item);
        }
        else if (GetControl<Toggle>("togEquip").isOn)
        {
            ChangeType(E_Bag_Type.Equip);
        }
        else
        {
            ChangeType(E_Bag_Type.Gem);
        }
    }

    /// <summary>
    /// 页签切换函数
    /// </summary>
    /// <param name="type"></param>
    private void ChangeType(E_Bag_Type type)
    {
        //根据传入的页签信息，更新相应数据

        //默认值为道具列表信息
        List<ItemInfo> tempInfo = DataManager.Instance().playerInfo.items;

        switch (type)
        {
            case E_Bag_Type.Equip:
                tempInfo = DataManager.Instance().playerInfo.equips;
                break;
            case E_Bag_Type.Gem:
                tempInfo = DataManager.Instance().playerInfo.gems;
                break;
        }
        //删除之前的格子
        for (int i = 0; i < list.Count; i++)
            Destroy(list[i].gameObject);
        list.Clear();

        //更新现在的数据
        for (int i = 0; i < tempInfo.Count; i++)
        {
            //实例化预设体，并得到挂载的ItemCell脚本
            ItemCell cell = ResMgr.Instance().Load<GameObject>("UI/ItemCell").GetComponent<ItemCell>();
            cell.transform.SetParent(content);
            cell.InitInfo(tempInfo[i]);
            list.Add(cell);
        }
    }

}
