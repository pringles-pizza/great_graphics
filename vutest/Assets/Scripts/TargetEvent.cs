using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetEvent : MonoBehaviour,ITrackableEventHandler  {

    private TrackableBehaviour mTrackableBehaviour;

    public bool isDetected = false;
    public Transform targetTransform;

    void Start()
    {
        targetTransform = transform;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            isDetected = true;
            print("Detected");
        }
        else
        {
            isDetected = false;
            print("UnDetected");
        }
    }
}
