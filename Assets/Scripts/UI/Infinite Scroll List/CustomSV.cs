using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// 格子对象的基础方法，格子对象必须继承，用于实现初始化
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IItemBase<T>
{
    void InitInfo(T info);
}

/// <summary>
/// 自定义SV类，通常缓存池创建利用对象，节约对象
/// </summary>
/// <typeparam name="T">数据来源类</typeparam>
/// <typeparam name="K">格子类</typeparam>
public class CustomSV<T,K> where K:IItemBase<T>
{
    //格子父对象，根据其得到可视范围位置
    private RectTransform content;

    /// <summary>
    /// 可视范围的高
    /// </summary>
    private int viewPortH;

    /// <summary>
    /// 当前显示的格子对象
    /// </summary>
    private Dictionary<int, GameObject> nowShowItems = new Dictionary<int, GameObject>();

    /// <summary>
    /// 数据来源
    /// </summary>
    private List<T> items;

    //记录上一次显示的索引范围
    private int oldMinIndex = -1;
    private int oldMaxIndex = -1;

    //格子的间隔宽高
    private int itemW;
    private int itemH;

    //格子的列数
    private int col;

    //预设体资源的路径
    private string itemResName;

    /// <summary>
    /// 初始化格子资源路径
    /// </summary>
    /// <param name="name"></param>
    public void InitItemResName(string name)
    {
        itemResName = name;
    }

    /// <summary>
    /// 初始化Content父对象和可视范围的高
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="h"></param>
    public void InitContenAndSVH(RectTransform trans, int h)
    {
        this.content = trans;
        this.viewPortH = h;
    }

    /// <summary>
    /// 初始化数据来源，并将Content的高初始化
    /// </summary>
    /// <param name="items"></param>
    public void InitInfos(List<T> items)
    {
        this.items = items;
        content.sizeDelta = new Vector2(0, Mathf.CeilToInt(items.Count / 3f) * 190);
    }


    /// <summary>
    /// 初始化格子间隔大小和列数
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <param name="col"></param>
    public void InitItemSizeAndCol(int w, int h, int col)
    {
        itemW = w;
        itemH = h;
        this.col = col;
    }

    /// <summary>
    /// 更新格子显示的方法
    /// </summary>
    public void CheckShowOrHide()
    {
        //检测应该显示出来的格子，通过格子最小索引与最大索引得到格子显示范围
        // content的起始位置y / 格子大小 * 一行格子数 = 起始位置显示的格子索引
        int minIndex = (int)(content.anchoredPosition.y / itemH) * col;

        //content的起始位置y+可视范围的高 / 一行格子数 +（一行格子数-1）= 最后一个显示格子的位置索引
        //多显示出一层，避免滑动列表时出现明显的补格子的感觉

        int maxIndex = Mathf.CeilToInt((content.anchoredPosition.y + viewPortH) / itemH) * col + col;

        //int maxIndex = Mathf.Round



        //最小索引值为0
        if (minIndex < 0)
            minIndex = 0;

        //最大索引不能超出道具最大数量
        if (maxIndex >= items.Count)
            maxIndex = items.Count - 1;

        if (minIndex != oldMinIndex || maxIndex != oldMaxIndex)
        {
            //根据上次索引与这次新索引，判断该移除的对象
            //删除上一节的溢出内容
            for (int i = oldMinIndex; i < minIndex; i++)
            {
                if (nowShowItems.ContainsKey(i))
                {
                    if (nowShowItems[i] != null)
                        PoolMgr.Instance().PushObj(itemResName, nowShowItems[i]);

                    nowShowItems.Remove(i);
                }
            }
            //删除下一节的溢出
            for (int i = maxIndex + 1; i <= oldMaxIndex; i++)
            {
                if (nowShowItems.ContainsKey(i))
                {
                    if (nowShowItems[i] != null)
                        PoolMgr.Instance().PushObj(itemResName, nowShowItems[i]);

                    nowShowItems.Remove(i);
                }
            }
        }

        //记录当前索引
        oldMinIndex = minIndex;
        oldMaxIndex = maxIndex;

        for (int i = minIndex; i < maxIndex; i++)
        {
            if (nowShowItems.ContainsKey(i))
                continue;

            else
            {
                int index = i;

                //先添加索引与空对象
                nowShowItems.Add(index, null);

                PoolMgr.Instance().GetObj(itemResName, (obj) =>
                {
                    //格创建后要做的事情

                    //设置格子的父对象
                    obj.transform.SetParent(content);
                    //重置相对缩放大小
                    obj.transform.localScale = Vector3.one;
                    //重置位置，索引 % 一行格子数 = 列数；索引/格子数 = 行数
                    obj.transform.localPosition = new Vector3((index % col) * itemW, -index / col * itemH, 0);
                    //更新格子信息
                    obj.GetComponent<K>().InitInfo(items[index]);


                    //判断有没有该索引。 异步加载资源，可能索引移除后资源才创建出来
                    if (nowShowItems.ContainsKey(index))
                        nowShowItems[index] = obj;
                    else
                        PoolMgr.Instance().PushObj(itemResName, obj);
                });
            }

        }
    }

}
