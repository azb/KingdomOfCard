// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectAndJoinRandom.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Utilities, 
// </copyright>
// <summary>
//  Simple component to call ConnectUsingSettings and to get into a PUN room easily.
// </summary>
// <remarks>
//  A custom inspector provides a button to connect in PlayMode, should AutoConnect be false.
//  </remarks>                                                                                               
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

using UnityEngine;

//using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace Photon.Pun.UtilityScripts
{
    /// <summary>Simple component to call ConnectUsingSettings and to get into a PUN room easily.</summary>
    /// <remarks>A custom inspector provides a button to connect in PlayMode, should AutoConnect be false.</remarks>
    public class ConnectAndJoinRandom : MonoBehaviourPunCallbacks
    {
        public Text DebugLabel;

        /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
        public bool AutoConnect = true;

        /// <summary>Used as PhotonNetwork.GameVersion.</summary>
        public byte Version = 1;

        /// <summary>Max number of players allowed in room. Once full, a new room will be created by the next connection attemping to join.</summary>
        [Tooltip("The max number of players allowed in room. Once full, a new room will be created by the next connection attemping to join.")]
        public byte MaxPlayers = 4;

        public int playerTTL = -1;

        public void Start()
        {
            if (this.AutoConnect)
            {
                this.ConnectNow();
                Invoke("CheckConnection", 15f);
            }
        }

        void CheckConnection()
        {
            //Check if the photon network is still connected and joined in a room
            //if not, attempt to reconnect and join a room
            if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            {
                this.ConnectNow();
            }

            Invoke("CheckConnection", 10f);
        }


        public void ConnectNow()
        {
            if (PhotonNetwork.ShowDebugs)
            Debug.Log("ConnectAndJoinRandom.ConnectNow() will now call: PhotonNetwork.ConnectUsingSettings().");

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;
        }


        // below, we implement some callbacks of the Photon Realtime API.
        // Being a MonoBehaviourPunCallbacks means, we can override the few methods which are needed here.


        public override void OnConnectedToMaster()
        {
            DebugLabel.text = "Connected";
            if (PhotonNetwork.ShowDebugs)
                Debug.Log("OnConnectedToMaster() was called by PUN. This client is now connected to Master Server in region [" + PhotonNetwork.CloudRegion +
                "] and can join a room. Calling: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedLobby()
        {
            DebugLabel.text = "Joined Lobby";
            if (PhotonNetwork.ShowDebugs)
                Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion + "]. This script now calls: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            DebugLabel.text = "Join Random Failed";
            if (PhotonNetwork.ShowDebugs)
                Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available in region [" + PhotonNetwork.CloudRegion + "], so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = this.MaxPlayers };
            if (playerTTL >= 0)
                roomOptions.PlayerTtl = playerTTL;

            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        // the following methods are implemented to give you some context. re-implement them as needed.
        public override void OnDisconnected(DisconnectCause cause)
        {
            if (PhotonNetwork.ShowDebugs)
                Debug.Log("OnDisconnected(" + cause + ")");
        }

        public override void OnJoinedRoom()
        {
            DebugLabel.text = "Joined Room";
            if (PhotonNetwork.ShowDebugs)
                Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room in region [" + PhotonNetwork.CloudRegion + "]. Game is now running.");
        }
        public override void OnPlayerEnteredRoom(Player player)
        {
            DebugLabel.text = "A player entered";
        }
    }


    //#if UNITY_EDITOR
    //[CanEditMultipleObjects]
    //[CustomEditor(typeof(ConnectAndJoinRandom), true)]
    //public class ConnectAndJoinRandomInspector : Editor
    //{
    //    void OnEnable() { EditorApplication.update += Update; }
    //    void OnDisable() { EditorApplication.update -= Update; }

    //    bool isConnectedCache = false;

    //    void Update()
    //    {
    //        if (this.isConnectedCache != PhotonNetwork.IsConnected)
    //        {
    //            this.Repaint();
    //        }
    //    }

    //    public override void OnInspectorGUI()
    //    {
    //        this.isConnectedCache = !PhotonNetwork.IsConnected;


    //        this.DrawDefaultInspector(); // Draw the normal inspector

    //        if (Application.isPlaying && !PhotonNetwork.IsConnected)
    //        {
    //            if (GUILayout.Button("Connect"))
    //            {
    //                ((ConnectAndJoinRandom)this.target).ConnectNow();
    //            }
    //        }
    //    }
    //}
    //#endif
}
