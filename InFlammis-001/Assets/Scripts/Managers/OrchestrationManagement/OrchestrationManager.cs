using FightShipArena.Assets.Scripts.Managers.OrchestrationManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// Implementation of an Orchestration manager.
    /// Coordinates a sequence of spawns of enemies in spawn points.
    /// The OrchestrationManager contains a list of <see cref="Wave"/>, executed in sequence.
    /// </summary>
    public class OrchestrationManager : 
        MyMonoBehaviour, 
        IOrchestrationManager
    {
        /// <summary>
        /// Collection of waves of enemies to spawn
        /// </summary>
        public Wave[] Waves;

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken RunCancellationToken;

        /// <inheritdoc/>
        public event Action<int> SendScore;

        /// <summary>
        /// Delay between two consecutive waves
        /// </summary>
        public float DelayBetweenWaves = 1.0f;

        /// <summary>
        /// Delay before starting the next wave
        /// </summary>
        public float DelayBeforeStart = 1.0f;

        /// <summary>
        /// Delay after the previous wave is done
        /// </summary>
        public float DelayAfterEnd = 1.0f;

        /// <summary>
        /// Status of the Orchestration
        /// </summary>
        public StatusEnum Status { get; private set; } = StatusEnum.NotStarted;

        public Messenger Messenger { get; private set; }

        /// <inheritdoc/>
        public void Run()
        {
            RunCancellationToken = new CancellationToken();
            StartCoroutine(CoRun(RunCancellationToken));
        }

        void Start()
        {
            Messenger = GameObject.FindObjectOfType<Messenger>();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            RunCancellationToken.Cancel = true; ;
        }

        /// <summary>
        /// CoRoutine that manages the execution
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IEnumerator CoRun(CancellationToken cancellationToken)
        {
            (Messenger as IOrchestrationEventsMessenger).OrchestrationStarted.Invoke(this, null);

            Status = StatusEnum.Running;

            yield return new WaitForSeconds(DelayBeforeStart);
            foreach(var wave in Waves)
            {
                if(cancellationToken.Cancel == true)
                {
                    (Messenger as IOrchestrationEventsMessenger).OrchestrationCancelled.Invoke(this, null);
                    yield break;
                }
                wave.SendScore += Wave_SendScore;
                wave.Run(this, cancellationToken);

                yield return new WaitUntil(() => wave.Status == StatusEnum.Done);

                yield return new WaitForSeconds(DelayBetweenWaves);
            }

            yield return new WaitForSeconds(DelayAfterEnd);

            Status = StatusEnum.Done;

            (Messenger as IOrchestrationEventsMessenger).OrchestrationComplete.Invoke(this, null);
        }

        /// <summary>
        /// EventHandler for the event SendScore of a Wave
        /// </summary>
        /// <param name="obj"></param>
        private void Wave_SendScore(int obj)
        {
            this.SendScore?.Invoke(obj);
        }

        void LevelGameOver(object publisher, string target)
        {
            Stop();
        }

        void LevelGameStarted(object publisher, string target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Simplified version of a CancellationToken.
        /// Used to Cancel an executing Orchestration
        /// </summary>
        public class CancellationToken
        {
            public bool Cancel = false;
        }

        /// <summary>
        /// Status of an orchestration
        /// </summary>
        public enum StatusEnum
        {
            NotStarted,
            Running,
            Done
        }
    }
}
