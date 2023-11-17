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
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Is not master so rotating");
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            Debug.Log("Is master so not rotating");
        }
    }

}
