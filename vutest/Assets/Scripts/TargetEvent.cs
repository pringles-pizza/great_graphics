using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetEvent : MonoBehaviour,ITrackableEventHandler  {

    private TrackableBehaviour mTrackableBehaviour;

    public bool isDetected = false;
    public Transform targetTransform; //오브젝트가 올라갈 곳
    public GameObject modelObject; //오브젝트 인스턴스 (끄고 켜짐)
    public GameObject modelObjectCreated; //생성될 오브젝트
    GameObject modelInstCreated; //생성된 오브젝트 인스턴스


    void Start()
    {
        //오브젝트가 올라갈 트랜스폼 설정
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
        if (modelObject != null)
        {
            modelObject.transform.parent = targetTransform;
        }
        //뷰포리아 초기화
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
        
        //모델의 감지 코드 호출
        if (modelObject != null)
        {
            ModelControl modelInstControl = modelObject.GetComponent<ModelControl>();
            modelInstControl.ModelDetected();
        }

        //모델 생성, 모델의 감지 코드 호출
        if (modelObjectCreated != null)
        {
            modelInstCreated = Instantiate(modelObjectCreated, targetTransform);
            ModelControl modelInstCreatedControl = modelInstCreated.GetComponent<ModelControl>();
            modelInstCreatedControl.ModelDetected();
        }
    }
    void Lost()
    {
        print("UnDetected");
        
        //모델 사라짐 코드 호출
        if (modelObject != null)
        {
            
            ModelControl modelInstControl = modelObject.GetComponent<ModelControl>();
            modelInstControl.ModelLost();
        }

        //모델(생성된) 사라짐 코드 호출
        if (modelObjectCreated != null)
        {
            
            ModelControl modelInstCreatedControl = modelInstCreated.GetComponent<ModelControl>();
            modelInstCreatedControl.ModelLost();
        }
    }

}
