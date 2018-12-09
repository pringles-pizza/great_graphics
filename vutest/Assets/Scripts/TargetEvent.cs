using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetEvent : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour mTrackableBehaviour;

    [Header("변경 X")]
    public bool isDetected = false;
    public bool isBaseDetected = false; //베이스와 자신 모두 디텍트될 때 모델 코드 실행

    [Header("베이스 마커")]
    public GameObject BaseMarker; // 베이스 마커
    
    [Header("모델 오브젝트 인스턴스 (내 자식 오브젝트)")]
    public GameObject modelObject; //오브젝트 인스턴스 (끄고 켜짐)
    [Header("내가 생성할 모델 오브젝트")]
    public GameObject modelObjectCreated; //생성될 오브젝트
    [Header("생성된 오브젝트가 올라갈 곳")]
    public Transform targetTransform; //오브젝트가 올라갈 곳

    GameObject modelInstCreated; //생성된 오브젝트 인스턴스


    void Start()
    {
        //오브젝트가 올라갈 트랜스폼 설정
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
        /*
        if (modelObject != null)
        {
            modelObject.transform.parent = targetTransform;
        }*/
        /*
        foreach (ModelControl modelControl in GetComponentsInChildren<ModelControl>())
        {
            modelControl.gameObject.transform.parent = targetTransform;
        }*/
        //뷰포리아 초기화
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    void Update()
    {
        if (BaseMarker != null) //베이스 마커가 있을 때
        {
            if ((BaseMarker.GetComponent<TargetEvent>().isDetected)
            && !isBaseDetected)
            {
                isBaseDetected = true;
                if (isDetected) //나는 이미 디텍트되었는데 베이스가 안보이다가 지금 보일 때
                {
                    CallDetection();
                }
            }

            if (!(BaseMarker.GetComponent<TargetEvent>().isDetected)
                && isBaseDetected)
            {
                isBaseDetected = false;
                if (isDetected) //나는 이미 디텍트되었는데 베이스가 보이다가 지금 안보일 때
                {
                    CallUnDetection();
                }
            }
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
        //print("Detected");

        if (BaseMarker != null)
        {
            if (isBaseDetected) //베이스가 원래 보이다가 나도 지금 보일 때
            { CallDetection(); }
        }
        else { CallDetection(); }
        
    }

    void Lost()
    {
        //print("UnDetected");

        if (BaseMarker != null)
        {
            if (isBaseDetected) //베이스가 원래 보이다가 나는 지금 사라질 때
            { CallUnDetection(); }
        }
        else { CallUnDetection(); }
    }



    void CallDetection()
    {
        //모델의 감지 코드 호출
        /*
        if (modelObject != null)
        {
            ModelControl modelInstControl = modelObject.GetComponent<ModelControl>();
            modelInstControl.ModelDetected();
        }*/

        foreach (ModelControl modelControl in GetComponentsInChildren<ModelControl>())
        {
            modelControl.ModelDetected();
        }

        //모델 생성, 모델의 감지 코드 호출
        if (modelObjectCreated != null)
        {
            modelInstCreated = Instantiate(modelObjectCreated, targetTransform);
            ModelControl modelInstCreatedControl = modelInstCreated.GetComponent<ModelControl>();
            modelInstCreatedControl.ModelDetected();
        }
    }

    void CallUnDetection()
    {
        //모델 사라짐 코드 호출
        /*
        if (modelObject != null)
        {

            ModelControl modelInstControl = modelObject.GetComponent<ModelControl>();
            modelInstControl.ModelLost();
        }*/
        foreach (ModelControl modelControl in GetComponentsInChildren<ModelControl>())
        {
            modelControl.ModelLost();
        }

        //모델(생성된) 사라짐 코드 호출
        if (modelObjectCreated != null)
        {

            ModelControl modelInstCreatedControl = modelInstCreated.GetComponent<ModelControl>();
            modelInstCreatedControl.ModelLost();
        }
    }

}
