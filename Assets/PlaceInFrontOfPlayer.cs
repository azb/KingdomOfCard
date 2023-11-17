using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInFrontOfPlayer : MonoBehaviour
{
    public Transform CenterEyeCamera;
    public CameraRigChooser cameraRigChooser;
    public bool BoardHasBeenPlaced;
    public GameObject AlignmentCanvas;

    // Update is called once per frame
    void Update()
    {
        if (!BoardHasBeenPlaced && cameraRigChooser.mode == CameraRigChooser.Mode.VR)
        {
            transform.position = CenterEyeCamera.position + CenterEyeCamera.forward;
            transform.rotation = Quaternion.Euler(
                new Vector3(
                    0,
                    CenterEyeCamera.rotation.eulerAngles.y,
                    0
                )
            );
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            BoardHasBeenPlaced = !BoardHasBeenPlaced;
            AlignmentCanvas.SetActive(!BoardHasBeenPlaced);
        }
    }
}
