using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Implementation of a ScoreManager
    /// </summary>
    public class ScoreManager : MyMonoBehaviour, IScoreManager
    {
        /// <summary>
        /// Event raised when the current score changes
        /// </summary>
        public UnityEvent<int> ScoreChanged;

        /// <summary>
        /// Event raised when the current multiplier changes
        /// </summary>
        public UnityEvent<int> MultiplierChanged;

        /// <summary>
        /// Event raised when the current hi-score changes
        /// </summary>
        public UnityEvent<int> HiScoreChanged;

        /// <summary>
        /// The current score
        /// </summary>
        private Score CurrentScore;

        /// <summary>
        /// The list of High Scores
        /// </summary>
        public HighScoreRecorder HighScores;

        private int _multiplier;
        
        /// <summary>
        /// The current Multiplier score
        /// </summary>
        public int Multiplier
        {
            get => _multiplier;
            protected set
            {
                if (value == _multiplier)
                {
                    return;
                }

                _multiplier = value;
                //Debug.Log($"Multiplier set to {Multiplier}");
                NotifyMultiplierValueChange();
            }
        }

        void Start()
        {
            ResetCurrentScore();
            ResetMultiplier();
            NotifyHighScoreValueChange();

        }

        private void NotifyHighScoreValueChange()
        {
            var highScore = HighScores.HighScores.OrderByDescending(x => x.Value).FirstOrDefault();
            int highScoreValue = 0;
            if (highScore != null)
            {
                highScoreValue = highScore.Value;
            }

            HiScoreChanged.Invoke(highScoreValue);
        }

        private void NotifyScoreValueChange()
        {
            ScoreChanged?.Invoke(CurrentScore.Value);
        }

        private void NotifyMultiplierValueChange()
        {
            MultiplierChanged?.Invoke(Multiplier);
        }

        /// <inheritdoc/>
        public void AddToHighScore()
        {
            if (CurrentScore.Value == 0)
            {
                return;
            }
            CurrentScore.Date = DateTime.Now.ToString("s");
            CurrentScore.Name = "DDR";
            HighScores.HighScores.Add(CurrentScore);
            NotifyHighScoreValueChange();
        }

        /// <inheritdoc/>
        public void AddToScore(int score)
        {
            var totScore = score * Multiplier;
            CurrentScore.Value += totScore;
            //Debug.Log($"Score set to {CurrentScore.Value}");
            NotifyScoreValueChange();
        }

        /// <inheritdoc/>
        public void AddToMultiplier(int multiplier)
        {
            Multiplier += multiplier;
        }

        /// <inheritdoc/>
        public void ResetMultiplier()
        {
            Multiplier = 1;
        }

        /// <inheritdoc/>
        public void ResetCurrentScore()
        {
            CurrentScore = new Score();
            NotifyScoreValueChange();

        }

        /// <inheritdoc/>
        public void ResetHighScore()
        {
            HighScores.HighScores.Clear();
            HiScoreChanged?.Invoke(HighScores.HighScores.OrderByDescending(x => x.Value).Select(x => x.Value).FirstOrDefault());

        }
    }
}
