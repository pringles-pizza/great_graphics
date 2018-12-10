using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour {

    private Animator animator;
    private Animation animation;

    [Header("수정X")]
    public TargetEvent target;

    [Header("오브젝트가 붙을 트랜스폼")]
    public Transform targetTransform; //오브젝트가 올라갈 곳

    [Header("마커 발견할 때 오브젝트 생성")]
    public GameObject createdObject;
    [Header("마커 사라질 때 삭제되는가")]
    public bool DeleteOnLost;
    [Header("마커 사라질 때 렌더러가 꺼지는가")]
    public bool HideOnLost;
    [Header("삭제되거나 렌더러가 꺼지기까지의 시간")]
    public float deletionTimer;

    [Header("생성될 때 재생하기")]
    public bool playOnCreate = false;


    void Start() {

        if (targetTransform != null)
        {
            transform.parent = targetTransform;
        }
        

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        
        if (GetComponent<Animation>() != null)
        {
            animation = GetComponent<Animation>();
        }

        if (playOnCreate)
        {
            CreateAction();
        }
    }

    void Update() {
        /*
        transform.rotation =Quaternion.Euler
            (new Vector3(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            Mathf.Clamp(transform.rotation.eulerAngles.z, 50, 80)));
            */
    }


    public void ModelDetected()
    {
        //자신의 ModelControl의 생성 스크립트 실행
        CreateAction();
        /*
        foreach (ModelControl childControl in transform.GetComponentsInChildren<ModelControl>())
        {
            childControl.CreateAction();
        }*/
    }

    public void ModelLost()
    {
        //자신의 ModelControl의 삭제 스크립트 실행
        DeleteAction();
        /*
        foreach (ModelControl childControl in transform.GetComponentsInChildren<ModelControl>())
        {
            childControl.DeleteAction();
        }
        */
    }

    public void CreateAction() //감지될 때 실행되는 모든 자식 모델의 코드
    {
        print(gameObject + " child detected");

        // 생성할 오브젝트 있을 경우 생성
        if (createdObject != null)
        {
            Instantiate(createdObject, transform);
        }

        // 애니메이션 실행
        if (animator != null)
        {
            animator.SetBool("isCreating", true);
            animator.SetBool("isDeleting", false);
        }
        if (animation != null)
        {
            
        }

        // 렌더러 숨김 실행 중지
        CancelInvoke("Hide");
        // 렌더러 보임
        Show();
    }

    public void DeleteAction()  //사라질 때 실행되는 모든 자식 모델의 코드
    {
        print(gameObject + " child lost");
        
        // 애니메이션 실행
        if (animator != null)
        {
            animator.SetBool("isCreating", false);
            animator.SetBool("isDeleting", true);
        }

        // 삭제해야 할 경우 삭제
        if (DeleteOnLost)
        {
            Destroy(gameObject, deletionTimer);
        }
        else if (HideOnLost) // 아니면 렌더러 숨김
        {
            Invoke("Hide", deletionTimer);
        }
    }

    private void Hide()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().enabled = false;
        }
        /*
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = false;
            print("hide____________");
        }*/
    }

    private void Show()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().enabled = true;
        }
        /*
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
        }
        */
    }

}
