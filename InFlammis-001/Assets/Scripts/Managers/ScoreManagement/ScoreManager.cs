using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.MessageBroker.Events;
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
        [SerializeField] private Messenger _messenger;
        public Messenger Messenger => _messenger;

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
                NotifyMultiplierValue();
            }
        }

        void Awake()
        {
            (Messenger as IPlayerEventsMessenger).ScoreMultiplierCollected.AddListener(PlayerScoreMultiplierCollected);
            (Messenger as IEnemyEventsMessenger).PlayerScored.AddListener(EnemyPlayerScored);
            (Messenger as ILevelEventsMessenger).GameOver.AddListener(LevelGameOver);
            (Messenger as ILevelEventsMessenger).GameStarted.AddListener(LevelGameStarted);
            (Messenger as ILevelEventsMessenger).PlayerWins.AddListener(LevelPlayerWins);
        }

        void Start()
        {
            NotifyHighScoreValue();
        }

        private void LevelPlayerWins(object publisher, string target)
        {
            AddToHighScore();
        }

        private void LevelGameStarted(object publisher, string target)
        {
            ResetCurrentScore();
            ResetMultiplier();
        }

        private void LevelGameOver(object publisher, string target)
        {
            AddToHighScore();
        }

        private void EnemyPlayerScored(object publisher, string target, int value)
        {
            AddToScore(value);
        }

        private void PlayerScoreMultiplierCollected(object publisher, string target, int value)
        {
            AddToMultiplier(value);
        }

        private void NotifyHighScoreValue()
        {
            var highScore = HighScores.HighScores.OrderByDescending(x => x.Value).FirstOrDefault();
            int highScoreValue = 0;
            if (highScore != null)
            {
                highScoreValue = highScore.Value;
            }
            (Messenger as IScoreManagerEventsMessenger).HiScoreChanged.Invoke(this, null, highScoreValue);
        }

        private void NotifyScoreValue()
        {
            (Messenger as IScoreManagerEventsMessenger).ScoreChanged.Invoke(this, null, CurrentScore.Value);
        }

        private void NotifyMultiplierValue()
        {
            (Messenger as IScoreManagerEventsMessenger).MultiplierChanged.Invoke(this, null, _multiplier);
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
            NotifyHighScoreValue();
        }

        /// <inheritdoc/>
        public void AddToScore(int score)
        {
            var totScore = score * Multiplier;
            CurrentScore.Value += totScore;
            NotifyScoreValue();
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
            NotifyScoreValue();
        }

        /// <inheritdoc/>
        public void ResetHighScore()
        {
            HighScores.HighScores.Clear();
            var hiScoreValue = HighScores.HighScores.OrderByDescending(x => x.Value).Select(x => x.Value).FirstOrDefault();
            (Messenger as IScoreManagerEventsMessenger).HiScoreChanged.Invoke(this, null, hiScoreValue);
        }
    }
}
