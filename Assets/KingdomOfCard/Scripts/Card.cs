using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : OVRGrabbable
{
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Debug.Log("GrabEnd Event Triggered");
        base.GrabEnd(linearVelocity, angularVelocity);
        Invoke("AttachToGameBoard",.1f);
    }

    void AttachToGameBoard()
    {
        GameObject gameBoard = GameObject.FindWithTag("GameBoard");
        Transform gameBoardT = gameBoard.transform;
        Debug.Log("gameBoard = " + gameBoard);
        Debug.Log("gameBoardT = " + gameBoardT);
        transform.parent = gameBoardT;
    }
}
