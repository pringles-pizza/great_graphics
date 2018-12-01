using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetEvent : MonoBehaviour,ITrackableEventHandler  {

    private TrackableBehaviour mTrackableBehaviour;

    public bool isDetected = false;
    public Transform targetTransform; //오브젝트가 올라갈 곳
    public GameObject modelObject; //오브젝트

    void Start()
    {
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
        modelObject.transform.parent = targetTransform;

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
            Detected();
        }
        else
        {
            isDetected = false;
            Lost();
        }
    }

    void Detected()
    {
        print("Detected");
        modelObject.GetComponent<Renderer>().enabled = true;


    }
    void Lost()
    {
        print("UnDetected");

    }

}
