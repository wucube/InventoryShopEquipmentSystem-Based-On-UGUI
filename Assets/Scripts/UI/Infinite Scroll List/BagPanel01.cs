using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel01 : BasePanel
{
    public RectTransform content;

    private CustomSV<GridItem,BagItem> sv;


    void Start()
    {
        sv = new CustomSV<GridItem, BagItem>();
        sv.InitItemResName("UI/BagItem");
        sv.InitItemSizeAndCol(200, 250, 3);
        sv.InitContenAndSVH(content,910);
        sv.InitInfos(BagMgr.Instance().items);

    }
    void Update()
    {
        sv.CheckShowOrHide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(content.anchoredPosition.y);
        }

    }

    
}
