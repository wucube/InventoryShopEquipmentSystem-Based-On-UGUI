using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    //key 为Item的id
    private Dictionary<int, Item> itemInfos = new Dictionary<int, Item>();

    /// <summary>
    /// 玩家数据
    /// </summary>
    public Player playerInfo;

    //玩家信息存储路径
    private static string PlayerInfo_Url = Application.persistentDataPath + "/PlayerInfo.txt";

    /// <summary>
    /// 读取道具信息的配置文件
    /// </summary>
    public void Initialize()
    {
        //加载Resources文件夹下的Json文件
        string info = ResMgr.Instance().Load<TextAsset>("Json/ItemInfo").text;

        //Debug.Log(PlayerInfo_Url);

        //根据Json文件内容解析成对应的数据结构
        Items items = JsonUtility.FromJson<Items>(info);
        for(int i = 0; i < items.info.Count; i++)
        {
            itemInfos.Add(items.info[i].id, items.info[i]);
        }

        //初始化 角色信息

        if (File.Exists(PlayerInfo_Url))
        {
            //读出指定路径文件的字节数组
            byte[] bytes = File.ReadAllBytes(PlayerInfo_Url);
            //把字节数组转为字符串
            string json = Encoding.UTF8.GetString(bytes);
            //再将字符串转为玩家的数据结构
            playerInfo = JsonUtility.FromJson<Player>(json);
        }
        else //没有玩家数据时，创建出默认数据，并存储下来
        {
            playerInfo = new Player();
            SavePlayerInfo();
        }
    }
    /// <summary>
    /// 存储玩家道具信息
    /// </summary>
    public void SavePlayerInfo()
    {
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllBytes(PlayerInfo_Url, Encoding.UTF8.GetBytes(json));
    }

    /// <summary>
    /// 根据道具ID，得到对应的道具详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItemInfo(int id)
    {
        if (itemInfos.ContainsKey(id))
            return itemInfos[id];
        return null;
    }

}


/// <summary>
/// 玩家基础信息
/// </summary>
public class Player
{
    public string name;
    public int lev;
    public int money;
    public int gem;
    public int pro;
    public List<ItemInfo> items;
    public List<ItemInfo> equips;
    public List<ItemInfo> gems;

    public Player()
    {
        name = "唐老铁";
        lev = 1;
        money = 99999;
        gem = 0;
        pro = 99;
        items = new List<ItemInfo>() { new ItemInfo() { id = 3, num = 99 } };
        equips = new List<ItemInfo>() { new ItemInfo() { id = 1, num = 1 },new ItemInfo() { id = 2, num = 1 } };
        gems = new List<ItemInfo>();
    }
}

/// <summary>
/// 玩家拥有的道具基础信息
/// </summary>
public class ItemInfo
{
    //道具装备ID
    public int id;
    //数量
    public int num;
}

/// <summary>
/// 表示道具信息的数据结构
/// </summary>
public class Items
{
    public List<Item> info;
}

/// <summary>
/// 道具的基础数据结构
/// </summary>
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string icon;
    public int type;
    public int prices;
    public string tips;
}
