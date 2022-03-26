using System;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Managers.Levels;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies.Pawn
{
    /// <summary>
    /// Specialization of a EnemyController for a Pawn enemy type
    /// </summary>
    public class PawnController : 
        EnemyController,
        IHealthManagerEventsSubscriber
    {
        public Messenger Messenger { get; set; }


        /// <summary>
        /// Event handler for a HasDied event from the HealthManager. Invoked when the enemy dies.
        /// </summary>
        private void HealthManager_HasDied()
        {
            Debug.Log($"Destroying object {this.gameObject.name}");

            _SoundManager.PlayExplodeSound();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }

        #region Unity methods

        void Awake()
        {
            Messenger = GameObject.FindObjectOfType<Messenger>();

            HealthManager = new HealthManager(this.GetInstanceID().ToString(), InitSettings.InitHealth, InitSettings.InitHealth, false);

            SubscribeToHealthManagerEvents();

            Core = new PawnControllerCore(this, HealthManager, InitSettings);
        }

        public virtual void SubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            var subscriber = (this as IHealthManagerEventsSubscriber);
            messenger.HasDied.AddListener(subscriber.HasDied);
            messenger.HealthLevelChanged.AddListener(subscriber.HealthLevelChanged);
        }

        public virtual void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            var subscriber = (this as IHealthManagerEventsSubscriber);
            messenger.HasDied.RemoveListener(subscriber.HasDied);
            messenger.HealthLevelChanged.RemoveListener(subscriber.HealthLevelChanged);
        }


        void Start()
        {

            var sceneManagerGO = GameObject.FindGameObjectWithTag("SceneManager");
            var sceneManager = sceneManagerGO?.GetComponent<LevelManager>();

            if (sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

            _SoundManager = gameObject.GetComponent<EnemySoundManager>();

            if (_SoundManager == null)
            {
                Debug.LogError("SoundManager not found");
            }

            _SoundManager.SceneManager = sceneManager;

            if (InitSettings == null)
            {
                throw new NullReferenceException("InitSettings");
            }

            var player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.Log("Player object not found");
            }
            else
            {
                Core.PlayerControllerCore = player.GetComponent<PlayerController>().Core;
            }

            Core.OnStart();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log($"Collision detected with {col.gameObject.name}");

            switch (col.gameObject.tag)
            {
                case "Player":
                {
                    Core.HandleCollisionWithPlayer();
                    break;
                }
                case "Bullet":
                {
                    //The collision is managed by the bullet
                    _SoundManager.PlayHitSound();
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            //if (Time.frameCount % InitSettings.UpdateEveryXFrames != 0)
            //    return;

            Core.Move();
        }

        void IHealthManagerEventsSubscriber.HasDied(object publisher, string target)
        {
            Debug.Log($"Destroying object {this.gameObject.name}");

            _SoundManager.PlayExplodeSound();

            UnsubscribeToHealthManagerEvents();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }

        void IHealthManagerEventsSubscriber.HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }

        #endregion
    }
}
