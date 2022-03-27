using System;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Managers.Levels;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.Player;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies.Infantry
{
    /// <summary>
    /// Specialization of a EnemyController for an Infantry enemy type
    /// </summary>
    public class InfantryController : 
        EnemyController,
        IHealthManagerEventsSubscriber
    {
        public Messenger Messenger { get; set; }

        private string _Target;

        #region Unity methods

        void Awake()
        {
            Messenger = GameObject.FindObjectOfType<Messenger>();
            _Target = GameObject.GetInstanceID().ToString();

            HealthManager = new HealthManager(this.GetInstanceID().ToString(), InitSettings.InitHealth, InitSettings.InitHealth, false);

            SubscribeToHealthManagerEvents();

            CheckWeaponsConfiguration();

            Core = new InfantryControllerCore(this, HealthManager, InitSettings);

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

            if(sceneManager == null)
            {
                Debug.LogError("SceneManager not found");
            }

            _SoundManager = gameObject.GetComponent<EnemySoundManager>();

            if(_SoundManager == null)
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
            Core?.Move();
        }

        #endregion

        /// <summary>
        /// Checks that the weapon configuration is available for each weapon
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CheckWeaponsConfiguration()
        {
            Weapons = this.GameObject.GetComponentsInChildren<WeaponBase>();
            foreach (var weapon in Weapons)
            {
                //If the current weapon has no configuration, throw.
                if (weapon.InitSettings == null)
                {
                    throw new Exception($"No settings for weapon {weapon.WeaponType}");
                }
            }
        }

        void IHealthManagerEventsSubscriber.HasDied(object publisher, string target)
        {
            if (target != _Target)
            {
                return;
            }

            Debug.Log($"Destroying object {this.gameObject.name}");

            _SoundManager.PlayExplodeSound();

            UnsubscribeToHealthManagerEvents();

            var eeInstance = Instantiate(this.ExplosionEffect, this.gameObject.transform);
            eeInstance.transform.SetParent(null);

            Destroy(eeInstance, 4);

            GameObject.Destroy(this.gameObject);
            ReleasePowerUp();
        }

        void IHealthManagerEventsSubscriber.HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }
    }
}
