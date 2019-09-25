using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        public Symbol mySymbol = Symbol.X;

        [Header("AI")]
        public bool isAI = false;
        public Difficulty difficulty = Difficulty.Easy;

        private GameStateManager gameStateManager = null;
        private GridManager gridManager = null;

        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GameStateManager.OnSymbolTurnStart += OnSymbolTurnStart;
        }

        private void OnDisable()
        {
            GameStateManager.OnSymbolTurnStart -= OnSymbolTurnStart;
        }
        #endregion
        private void Awake()
        {
            gameStateManager = FindObjectOfType<GameStateManager>();
            gridManager = FindObjectOfType<GridManager>();
        }

        #region Events
        private void OnSymbolTurnStart(Symbol currentSymbolTurn)
        {
            if (gameStateManager.CheckIfIsOnlineGame())
            {
                if (currentSymbolTurn == mySymbol)
                {
                    Debug.Log(gameObject.name + " is my turn");
                    gridManager.canvasGroup.interactable = true;
                }
                else
                {
                    Debug.Log(gameObject.name + " is enemy turn");
                    gridManager.canvasGroup.interactable = false;
                }
            }

            else if (currentSymbolTurn == mySymbol && isAI)
            {
                Debug.Log(gameObject.name + " is AI turn");
                gridManager.canvasGroup.interactable = false;

                switch (difficulty)
                {
                    case Difficulty.Easy:
                        Vector2Int[] emptyCells = TicTacToeUtility.GetEmptyPositions(gridManager.grid);
                        if (emptyCells.Length > 0) gridManager.GetGridButtonAtPosition(emptyCells[UnityEngine.Random.Range(0, emptyCells.Length)]).onClick.Invoke();
                        break;
                    case Difficulty.Normal:
                        gridManager.GetGridButtonAtPosition(TicTacToeUtility.MiniMax(gridManager.grid, 2, true).position).onClick.Invoke();
                        break;
                    case Difficulty.Hard:
                        gridManager.GetGridButtonAtPosition(TicTacToeUtility.MiniMax(gridManager.grid, 9, true).position).onClick.Invoke();
                        break;
                    default:
                        break;
                }
            }
            else if (currentSymbolTurn == mySymbol)
            {
                Debug.Log(gameObject.name + " is Player turn");
                gridManager.canvasGroup.interactable = true;
            }               
        }
        #endregion

    } // class
} // namespace
