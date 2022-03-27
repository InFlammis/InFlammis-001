using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Managers.HudManagement;
using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
using FightShipArena.Assets.Scripts.Managers.SceneManagement;
using FightShipArena.Assets.Scripts.Managers.ScoreManagement;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    public abstract class LevelManager : 
        SceneManager, 
        ILevelManager
    {
        /// <inheritdoc/>
        public abstract event EventHandler<Sound> PlaySoundEvent;

        /// <inheritdoc/>
        public abstract event Action ReturnToMainEvent;

        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <inheritdoc/>
        public IScoreManager ScoreManager { get; set; }

        /// <inheritdoc/>
        public IOrchestrationManager OrchestrationManager { get; set; }

        /// <inheritdoc/>
        public IHudManager HudManager { get; set; }

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

            this.HudManager = GetComponent<IHudManager>();
            if(HudManager == null)
            {
                Debug.LogError("HudManager not found in LevelManager OnStart");
            }
        }

        /// <inheritdoc/>
        public virtual void OnAwake() { }

        /// <inheritdoc/>
        public virtual void ReturnToMain() { }
    }
}
