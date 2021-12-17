﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.HealthManagement
{
    public class HealthManager : IHealthManager
    {
        public event Action<int> HealthLevelChanged;
        public event Action HasDied;

        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public bool IsInvulnerable { get; set; }

        public void Heal(int byValue)
        {
            Health += byValue;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            HealthLevelChanged?.Invoke(Health);
        }

        public void Heal()
        {
            Health = MaxHealth;
        }

        public void Damage(int byValue)
        {
            if (IsInvulnerable) return;

            Health -= byValue;

            HealthLevelChanged?.Invoke(Health);

            if (Health <= 0)
            {
                HasDied?.Invoke();
            }
        }

        public void Kill()
        {
            Damage(Health);
        }

        public HealthManager(int health, int maxHealth, bool isInvulnerable)
        {
            this.Health = health;
            this.MaxHealth = maxHealth;
            this.IsInvulnerable = isInvulnerable;
        }

    }
}
