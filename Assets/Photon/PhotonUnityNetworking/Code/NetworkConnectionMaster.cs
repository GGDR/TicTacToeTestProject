using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkConnectionMaster : MonoBehaviourPunCallbacks
{
    public Button buttonConnectToMaster;
    public Button buttonConnectToRoom;

    public bool tryToConnectToMaster;
    public bool tryToConnectToRoom;

    private void Start()
    {
        tryToConnectToMaster = false;
        tryToConnectToRoom = false;
    }


    private void Update()
    {
        buttonConnectToMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !tryToConnectToMaster);
        buttonConnectToRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !tryToConnectToMaster && !tryToConnectToRoom);
    }

    public void OnCickConnectToMaster()
    {
        PhotonNetwork.NickName = "SomeRandomNickName";
        PhotonNetwork.GameVersion = "V1.0.0";

        tryToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        tryToConnectToMaster = false;
        tryToConnectToRoom = false;

        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        tryToConnectToMaster = false;

        Debug.Log("Connected to Master");
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        tryToConnectToRoom = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        tryToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        SceneManager.LoadScene("TestScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        Debug.Log(message);
        tryToConnectToRoom = false;
    }
}
