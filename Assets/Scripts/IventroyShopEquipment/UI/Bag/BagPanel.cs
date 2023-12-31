using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_Bag_Type
{
    Item = 1,
    Equip,
    Gem
}

public class BagPanel : BasePanel
{
    // ScrollView 下的 Content 对象
    public RectTransform content;

    public int scrollViewHeigh = 652;

    private RecycleScrollView<ItemInfo, ItemCell> recycleSV;

    /// <summary>
    /// 存储显示出的格子对象
    /// </summary>
    private List<ItemCell> cellList = new List<ItemCell>();
    
    void Start()
    {
        //为按钮组件添加事件监听
        GetControl<Button>("btnClose").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("BagPanel");
            
        });

        //为Toggle添加事件监听，触发数据更新
        GetControl<Toggle>("togItem").onValueChanged.AddListener(ToggleValueChange);
        GetControl<Toggle>("togEquip").onValueChanged.AddListener(ToggleValueChange);
        GetControl<Toggle>("togGem").onValueChanged.AddListener(ToggleValueChange);

    }

    void Update()
    {
        //recycleSV.CheckShowOrHide();
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //recycleSV = new RecycleScrollView<ItemInfo, ItemCell>();

        //recycleSV.InitItemResName("UI/ItemCell");
        //recycleSV.InitItemSizeAndCol(200, 200, 3);
        //recycleSV.InitContenAndSVH(content, scrollViewHeigh);

        ChangeType(E_Bag_Type.Item);

        //Debug.Log("222222222222222");
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
        List<ItemInfo> tempInfo = DataManager.Instance.playerInfo.items;


        switch (type)
        {
            case E_Bag_Type.Equip:
                tempInfo = DataManager.Instance.playerInfo.equips;
                break;
            case E_Bag_Type.Gem:
                tempInfo = DataManager.Instance.playerInfo.gems;
                break;
        }

        //删除之前的格子
        for (int i = 0; i < cellList.Count; i++)
            Destroy(cellList[i].gameObject);
        cellList.Clear();

        //更新现在的数据
        for (int i = 0; i < tempInfo.Count; i++)
        {
            //实例化预设体，并得到挂载的ItemCell脚本
            ItemCell cell = ResMgr.Instance.Load<GameObject>("UI/ItemCell").GetComponent<ItemCell>();
            cell.transform.SetParent(content);
            cell.InitInfo(tempInfo[i]);
            cellList.Add(cell);
        }

        //recycleSV.InitInfos(tempInfo);
        //recycleSV.UpdateView();
    }

}
