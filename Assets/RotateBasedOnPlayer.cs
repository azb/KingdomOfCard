using Photon.Pun;
using UnityEngine;

public class RotateBasedOnPlayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public override void OnJoinedRoom()
    {
        if (enabled)
        {
            Debug.Log("Joined Room Actor Number = " + PhotonNetwork.LocalPlayer.ActorNumber);
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                Debug.Log("Is master so not rotating", gameObject);
            }
            else
            {
                Debug.Log("Is not master so rotating", gameObject);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

}
