using TMPro;
using UnityEngine;

namespace MatchThreeEngine
{
    public sealed class ScoreCounter : MonoBehaviour
    {
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score == value) return;
                _score = value;
                UpdateScoreText();
            }
        }

        [SerializeField] private TextMeshProUGUI scoreText;

        private Board _board; 
        public void SetBoard(Board board)
        {
            _board = board;
            if (_board != null)
            {
                _board.OnMatch += UpdateScore;
            }
            else
            {
                Debug.LogError("Board instance not found!");
            }
        }

        private void Awake()
        {
            SetBoard(FindObjectOfType<Board>());
        }

        private void UpdateScore(TileTypeAsset tileType, int count)
        {
            Score += count * 10;
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.SetText($"Score = {_score}");
            }
        }
    }
}
