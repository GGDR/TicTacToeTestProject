using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI p1ScoreTxt = null;
        [SerializeField] private TextMeshProUGUI p2ScoreTxt = null;
        [SerializeField] private TextMeshProUGUI drawScoreTxt = null;

        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GameStateManager.OnGameOver += OnGameOver;
            GameStateManager.OnGameReset += OnGameReset;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameOver -= OnGameOver;
            GameStateManager.OnGameReset -= OnGameReset;
        }
        #endregion

        #region Events
        private void OnGameOver(WinResult result)
        {
            switch (result.score)
            {
                case 1:
                    p1ScoreTxt.text = (Convert.ToInt16(p1ScoreTxt.text) + 1).ToString();
                    break;
                case -1:
                    p2ScoreTxt.text = (Convert.ToInt16(p2ScoreTxt.text) + 1).ToString();
                    break;
                case 0:
                    drawScoreTxt.text = (Convert.ToInt16(drawScoreTxt.text) + 1).ToString();
                    break;
                default:
                    break;
            }
        }

        private void OnGameReset()
        {
            p1ScoreTxt.text = "0";
            p2ScoreTxt.text = "0";
            drawScoreTxt.text = "0";
        }
        #endregion
    }
}
