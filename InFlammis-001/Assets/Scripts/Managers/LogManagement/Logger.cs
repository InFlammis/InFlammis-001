using FightShipArena.Assets.Scripts.MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Managers.LogManagement
{
    
    public interface ILogManager : 
        ILevelEventsSubscriber,
        IOrchestrationEventsSubscriber,
        IEnemyEventsSubscriber
    {
    }

    public enum Level
    {
        Assert,
        Debug,
        Information,
        Event,
        Warning,
        Exception,
        Error,
        Panic
    }

    public class Logger :
        MonoBehaviour,
        ILogManager
    {
        [SerializeField] private Messenger _messenger;
        [SerializeField] private bool _logEnabled;
        [SerializeField] private Level _filterLevel;

        public Messenger Messenger => _messenger;
        public bool logEnabled => _logEnabled;
        public Level filterLevel => _filterLevel;

        void Start()
        {

        }

        public bool IsLogTypeAllowed(Level logType)
        {
            return logType >= filterLevel;
        }

        public void Log(Level level, object message)
        {
            if (logEnabled && IsLogTypeAllowed(level))
            {
                Debug.Log(message);
            }
        }

        public void Log(Level level, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(level))
            {
                Debug.Log(message, context);
            }
        }

        public void LogError(string tag, object message)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Error))
            {
                Debug.LogError(message);
            }
        }

        public void LogError(string tag, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Error))
            {
                Debug.LogError(message, context);
            }
        }

        public void LogException(Exception exception)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Exception))
            {
                Debug.LogException(exception);
            }
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Exception))
            {
                Debug.LogException(exception, context);
            }
        }

        public void LogWarning(string tag, object message)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Warning))
            {
                Debug.LogWarning(message);
            }
        }

        public void LogWarning(string tag, object message, UnityEngine.Object context)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Warning))
            {
                Debug.LogWarning(message, context);
            }
        }

        void ILevelEventsSubscriber.GameOver(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "GameOver");
        }

        public void LogEvent(string publisher, string target, string eventName)
        {
            if (logEnabled && IsLogTypeAllowed(Level.Event))
            {
                Debug.Log($"Pub: {publisher} - Event: {eventName} - Target: {target}");
            }

        }

        void ILevelEventsSubscriber.GameStarted(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "GameStarted");
        }

        void IEnemyEventsSubscriber.HasDied(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "HasDied");
        }

        public void PlayerHasDied(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "HasDied");
        }

        public void PlayerHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            LogEvent(publisher.GetType().Name, target, "HealthLevelChanged");
        }

        void IOrchestrationEventsSubscriber.OrchestrationCancelled(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationCancelled");
        }

        void IOrchestrationEventsSubscriber.OrchestrationComplete(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationComplete");
        }

        void IOrchestrationEventsSubscriber.OrchestrationStarted(object publisher, string target)
        {
            LogEvent(publisher.GetType().Name, target, "OrchestrationStarted");
        }

        public void PlayerScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
            LogEvent(publisher.GetType().Name, target, "ScoreMultiplierCollected");
        }

    }
}
