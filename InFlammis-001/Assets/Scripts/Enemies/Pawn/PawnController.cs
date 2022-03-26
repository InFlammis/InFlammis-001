using System;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Managers.Levels;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies.Pawn
{
    /// <summary>
    /// Specialization of a EnemyController for a Pawn enemy type
    /// </summary>
    public class PawnController : EnemyController
    {

        /// <summary>
        /// Event handler for a HealthLevelChanged event from the HealthManager. Invoked when the enemy loses energy.
        /// </summary>
        /// <param name="value">New health level</param>
        /// <param name="maxValue">Max value of health</param>
        private void HealthManager_HealthLevelChanged(int value, int maxValue)
        {
        }

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
            HealthManager = new HealthManager(InitSettings.InitHealth, InitSettings.InitHealth, false);
            HealthManager.HasDied += HealthManager_HasDied;
            HealthManager.HealthLevelChanged += HealthManager_HealthLevelChanged;
            Core = new PawnControllerCore(this, HealthManager, InitSettings);
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

        #endregion
    }
}
