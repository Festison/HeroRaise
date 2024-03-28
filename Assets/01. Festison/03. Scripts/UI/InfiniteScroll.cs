using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentTransfrom;

    Vector2 oldVelocity;
    bool isUpdated;

    public void Start()
    {
        isUpdated = false;
        oldVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (isUpdated)
        {
            isUpdated = false;
            scrollRect.velocity = oldVelocity;
        }

        if (contentTransfrom.localPosition.y > (contentTransfrom.rect.height-700))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            contentTransfrom.localPosition = new Vector3(0, 0, 0);
            isUpdated = true;
        }
    }
}
