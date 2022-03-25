﻿using FightShipArena.Assets.Scripts.Managers.Levels.StateMachine;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    /// <summary>
    /// Interface of a generic LevelManagerCore
    /// </summary>
    public interface ILevelManagerCore
    {
        /// <summary>
        /// Current state of the levelManagerCore
        /// </summary>
        State CurrentState { get;}

        /// <summary>
        /// Reference to the LevelManager instance
        /// </summary>
        ILevelManager LevelManager { get; set; }

        /// <summary>
        /// Reference to the PlayerControllercore
        /// </summary>
        IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <summary>
        /// Invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Event Handler for a move action
        /// </summary>
        /// <param name="context"></param>
        void Move(InputAction.CallbackContext context);

        /// <summary>
        /// Enable the responsiveness to the Player input
        /// </summary>
        void DisablePlayerInput();

        /// <summary>
        /// Disable the responsiveness to the Player input
        /// </summary>
        void EnablePlayerInput();
    }
}