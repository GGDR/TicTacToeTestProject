using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] Image line = null;

        private GridManager gridManager = null;

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

        private void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        #region Events
        private void OnGameOver(WinResult result)
        {
            Debug.Log("Drawning line");
            Debug.Log(result.lineType + ": " + result.lineIndex);

            switch (result.lineType)
            {
                case LineType.Horizontal:
                    line.rectTransform.position = gridManager.GetGridButtonAtPosition(new Vector2Int(result.lineIndex, 1)).gameObject.GetComponent<RectTransform>().position;
                    line.rectTransform.localScale = new Vector3(28, 1, 1);
                    line.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case LineType.Vertical:
                    line.rectTransform.position = gridManager.GetGridButtonAtPosition(new Vector2Int(1, result.lineIndex)).gameObject.GetComponent<RectTransform>().position;
                    line.rectTransform.localScale = new Vector3(1, 28, 1);
                    line.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case LineType.Diagonal:
                    line.rectTransform.position = gridManager.GetGridButtonAtPosition(new Vector2Int(1, 1)).gameObject.GetComponent<RectTransform>().position;
                    line.rectTransform.localScale = new Vector3(40, 1, 1);
                    line.rectTransform.localRotation = Quaternion.Euler(0, 0, -45);
                    break;
                case LineType.AntiDiagonal:
                    line.rectTransform.position = gridManager.GetGridButtonAtPosition(new Vector2Int(1, 1)).gameObject.GetComponent<RectTransform>().position;
                    line.rectTransform.localScale = new Vector3(40, 1, 1);
                    line.rectTransform.localRotation = Quaternion.Euler(0, 0, 45);
                    break;
                default:
                    break;
            }
        }

        private void OnRematch()
        {
            line.rectTransform.localScale = new Vector3(0, 0, 0);
            line.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion
    } 
}
