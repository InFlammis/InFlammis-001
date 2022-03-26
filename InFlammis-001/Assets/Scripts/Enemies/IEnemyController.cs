using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies
{
    /// <summary>
    /// Interface for an IEnemyController
    /// </summary>
    public interface IEnemyController : IMyMonoBehaviour
    {
        /// <summary>
        /// Instance of EnemySettings
        /// </summary>
        EnemySettings InitSettings { get; }

        /// <summary>
        /// Instance of the game object for the enemy
        /// </summary>
        GameObject GameObject { get; }

        /// <summary>
        /// Core class for the EnemyController
        /// </summary>
        IEnemyControllerCore Core { get; set; }

        /// <summary>
        /// Instance of the HealthManager
        /// </summary>
        IHealthManager HealthManager { get; set; }

        /// <summary>
        /// Collection of available weapons
        /// </summary>
        WeaponBase[] Weapons { get; set; }
    }
}