﻿using FightShipArena.Assets.Scripts.Managers.HudManagement;
using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
using FightShipArena.Assets.Scripts.Managers.ScoreManagement;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    /// <summary>
    /// Interface of a generic LevelManager
    /// </summary>
    public interface ILevelManager : IMyMonoBehaviour
    {

        /// <summary>
        /// Event raised to play a sound
        /// </summary>
        event EventHandler<Sound> PlaySoundEvent;

        /// <summary>
        /// Event raised to ask for a return to the main menu
        /// </summary>
        event Action ReturnToMainEvent;

        IMessenger Messenger { get; }

        /// <summary>
        /// Reference to the PlayerControllerCore instance
        /// </summary>
        IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <summary>
        /// Reference to the OrchestrationManager instance
        /// </summary>
        IOrchestrationManager OrchestrationManager { get; set; }

        /// <summary>
        /// Invoked on start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Event Handler for a move action
        /// </summary>
        /// <param name="context"></param>
        void Move(InputAction.CallbackContext context);

        /// <summary>
        /// Disable the responsiveness to the Player input
        /// </summary>
        void DisablePlayerInput();

        /// <summary>
        /// Enable the responsiveness to the Player input
        /// </summary>
        void EnablePlayerInput();

        /// <summary>
        /// Return to main menu
        /// </summary>
        void ReturnToMain();

    }
}