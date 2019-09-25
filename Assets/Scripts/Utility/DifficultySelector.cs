using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class DifficultySelector : MonoBehaviour
    {
        [SerializeField] private Difficulty difficulty = Difficulty.Easy;

        public void SaveSelectedDifficulty()
        {
            TicTacToeUtility.SaveDifficulty(difficulty);
        }
    }
}
