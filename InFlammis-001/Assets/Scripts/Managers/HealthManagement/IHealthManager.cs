﻿using System;

namespace FightShipArena.Assets.Scripts.Managers.HealthManagement
{
    /// <summary>
    /// This class takes count of the health status of a game character
    /// </summary>
    public interface IHealthManager
    {
        ///// <summary>
        ///// Event raised when the character Health level changes
        ///// </summary>
        //event Action<int, int> HealthLevelChanged;

        ///// <summary>
        ///// Event raised when the character dies
        ///// </summary>
        //event Action HasDied;

        /// <summary>
        /// Max health of the character
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Current level of health of the character
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets if the character is invulnerable
        /// </summary>
        bool IsInvulnerable { get; set; }

        /// <summary>
        /// Gets if the character is dead
        /// </summary>
        bool IsDead { get;}

        /// <summary>
        /// Heal the character by a value
        /// </summary>
        /// <param name="byValue">Value to heal the character by</param>
        void Heal(int byValue);

        /// <summary>
        /// Fully heal the character to its maximum health level
        /// </summary>
        void Heal();

        /// <summary>
        /// Apply a damage to the character
        /// </summary>
        /// <param name="byValue">Value to heal the character by</param>
        void Damage(int byValue);

        /// <summary>
        /// Kill the character
        /// </summary>
        void Kill();
    }
}