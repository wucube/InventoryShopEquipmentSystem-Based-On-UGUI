using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BagItem_RSV : BasePanel,IItemInit<GridItem>
{
    /// <summary>
    /// 初始化道具格子信息  
    /// </summary>
    public void InitInfo(GridItem info)
    {
        //先读取道具表

        //根据表中数据，更新信息

        //更新图标
        //更新名字


        //更新道具数量
        GetControl<Text>("txtNum").text = info.num.ToString();
    }
}
