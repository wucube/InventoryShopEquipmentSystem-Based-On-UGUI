using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : BasePanel, IItemInit<ItemInfo>
{
    private ItemInfo itemInfo;

    void Start()
    {
        Image imgIcon = GetControl<Image>("imgIcon");
        //用UI管理器提供的添加自定义事件监听的方法，将对应的函数和事件关联
        UIManager.AddCustomEventListener(imgIcon, EventTriggerType.PointerEnter, EnterItemCell);
        UIManager.AddCustomEventListener(imgIcon, EventTriggerType.PointerExit, ExitItemCell);


        /* 手动监听鼠标移入 鼠标移除的事件 来进行处理
        EventTrigger trigger = GetControl<Image>("imgIcon").gameObject.AddComponent<EventTrigger>();

        //声明一个 鼠标进入的事件类对象
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(EnterItemCell);

        //声明一个鼠标移除的事件类对象
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener(ExitItemCell);

        trigger.triggers.Add(enter);
        trigger.triggers.Add(exit);
        */

    }

    /// <summary>
    /// 鼠标进入格子时的事件
    /// </summary>
    /// <param name="data"></param>
    private void EnterItemCell(BaseEventData data)
    {
        //显示提示面板
        UIManager.Instance.ShowPanel<TipsPanel>("TipsPanel", E_UI_Layer.Top, (panel) =>
        {
            //面板异步加载完成后，更新信息
            panel.InitInfo(itemInfo);
            //更新位置
            panel.transform.position = GetControl<Image>("imgIcon").transform.position;
        });
    }
    /// <summary>
    /// 鼠标退出格子里的事件
    /// </summary>
    /// <param name="data"></param>
    private void ExitItemCell(BaseEventData data)
    {
        UIManager.Instance.HidePanel("TipsPanel");
    }

    public void InitInfo(ItemInfo info)
    {
        itemInfo = info;
        //根据道具信息的数据，更新格子对象
        Item itemData = DataManager.Instance.GetItemInfo(info.id);
        //加载图标
        GetControl<Image>("imgIcon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.icon);
        //更新数量
        GetControl<Text>("txtNum").text = info.num.ToString();
    }
}
