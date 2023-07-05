using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具信息
/// </summary>
public class GridItem
{
    public int id;
    public int num;
}


/// <summary>
/// 管理公共数据和公共方法
/// </summary>
public class BagMgr : BaseManager<BagMgr>
{
    /// <summary>
    /// 装载格子的容器
    /// </summary>
    public List<GridItem> items = new List<GridItem>();

    /// <summary>
    /// 模拟获取数据的方法，实际开发中数据都从服务器或本地文件中读取
    /// </summary>
    public void InitItemInfo()
    {
        for(int i = 0; i < 10000; i++)
        {
            GridItem item = new GridItem
            {
                id = i,
                num = i
            };

            items.Add(item);
        }
    }
}
