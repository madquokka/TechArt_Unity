using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TouchMgr : MonoBehaviour
{

    public GameObject drone;

    public Camera arCamera;

    int droneCnt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if(droneCnt < 1 && touch.phase == TouchPhase.Began)
        {
            TrackableHit hit;
            TrackableHitFlags flag = TrackableHitFlags.FeaturePointWithSurfaceNormal
                                   | TrackableHitFlags.PlaneWithinPolygon;

            if(Frame.Raycast(touch.position.x, touch.position.y, flag, out hit))
            {
                Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
                Instantiate(drone, hit.Pose.position, hit.Pose.rotation, anchor.transform);
                droneCnt++;
            }
        }
    }
}
