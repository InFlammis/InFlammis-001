using System;
using FightShipArena.Assets.Scripts.Managers.GameManagement.StateMachine;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.GameManagement
{
    /// <summary>
    /// Core for the GameManager class. Contains the logic that rules the GameManager behaviour.
    /// </summary>
    public class GameManagerCore : IGameManager
    {
        /// <summary>
        /// Reference to the Parent instance
        /// </summary>
        public readonly IMyMonoBehaviour Parent;

        private IUnitySceneManagerWrapper _sceneManagerWrapper;

        /// <summary>
        /// Stack for the State machine
        /// </summary>
        protected StateStack _stateStack = new StateStack();

        /// <inheritdoc/>
        public ISoundManager SoundManager { get; protected set; }

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="parent">Reference to the parent object</param>
        public GameManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
            SoundManager = (Parent as IGameManager).SoundManager;
            _sceneManagerWrapper = UnitySceneManagerWrapper.Instance;
        }

        #region MonoBehaviour methods

        public void OnAwake()
        {
            _stateStack.PoppingStateEvent += StateStack_PoppingStateEvent;
            _stateStack.PushingStateEvent += StateStack_PushingStateEvent;
        }

        public void OnStart()
        {
            PushState(new Init(this, _sceneManagerWrapper));
        }

        #endregion

        #region Event Handlers for StateStack events

        /// <summary>
        /// Attach the eventhandlers to the current state
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="state">New state being pushed into the stack</param>
        protected virtual void StateStack_PushingStateEvent(object sender, State state)
        {
            state.PauseGameEvent += State_PauseGameEvent;
            state.PlayGameEvent += State_PlayGameEvent;
            state.ResumeGameEvent += State_ResumeGameEvent;
            state.QuitCurrentGameEvent += State_QuitCurrentGameEvent;
            state.QuitGameEvent += State_QuitGameEvent;
            state.CreditsEvent += State_CreditsEvent;
            state.HelpEvent += State_HelpEvent;
            state.BackToMainMenuEvent += State_BackToMainMenuEvent;
        }

        /// <summary>
        /// Detache the eventhandlers from the state being popped out of the stack
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="state">The state being popped out of the stack</param>
        protected virtual void StateStack_PoppingStateEvent(object sender, State state)
        {
            state.PauseGameEvent -= State_PauseGameEvent;
            state.PlayGameEvent -= State_PlayGameEvent;
            state.ResumeGameEvent -= State_ResumeGameEvent;
            state.QuitCurrentGameEvent -= State_QuitCurrentGameEvent;
            state.QuitGameEvent -= State_QuitGameEvent;
            state.CreditsEvent -= State_CreditsEvent;
            state.HelpEvent -= State_HelpEvent;
            state.BackToMainMenuEvent -= State_BackToMainMenuEvent;
        }

        #endregion

        #region Handler methods for StateStack

        protected virtual void ReplaceState(State state)
        {
            PopState();

            PushState(state);
        }

        protected virtual void PushState(State state)
        {
            _stateStack.Push(state);
        }

        protected virtual void PopState()
        {
            var state = _stateStack.Pop();
        }

        #endregion

        #region Event Handlers for State events

        /// <summary>
        /// Handle a request to pause the game
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_PauseGameEvent(object sender, EventArgs e)
        {
            PushState(new Pause(this, _sceneManagerWrapper));
        }

        /// <summary>
        /// Handle a request to resume a previously paused game
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_ResumeGameEvent(object sender, EventArgs e)
        {
            PopState();
        }

        /// <summary>
        /// Handle a request to open the Credits menu
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        private void State_CreditsEvent(object sender, EventArgs e)
        {
            ReplaceState(new Credits(this, _sceneManagerWrapper));
        }

        /// <summary>
        /// Handle a request to open the Help menu
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        private void State_HelpEvent(object sender, EventArgs e)
        {
            ReplaceState(new Help(this, _sceneManagerWrapper));
        }

        /// <summary>
        /// Handle a request to start a new game
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_PlayGameEvent(object sender, EventArgs e)
        {
            ReplaceState(new Play(this, _sceneManagerWrapper));
        }

        /// <summary>
        /// Handle a request to quit the current game
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_QuitCurrentGameEvent(object sender, EventArgs e)
        {
            _stateStack.Clear();
            PushState(new Init(this, _sceneManagerWrapper));
        }

        /// <summary>
        /// Handle a request to close the game and return to the OS
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_QuitGameEvent(object sender, EventArgs e)
        {
            PushState(new Quit(this, _sceneManagerWrapper));

            Application.Quit();
        }

        /// <summary>
        /// Handle a request to return to the Main menu
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e"></param>
        protected virtual void State_BackToMainMenuEvent(object sender, EventArgs e)
        {
            ReplaceState(new Init(this, _sceneManagerWrapper));
        }

        #endregion

        #region Input Event Handlers

        /// <summary>
        /// Event Handler for PauseResume actions
        /// Invokes PauseResume on the current state
        /// <remarks>This method is not testable, as the input parameter cannot be mocked</remarks>
        /// </summary>
        /// <param name="context"></param>
        public void OnPauseResumeGame(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    //Debug.Log($"{context.action} Performed");

                    _stateStack.Peek()?.PauseResumeGame();
                    break;
            }
        }

        #endregion
    }
}
