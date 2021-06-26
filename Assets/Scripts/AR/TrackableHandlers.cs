using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableHandlers : DefaultTrackableEventHandler
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        gameManager.SubscribeTarget(gameObject.GetComponent<ImageTarget>());
 
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        gameManager.UnsubscribeTarget(gameObject.GetComponent<ImageTarget>());
    }
}
