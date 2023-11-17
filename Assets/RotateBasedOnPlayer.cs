using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBasedOnPlayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Actor Number = " + PhotonNetwork.LocalPlayer.ActorNumber);
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            Debug.Log("Is master so not rotating");
        }
        else
        {
            Debug.Log("Is not master so rotating");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
