using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Managers.Levels.StateMachine;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.MessageBroker.Player;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    public class Level_01ManagerCore : 
        ILevelManagerCore,
        IPlayerEventsSubscriber
    {
        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <inheritdoc/>
        public State CurrentState { get; private set; }
        /// <inheritdoc/>
        public ILevelManager LevelManager { get; set; }

        public Messenger Messenger { get; private set; }

        public bool SpawnEnemiesEnabled = true;

        protected PlayerInput _playerInput;

        private StateConfiguration _stateConfiguration;

        /// <summary>
        /// Create and instance of the class
        /// </summary>
        /// <param name="levelManager">Reference to the <see cref="ILevelManager"/> instance</param>
        public Level_01ManagerCore(ILevelManager levelManager)
        {
            LevelManager = levelManager;

        }

        /// <inheritdoc/>
        public void OnStart()
        {
            Messenger = GameObject.FindObjectOfType<Messenger>();

            (Messenger as IPlayerEventsMessenger).ScoreMultiplierCollected.AddListener((this as IPlayerEventsSubscriber).ScoreMultiplierCollected);
            (Messenger as IPlayerEventsMessenger).HasDied.AddListener((this as IPlayerEventsSubscriber).HasDied);

            this.PlayerControllerCore = LevelManager.PlayerControllerCore;

            StartGame();

        }

        private void StartGame()
        {
            this.PlayerControllerCore.HealthManager.Heal();

            _stateConfiguration = new StateConfiguration(
                levelManagerCore: this,
                orchestrationManager: this.LevelManager.OrchestrationManager,
                hudManager: this.LevelManager.HudManager,
                spawnEnemiesEnabled: true
            );

            ChangeStateRequestEventHandler(this, new WaitForStart(_stateConfiguration));
            Debug.Log($"Level started");

            (Messenger as ILevelEventsMessenger).GameStarted.Invoke(this, null);

        }

        /// <summary>
        /// Handler for a request to change current state.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="e">The new state.</param>
        protected void ChangeStateRequestEventHandler(object sender, State e)
        {
            //Debug.Log($"Changing state from {sender} to {e}");
            if (CurrentState != null)
            {
                CurrentState.ChangeStateRequestEvent -= ChangeStateRequestEventHandler;
                CurrentState.OnExit();
            }
            CurrentState = e;
            CurrentState.ChangeStateRequestEvent += ChangeStateRequestEventHandler;
            CurrentState.OnEnter();
        }

        /// <inheritdoc/>
        public void OnAwake() 
        {
            LevelManager.OrchestrationManager.SendScore += OrchestrationManager_SendScore;
            LevelManager.OrchestrationManager.OrchestrationComplete += OrchestrationManager_OrchestrationComplete;

            _playerInput = LevelManager.GameObject.GetComponent<PlayerInput>();
        }

        /// <summary>
        /// EventHandler for the OrchestrationComplete event of the OrchestrationManager
        /// </summary>
        private void OrchestrationManager_OrchestrationComplete()
        {
            Debug.Log("Orchestration complete");
            //LevelManager.ScoreManager.AddToHighScore();
            ChangeStateRequestEventHandler(this, new Win(_stateConfiguration));

        }

        /// <summary>
        /// EventHandler for the SendScore event of the OrchestrationManager
        /// </summary>
        private void OrchestrationManager_SendScore(int value)
        {
            LevelManager.ScoreManager.AddToScore(value);
        }

        /// <inheritdoc/>
        public void Move(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {

                case InputActionPhase.Started:
                    Debug.Log($"{context.action} Started");
                    break;
                case InputActionPhase.Performed:
                    Debug.Log($"{context.action} Performed");
                    break;
                case InputActionPhase.Canceled:
                    Debug.Log($"{context.action} Cancelled");
                    break;
            }
        }

        /// <inheritdoc/>
        public void DisablePlayerInput()
        {
            _playerInput.enabled = false;
        }

        /// <inheritdoc/>
        public void EnablePlayerInput()
        {
            _playerInput.enabled = true;
        }

        private void GameOver()
        {
            (Messenger as ILevelEventsMessenger).GameOver.Invoke(this, null);
            ChangeStateRequestEventHandler(this, new StateMachine.GameOver(_stateConfiguration));

        }
        void IPlayerEventsSubscriber.HasDied(object publisher, string target)
        {
            GameOver();
        }

        void IPlayerEventsSubscriber.ScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
            LevelManager.ScoreManager.AddToMultiplier(scoreMultiplier);
        }

        void IPlayerEventsSubscriber.HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }
    }
}
