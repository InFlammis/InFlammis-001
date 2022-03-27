using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// Interface for the OrchestrationManager
    /// </summary>
    public interface IOrchestrationManager
    {
        /// <summary>
        /// Event raised to notify of a change in the player's score
        /// </summary>
        event Action<int> SendScore;

        /// <summary>
        /// Start the orchestration
        /// </summary>
        void Run();

        /// <summary>
        /// Stop the orchestration
        /// </summary>
        void Stop();
    }
}
