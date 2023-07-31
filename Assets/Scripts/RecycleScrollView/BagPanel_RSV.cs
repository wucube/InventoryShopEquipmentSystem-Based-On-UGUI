using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel_RSV : BasePanel
{
    private RectTransform content;

    [SerializeField] private int scrollViewHeigh;

    private RecycleScrollView<GridItem,BagItem_RSV> sv;

    protected override void Awake()
    {
        base.Awake();

        content = (RectTransform)transform.FindChildByName("Content");
    }

    void Start()
    {
        sv = new RecycleScrollView<GridItem, BagItem_RSV>();
        sv.InitItemResName("UI/BagItem_RSV");
        sv.InitItemSizeAndCol(200, 250, 3);
        sv.InitContenAndSVH(content, scrollViewHeigh);
        sv.InitInfos(BagMgr.Instance.items);

    }
    void Update()
    {
        sv.CheckShowOrHide();
    }

}
