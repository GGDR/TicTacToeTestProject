using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Asteroids
{
    public class LobbyTopPanel : MonoBehaviour
    {
        [Header("UI References")]
        public TextMeshProUGUI ConnectionStatusText;

        #region UNITY

        public void Update()
        {
            ConnectionStatusText.text = PhotonNetwork.NetworkClientState.ToString();
        }

        #endregion
    }
}