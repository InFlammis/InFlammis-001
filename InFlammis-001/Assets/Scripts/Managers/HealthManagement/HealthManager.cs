using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.HealthManagement
{
    public class HealthManager : IHealthManager
    {
        /// <inheritdoc/>
        public event Action<int, int> HealthLevelChanged;

        /// <inheritdoc/>
        public event Action HasDied;

        /// <inheritdoc/>
        public int MaxHealth { get; set; }

        /// <inheritdoc/>
        private int _health;

        /// <inheritdoc/>
        public int Health
        {
            get => _health;
            set
            {
                //if (value == _health)
                //{
                //    return;
                //}

                _health = value;

                HealthLevelChanged?.Invoke(_health, MaxHealth);

                if (_health <= 0)
                {
                    HasDied?.Invoke();
                }
            }
        }
        
        /// <inheritdoc/>
        public bool IsInvulnerable { get; set; }
        
        /// <inheritdoc/>
        public bool IsDead { get; protected set;
        }

        /// <inheritdoc/>
        public void Heal(int byValue)
        {
            var newHealthValue =  Health + byValue;
            if (newHealthValue > MaxHealth)
            {
                newHealthValue = MaxHealth;
            }

            Health = newHealthValue;

        }

        /// <inheritdoc/>
        public void Heal()
        {
            Heal(MaxHealth);
            IsDead = false;
        }

        /// <inheritdoc/>
        public void Damage(int byValue)
        {
            if (IsInvulnerable) return;

            var newHealthValue = Health - byValue;
            if (newHealthValue < 0)
            {
                newHealthValue = 0;
            }

            Health = newHealthValue;
        }

        /// <inheritdoc/>
        public void Kill()
        {
            Damage(Health);
        }

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="health">Initial health level</param>
        /// <param name="maxHealth">Maximum health level</param>
        /// <param name="isInvulnerable">It is invulnerable at start</param>
        public HealthManager(int health, int maxHealth, bool isInvulnerable)
        {
            this.Health = health;
            this.MaxHealth = maxHealth;
            this.IsInvulnerable = isInvulnerable;
        }

    }
}
