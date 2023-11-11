using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public Transform GameBoard;
    Vector3 GameBoardStartPosition;
    Vector3 HandStartPosition;
    float GameBoardStartYRotation;
    float HandStartYRotation;

    // Update is called once per frame
    void Update()
    {
        // Detect the grip button press on the right controller
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            // Grip button is pressed on the right controller
            //Debug.Log("Grip button pressed on the right controller.");
        }

        // Detect the grip button press on the left controller
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            // Grip button is pressed on the left controller
            //Debug.Log("Grip button pressed on the left controller.");

            GameBoardStartPosition = GameBoard.position;

            HandStartPosition = transform.position;

            GameBoardStartYRotation = GameBoard.rotation.eulerAngles.y;
            HandStartYRotation = transform.rotation.eulerAngles.y;
        }
        // Detect the grip button press on the left controller
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            // Grip button is pressed on the left controller
            //Debug.Log("Grip button pressed on the left controller.");

        }
        // Detect the grip button press on the left controller
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            // Grip button is pressed on the left controller
            //Debug.Log("Grip button pressed on the left controller.");

            Vector3 HandDeltaPosition = transform.position - HandStartPosition;
            float HandDeltaYRotation = transform.rotation.eulerAngles.y - HandStartYRotation;

            GameBoard.position = GameBoardStartPosition + HandDeltaPosition * 2f;
            //GameBoard.rotation = Quaternion.Euler(0, GameBoardStartYRotation + HandDeltaYRotation, 0);
        }
    }
}
