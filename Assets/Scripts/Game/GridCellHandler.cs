using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class GridCellHandler : MonoBehaviour
    {
        public static Action<int, int, int> OnCellClick; // row, column, symbolIndex

        [SerializeField] private int rowIndex = 0;
        [SerializeField] private int columnIndex = 0;

        [SerializeField] private TextMeshProUGUI txt = null;

        private GameStateManager gameStateManager = null;
        private GridManager gridManager = null;

        private Button cellButton = null;

        private PhotonView photonView = null;
        
        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GameStateManager.OnRematch += OnRematch;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameReset -= OnRematch;
        }
        #endregion

        private void Awake()
        {
            cellButton = GetComponent<Button>();
            cellButton.onClick.AddListener(CellOnClickListener);
        }

        private void Start()
        {
            gameStateManager = FindObjectOfType<GameStateManager>();
            gridManager = FindObjectOfType<GridManager>();
            gridManager.AddButtonToGrid(new Vector2Int(rowIndex, columnIndex), cellButton);

            photonView = GetComponent<PhotonView>();
        }

        #region Events
        private void OnRematch()
        {
            cellButton.interactable = true;
            txt.text = "";
        }
        #endregion

        public void CellOnClickListener()
        {
            if (gameStateManager.CheckIfIsOnlineGame())
            {
                photonView.RPC("HandleClick", RpcTarget.AllViaServer);
            }
            else
            {
                HandleClick();
            }
        }

        [PunRPC]
        public void HandleClick()
        {
            cellButton.interactable = false;
            txt.text = gameStateManager.currentSymbolTurn == Symbol.X ? "X" : "O";
            OnCellClick?.Invoke(rowIndex, columnIndex, gameStateManager.currentSymbolTurn == Symbol.X ? 1 : -1);
        }

    } // class
} // namespace
