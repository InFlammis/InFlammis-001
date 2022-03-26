using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Player;
using UnityEngine;

namespace FightShipArena.Assets.Scripts
{
    /// <summary>
    /// Implementation of a IMyMonoBehaviour. MonoBehaviour object implementing the IMyMonoBehaviour interface
    /// </summary>
    public class MyMonoBehaviour : MonoBehaviour, IMyMonoBehaviour
    {
        /// <inheritdoc/>
        public virtual GameObject GameObject => base.gameObject;
    }
}
