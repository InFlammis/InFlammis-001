﻿using FightShipArena.Assets.Scripts.Managers.SoundManagement;
using System;

namespace FightShipArena.Assets.Scripts.Managers.Menus
{
    public interface IHelpMenuManager
    {
        event EventHandler BackEvent;
        event EventHandler<Sound> PlaySoundEvent;

        void OnStart();
        void OnAwake();
        void BackToMainMenu();
    }
}
