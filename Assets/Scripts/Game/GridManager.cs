using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class GridManager : MonoBehaviour
    {
        public static Action<int [,], int> OnGridCellFilled;

        [HideInInspector] public int[,] grid { get; private set; } = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        [HideInInspector] public CanvasGroup canvasGroup = null;

        private const int gridSize = 3;
        private int maxCellsCount = 9;
        private int emptyCellsCount = 0;

        private Dictionary<Vector2Int, Button> gridButtons = new Dictionary<Vector2Int, Button>();

        #region OnEnable/OnDisable
        private void OnEnable()
        {
            GridCellHandler.OnCellClick += OnCellClick;
            GameStateManager.OnRematch += OnRematch;
        }

        private void OnDisable()
        {
            GridCellHandler.OnCellClick -= OnCellClick;
            GameStateManager.OnRematch -= OnRematch;
        }
        #endregion

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            emptyCellsCount = maxCellsCount;
        }

        private void Update()
        {
            /*
            if (Input.GetKeyUp(KeyCode.M))
            {
                int[,] testGrid = { { 1, -1, -1 }, { -1, 1, 1 }, { 1, 1, -1 } };

                //Debug.Log(TicTacToeUtility.GetBestMoveEasy(testGrid, 1));
            }
            else if (Input.GetKeyUp(KeyCode.T))
            {
                int[,] testGrid = { { 0, 1, 0 }, { 0, 0, 1 }, { -1, -1, 1 } };

                PositionScore ps = TicTacToeUtility.MiniMax(testGrid, 2, true);
                Debug.Log(ps.position);
            }
            */
        }

        #region Events
        private void OnCellClick(int row, int column, int symbolIndex)
        {
            FillCell(row, column, symbolIndex);
        }

        private void OnRematch()
        {
            ResetGrid();
        }
        #endregion

        private void ResetGrid()
        {
            emptyCellsCount = maxCellsCount;
            for (int r = 0; r < gridSize; r++)
            {
                for (int c = 0; c < gridSize; c++)
                {
                    grid[r, c] = 0;
                }
            }
        }

        private void FillCell(int row, int column, int symbolIndex)
        {
            grid[row, column] = symbolIndex;
            emptyCellsCount--;
            OnGridCellFilled?.Invoke(grid, gridSize);
        }

        public void AddButtonToGrid(Vector2Int gridPosition, Button button)
        {
            gridButtons.Add(gridPosition, button);
        }

        public Button GetGridButtonAtPosition(Vector2Int gridPosition)
        {
            Button returnedButton;
            gridButtons.TryGetValue(gridPosition, out returnedButton);
            return returnedButton;
        }

    } // class
} // namespace
