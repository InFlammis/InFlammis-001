﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.Levels.StateMachine
{
    /// <summary>
    /// Generic abstract state for a Level
    /// </summary>
    public abstract class State
    {
        public readonly StateConfiguration Configuration;
        /// <summary>
        /// Event raised to invoke a change of state.
        /// </summary>
        public abstract event EventHandler<State> ChangeStateRequestEvent;

        /// <summary>
        /// Gets or sets the delay in seconds during a change of state.
        /// </summary>
        public virtual float ChangeStateDelay { get; set; } = 1;

        public State(StateConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Invoked when the state enters..
        /// </summary>
        public virtual void OnEnter()
        {
            Debug.Log($"Entering { GetType().Name} state");
        }

        /// <summary>
        /// Invoked when the state exits.
        /// </summary>
        public virtual void OnExit()
        {
            Debug.Log($"Exiting { GetType().Name} state");
        }

    }
}
