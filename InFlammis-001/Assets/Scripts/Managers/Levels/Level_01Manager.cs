using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
using FightShipArena.Assets.Scripts.Managers.ScoreManagement;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FightShipArena.Assets.Scripts.Managers.Levels
{
    public class Level_01Manager : LevelManager
    {
        /// <inheritdoc/>
        public ILevelManagerCore Core { get; protected set; }
        private PlayerInput _playerInput;

        /// <inheritdoc/>
        public override event EventHandler<Sound> PlaySoundEvent;

        /// <inheritdoc/>
        public override event Action ReturnToMainEvent;

        void Awake()
        {
            ScoreManager = GameObject.GetComponent<IScoreManager>();
            OrchestrationManager = GameObject.GetComponent<IOrchestrationManager>();

            Core = new Level_01ManagerCore(this);

            OnAwake();
        }

        private void OnDestroy()
        {
            Core.OnDestroy();
        }

        void Start()
        {
            OnStart();
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            base.OnStart();
            Core.OnStart();
        }

        /// <inheritdoc/>
        public void OnAwake()
        {
            Core.OnAwake();
        }

        /// <inheritdoc/>
        public override void Move(InputAction.CallbackContext context)
        {
            Core.Move(context);
        }

        /// <inheritdoc/>
        public override void DisablePlayerInput()
        {
            Core.DisablePlayerInput();
        }

        /// <inheritdoc/>
        public override void EnablePlayerInput()
        {
            Core.EnablePlayerInput();
        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            PlaySoundEvent?.Invoke(this, sound);
        }

        /// <inheritdoc/>
        public override void ReturnToMain()
        {
            ReturnToMainEvent?.Invoke();
        }
    }
}
