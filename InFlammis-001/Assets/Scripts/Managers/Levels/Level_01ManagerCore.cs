using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Managers.Levels.StateMachine;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.MessageBroker.Events;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    public class Level_01ManagerCore : 
        ILevelManagerCore
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

            (Messenger as IOrchestrationEventsMessenger).OrchestrationComplete.AddListener(OrchestrationManagerOrchestrationComplete);

            (Messenger as IPlayerEventsMessenger).HasDied.AddListener(PlayerHasDied);

            this.PlayerControllerCore = LevelManager.PlayerControllerCore;

            StartGame();
        }

        public void OnDestroy()
        {
            (Messenger as IOrchestrationEventsMessenger).OrchestrationComplete.RemoveListener(OrchestrationManagerOrchestrationComplete);

            (Messenger as IPlayerEventsMessenger).HasDied.RemoveListener(PlayerHasDied);
        }

        private void StartGame()
        {
            this.PlayerControllerCore.HealthManager.Heal();

            _stateConfiguration = new StateConfiguration(
                messenger: Messenger,
                levelManagerCore: this,
                orchestrationManager: this.LevelManager.OrchestrationManager,
                hudManager: this.LevelManager.HudManager,
                spawnEnemiesEnabled: true
            );

            ChangeStateRequestEventHandler(this, new WaitForStart(_stateConfiguration));

            (Messenger as ILevelEventsPublisher).PublishGameStarted(this, null);

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
            _playerInput = LevelManager.GameObject.GetComponent<PlayerInput>();
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
                    //Debug.Log($"{context.action} Started");
                    break;
                case InputActionPhase.Performed:
                    //Debug.Log($"{context.action} Performed");
                    break;
                case InputActionPhase.Canceled:
                    //Debug.Log($"{context.action} Cancelled");
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
        void PlayerHasDied(object publisher, string target)
        {
            GameOver();
        }

        void OrchestrationManagerOrchestrationComplete(object publisher, string target)
        {
            ChangeStateRequestEventHandler(this, new Win(_stateConfiguration));
        }
    }
}
