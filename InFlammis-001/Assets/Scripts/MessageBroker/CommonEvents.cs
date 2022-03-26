using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    [Serializable] public class HasDied : UnityEvent<object, string> { }
    //[Serializable] public class DamageReceived : UnityEvent<object, string, int> { }
    [Serializable] public class HealthLevelChanged : UnityEvent<object, string, int, int> { }

}
