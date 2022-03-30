﻿using FightShipArena.Assets.Scripts.Managers.SceneManagement;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    public abstract class LevelManager : 
        SceneManager, 
        ILevelManager
    {
        [SerializeField] private Messenger _messenger;
        public IMessenger Messenger => _messenger;

        /// <inheritdoc/>
        public abstract event EventHandler<Sound> PlaySoundEvent;

        /// <inheritdoc/>
        public abstract event Action ReturnToMainEvent;

        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <inheritdoc/>
        public virtual void Move(InputAction.CallbackContext context){}

        /// <inheritdoc/>
        public virtual void DisablePlayerInput(){}

        /// <inheritdoc/>
        public virtual void EnablePlayerInput(){}

        /// <inheritdoc/>
        public virtual void OnStart() 
        {
            var player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                throw new NullReferenceException("Player object not found");
            }

            this.PlayerControllerCore = player.GetComponent<IPlayerController>().Core;
        }

        /// <inheritdoc/>
        public virtual void OnAwake() { }

        /// <inheritdoc/>
        public virtual void ReturnToMain() { }
    }
}
