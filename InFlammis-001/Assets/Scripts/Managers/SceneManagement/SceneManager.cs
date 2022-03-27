using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.SceneManagement
{
    /// <summary>
    /// Base class for a scene manager. A scene can be a level scene or a menu scene.
    /// Managers for levels and menus inherit from here.
    /// </summary>
    public class SceneManager : MyMonoBehaviour, IMyMonoBehaviour
    {
        void Start()
        {
            //Debug.Log("Scene started");
        }

        /// <summary>
        /// Method invoked to request to play a sound
        /// </summary>
        /// <param name="sound"></param>
        public virtual void PlaySound(Sound sound) { }
    }
}
