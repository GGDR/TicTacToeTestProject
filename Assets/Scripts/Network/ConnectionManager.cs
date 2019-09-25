using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject otherPlayerLeftPanel = null;

    public override void OnPlayerLeftRoom(Player other)
    {
        otherPlayerLeftPanel.GetComponent<CanvasGroupController>().ShowCanvasGroup();
        PhotonNetwork.Disconnect();
        DOVirtual.DelayedCall(2f, () => SceneManager.LoadScene("MainMenu"));
    }
}
