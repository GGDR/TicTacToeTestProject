using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public enum Symbol { X, O }
    public enum Difficulty { Easy, Normal, Hard }

    public enum LineType { None, Horizontal, Vertical, Diagonal, AntiDiagonal}

    public struct PositionScore
    {
        public Vector2Int position;
        public int index;
        public int score;

        public PositionScore(Vector2Int _position, int _index, int _score)
        {
            position = _position;
            index = _index;
            score = _score;
        }
    }

    public struct WinResult
    {
        public int score;
        public LineType lineType;
        public int lineIndex;

        public WinResult(int _score, LineType _lineType, int _lineIndex)
        {
            score = _score;
            lineType = _lineType;
            lineIndex = _lineIndex;
        }
    }
}
