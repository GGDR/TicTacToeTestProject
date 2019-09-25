using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class GameResultWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt = null;

        private CanvasGroup canvasGroup = null;
        
        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GameStateManager.OnGameOver += OnGameOver;
            GameStateManager.OnRematch += OnRematch;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameOver -= OnGameOver;
            GameStateManager.OnRematch -= OnRematch;
        }
        #endregion

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            HideResultWindow();
        }

        #region Events
        private void OnGameOver(WinResult result)
        {
            switch (result.score)
            {
                case 1:
                    txt.text = "X - Win";
                    break;
                case -1:
                    txt.text = "O - Win";
                    break;
                case 0:
                    txt.text = "Draw";
                    break;
                default:
                    Debug.LogError("Incorrect result");
                    break;
            }
            ShowResultWindow();
        }

        private void OnRematch()
        {
            HideResultWindow();
        }
        #endregion

        private void ShowResultWindow()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private void HideResultWindow()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

    } // class
} // namespace