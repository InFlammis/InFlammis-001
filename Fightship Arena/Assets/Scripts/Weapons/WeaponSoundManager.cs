﻿using FightShipArena.Assets.Scripts.Managers.SceneManagement;
using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Weapons
{
    /// <summary>
    /// Class that manages the sounds produced by a weapon
    /// </summary>
    public class WeaponSoundManager : MyMonoBehaviour
    {
        /// <summary>
        /// Sound reproduced on fire.
        /// </summary>
        [SerializeField]
        private Sound FireSound;

        /// <summary>
        /// Instance of the current SceneManager
        /// </summary>
        private SceneManager SceneManager;

        private void Start()
        {
            var sceneManagerGo = GameObject.FindGameObjectWithTag("SceneManager");
            SceneManager = sceneManagerGo?.GetComponent<SceneManager>();

            if(SceneManager == null)
            {
                throw new Exception("SceneManager not found");
            }
        }

        /// <summary>
        /// Play the fire sound
        /// </summary>
        public void PlayFireSound()
        {
            SceneManager.PlaySound(FireSound);
        }
    }
}
