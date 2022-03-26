using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.PowerUps.EnergyCharger
{
    /// <summary>
    /// Power-up of type EnergyCharger. When collected by the Player, increases its health level by the carried amount.
    /// </summary>
    public class EnergyCharger : PowerUpBase
    {
        /// <summary>
        /// Manage collision with player and transfer its value to player's healthManager
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            var playerGo = collision.gameObject;
            var playerController = playerGo.GetComponent<PlayerController>();
            playerController.Core.HealthManager.Heal((int)InitSettings.Value);
            GameObject.Destroy(this.GameObject);
        }
    }
}
