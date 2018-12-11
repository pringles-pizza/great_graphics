using UnityEngine;

public class CameraController : MonoBehaviour {

    [Range(1f, 10f)] public float RotationSpeed = 10f;
    [Range(1f, 20f)] public float CamDistance = 6.5f;
    public float[] CamYPos;
    public float YTarget = 0;

    private GameObject cam;
    private GameObject target;
    private int Index = 0;
    private Vector3 Ref;

    private void Start () {

        cam = Camera.main.gameObject;
        target = new GameObject ("Target");
        cam.transform.SetParent (target.transform);

    }

    private void Update () {

        if (Index >= CamYPos.Length) {

            Index = 0;

        }

        target.transform.Rotate (Vector3.up * Time.deltaTime * RotationSpeed, Space.World);
        cam.transform.LookAt (Vector3.up * YTarget);
        cam.transform.localPosition = Vector3.SmoothDamp (cam.transform.localPosition, new Vector3 (0, CamYPos[Index], CamDistance), ref Ref, 0.3f);

        if (Input.GetKeyDown (KeyCode.Mouse1)) {

            Index++;

        }

    }

}
