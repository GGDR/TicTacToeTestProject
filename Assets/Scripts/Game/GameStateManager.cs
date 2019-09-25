using System;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    public class GameStateManager : MonoBehaviour
    {
        public static Action OnRematch;
        public static Action OnGameReset;
        public static Action<WinResult> OnGameOver;
        public static Action<Symbol> OnSymbolTurnStart;

        [SerializeField] private bool isOnlineGame = false;

        public Symbol currentSymbolTurn { get; private set; } = Symbol.X;

        private bool isGameOver = false;

        // Score
        private int p1Score = 0;
        private int p2Score = 0;
        private int drawScore = 0;

        private PhotonView photonView = null;

        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GridManager.OnGridCellFilled += OnGridCellFilled;
        }

        private void OnDisable()
        {
            GridManager.OnGridCellFilled -= OnGridCellFilled;
        }
        #endregion

        private void Start()
        {
            if (isOnlineGame)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PlayerController tempController = new GameObject("PlayerController").AddComponent<PlayerController>();
                    tempController.mySymbol = Symbol.X;
                }
                else
                {
                    PlayerController tempController = new GameObject("PlayerController").AddComponent<PlayerController>();
                    tempController.mySymbol = Symbol.O;
                }
            }
            else
            {
                
                PlayerController tempController = new GameObject("AIController").AddComponent<PlayerController>();
                tempController.mySymbol = Symbol.O;
                tempController.isAI = true;
                switch (TicTacToeUtility.GetDifficulty())
                {
                    case Difficulty.Easy:
                        Debug.Log("Is easy");
                        tempController.difficulty = Difficulty.Easy;
                        break;
                    case Difficulty.Normal:
                        Debug.Log("Is normal");
                        tempController.difficulty = Difficulty.Normal;
                        break;
                    case Difficulty.Hard:
                        Debug.Log("Is hard");
                        tempController.difficulty = Difficulty.Hard;
                        break;
                    default:
                        break;
                }
                

                tempController = new GameObject("PlayerController").AddComponent<PlayerController>();
                tempController.mySymbol = Symbol.X;
                
            }

            OnGameReset?.Invoke();
            OnRematch?.Invoke();
            OnSymbolTurnStart?.Invoke(Symbol.X);

            photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Rematch();
            }
        }

        #region Events
        private void OnGridCellFilled(int[,] grid, int gridSize)
        {
            WinResult result = TicTacToeUtility.CheckWinner(grid, gridSize);
            switch (result.score)
            {
                case 1:
                    Debug.Log("X - win");
                    p1Score++;
                    isGameOver = true;
                    OnGameOver?.Invoke(result);
                    break;
                case -1:
                    Debug.Log("O - win");
                    p2Score++;
                    isGameOver = true;
                    OnGameOver?.Invoke(result);
                    break;
                case 0:
                    if (!TicTacToeUtility.CheckForEmptyCells(grid))
                    {
                        Debug.Log("Draw");
                        drawScore++;
                        isGameOver = true;
                        OnGameOver?.Invoke(result);
                    }
                    break;
                default:
                    Debug.Log("Incorrect result");
                    break;
            }
            if (!isGameOver)
            {
                SwitchSymbolTurn();
                OnSymbolTurnStart?.Invoke(currentSymbolTurn);
            }
            else
            {
                if (result.score == 0)
                    SwitchSymbolTurn();
                DOVirtual.DelayedCall(2f, () => Rematch());
            }
        }
        #endregion

        private void SwitchSymbolTurn()
        {
            if (currentSymbolTurn == Symbol.X) currentSymbolTurn = Symbol.O;
            else currentSymbolTurn = Symbol.X;
        }

        private void ResetScore()
        {
            p1Score = 0;
            p2Score = 0;
            drawScore = 0;
        }

        public void Rematch()
        {
            isGameOver = false;
            OnRematch?.Invoke();
            OnSymbolTurnStart?.Invoke(currentSymbolTurn);
        }

        public bool CheckIfIsOnlineGame()
        {
            return isOnlineGame;
        }

    } // class
} // namespace
