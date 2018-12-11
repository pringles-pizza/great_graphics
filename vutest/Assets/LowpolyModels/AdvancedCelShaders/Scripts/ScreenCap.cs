using UnityEngine;

public class ScreenCap : MonoBehaviour {

    private int i;

    private void Update () {

        if (Input.GetButtonDown ("Fire1")) {

            ScreenCapture.CaptureScreenshot ("pic_" + i + ".png", 4);
            i++;

        }

    }

}
