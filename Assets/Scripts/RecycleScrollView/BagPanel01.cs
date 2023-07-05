using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel01 : BasePanel
{
    public RectTransform content;

    public int scrollViewHeigh;

    private RecycleScrollView<GridItem,BagItem> sv;


    void Start()
    {
        sv = new RecycleScrollView<GridItem, BagItem>();
        sv.InitItemResName("UI/BagItem");
        sv.InitItemSizeAndCol(200, 250, 3);
        sv.InitContenAndSVH(content, scrollViewHeigh);
        sv.InitInfos(BagMgr.Instance().items);

    }
    void Update()
    {
        sv.CheckShowOrHide();
    }

}
