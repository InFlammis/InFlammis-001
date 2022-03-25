﻿using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using System;

namespace FightShipArena.Assets.Scripts.Managers.Menus
{
    /// <summary>
    /// Interface for the HelpMenuManager
    /// </summary>
    public interface IHelpMenuManager
    {
        /// <summary>
        /// Event raised by the Back button
        /// </summary>
        event EventHandler BackEvent;

        /// <summary>
        /// Event raised to play a sound
        /// </summary>
        event EventHandler<Sound> PlaySoundEvent;

        /// <summary>
        /// Invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        void OnAwake();

        /// <summary>
        /// Invoked to return to the Main Menu
        /// </summary>
        void BackToMainMenu();
    }
}
