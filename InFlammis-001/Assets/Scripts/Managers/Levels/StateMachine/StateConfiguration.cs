using FightShipArena.Assets.Scripts.Managers.HudManagement;
using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
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
        /// <param name="levelManagerCore"><see cref="ILevelManagerCore"/></param>
        /// <param name="orchestrationManager"><see cref="IOrchestrationManager"/></param>
        /// <param name="hudManager"><see cref="IHudManager"/></param>
        /// <param name="spawnEnemiesEnabled"><see cref="SpawnEnemiesEnabled"/></param>
        public StateConfiguration(
            ILevelManagerCore levelManagerCore,
            IOrchestrationManager orchestrationManager,
            IHudManager hudManager,
            bool spawnEnemiesEnabled = true)
        {
            LevelManagerCore = levelManagerCore;
            OrchestrationManager = orchestrationManager;
            HudManager = hudManager;
            SpawnEnemiesEnabled = spawnEnemiesEnabled;
        }

    }
}
