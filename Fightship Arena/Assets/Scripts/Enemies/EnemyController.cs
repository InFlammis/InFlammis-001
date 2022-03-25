﻿using System.Linq;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies
{
    /// <summary>
    /// Abstract class for an Enemy controller
    /// </summary>
    public abstract class EnemyController : MyMonoBehaviour, IEnemyController
    {
        /// <summary>
        /// Instance of EnemySoundManager
        /// </summary>
        protected EnemySoundManager _SoundManager;

        /// <summary>
        /// Instance of EnemySettings
        /// </summary>
        [SerializeField] private EnemySettings _initSettings;

        /// <summary>
        /// Core class for the EnemyController
        /// </summary>
        public IEnemyControllerCore Core { get; set; }

        /// <summary>
        /// Instance of the HealthManager
        /// </summary>
        public IHealthManager HealthManager { get; set; }

        /// <summary>
        /// Collection of available weapons
        /// </summary>
        public WeaponBase[] Weapons { get; set; }

        /// <summary>
        /// Instance of EnemySettings
        /// </summary>
        public EnemySettings InitSettings { get => _initSettings; }

        /// <summary>
        /// Instance of the explosion effect - This animation is activated when the enemy is destroyed.
        /// </summary>
        public GameObject ExplosionEffect;

        /// <summary>
        /// Instance of the Spawn activation effect - This animation is activated when the enemy is spawned.
        /// </summary>
        public GameObject SpawnActivationEffect;

        /// <summary>
        /// Manage the release of a power up when the enemy is destroyed
        /// </summary>
        protected virtual void ReleasePowerUp()
        {
            if (!_initSettings.Powerups.Any())
            {
                return;
            }

            var value = (UnityEngine.Random.value * _initSettings.Powerups.Count) % _initSettings.Powerups.Count;
            var index = Mathf.FloorToInt(value);

            var selectedPowerUp = _initSettings.Powerups[index];
            if (selectedPowerUp.ReleaseRate < value - index)
            {
                return;
            }

            var instance = GameObject.Instantiate(selectedPowerUp.PowerUp);
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(instance, UnityEngine.SceneManagement.SceneManager.GetSceneAt(1));

            instance.transform.parent = null;
            instance.transform.position = this.transform.position;
        }
    }
}