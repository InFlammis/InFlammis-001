using FightShipArena.Assets.Scripts.Managers.HudManagement;
using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.Managers.Levels.StateMachine
{
    /// <summary>
    /// Initial state configuration. Contains all reference needed to the state.
    /// </summary>
    public class StateConfiguration
    {
        public readonly Messenger Messenger;
        /// <summary>
        /// Reference to the LevelManagerCore instance
        /// </summary>
        public readonly ILevelManagerCore LevelManagerCore;

        /// <summary>
        /// Reference to the OrchestrationManager instance
        /// </summary>
        public readonly IOrchestrationManager OrchestrationManager;

        /// <summary>
        /// Reference to the HudManager instance
        /// </summary>
        public readonly IHudManager HudManager;

        /// <summary>
        /// Enable the spawning of enemies
        /// </summary>
        public bool SpawnEnemiesEnabled;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="messenger"><see cref="Messenger"/></param>
        /// <param name="levelManagerCore"><see cref="ILevelManagerCore"/></param>
        /// <param name="orchestrationManager"><see cref="IOrchestrationManager"/></param>
        /// <param name="hudManager"><see cref="IHudManager"/></param>
        /// <param name="spawnEnemiesEnabled"><see cref="SpawnEnemiesEnabled"/></param>
        public StateConfiguration(
            Messenger messenger,
            ILevelManagerCore levelManagerCore,
            IOrchestrationManager orchestrationManager,
            IHudManager hudManager,
            bool spawnEnemiesEnabled = true)
        {
            Messenger = messenger;
            LevelManagerCore = levelManagerCore;
            OrchestrationManager = orchestrationManager;
            HudManager = hudManager;
            SpawnEnemiesEnabled = spawnEnemiesEnabled;
        }

    }
}
