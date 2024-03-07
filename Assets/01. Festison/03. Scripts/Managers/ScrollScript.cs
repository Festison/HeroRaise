using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollScript : ScrollRect
{
    bool forParent;
    ScrollManager scrollview;
    ScrollRect parentScrollRect;

    protected override void Start()
    {
        scrollview = GameObject.FindWithTag("ParentUI").GetComponent<ScrollManager>();
        parentScrollRect = GameObject.FindWithTag("ParentUI").GetComponent<ScrollRect>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� �����ϴ� ���� �����̵��� ũ�� �θ� �巡�� ������ ��, �����̵��� ũ�� �ڽ��� �巡�� ������ ��
        forParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if (forParent)
        {
            scrollview.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        }
        else base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            scrollview.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        }
        else base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            scrollview.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        }
        else base.OnEndDrag(eventData);
    }

}