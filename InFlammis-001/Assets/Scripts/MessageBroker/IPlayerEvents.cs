using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    [Serializable] public class ScoreMultiplierCollected : UnityEvent<object, string, int> { }

    /// <summary>
    /// Interface required for a Player Events Publisher.
    /// </summary>
    public interface IPlayerEventsPublisher : IEventPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);

        /// <summary>
        /// Publish an event of type <see cref="ScoreMultiplierCollected"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="scoreMultiplier">Damage received.</param>
        void PublishScoreMultiplierCollected(object publisher, string target, int scoreMultiplier);

        /// <summary>
        /// Publish an event of type <see cref="HealthLevelChanged"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="healthLevel">The new health level.</param>
        /// <param name="maxHealthLevel">Maximum health level.</param>
        void PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel);

    }

    /// <summary>
    /// Interface required by a Player Event Subscriber, in order to subscribe to messages published by a <see cref="IPlayerEventsPublisher"/>
    /// </summary>
    public interface IPlayerEventsSubscriber : IEventSubscriber
    {
        /// <summary>
        /// Event Handler for an event of type <see cref="HasDied"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void HasDied(object publisher, string target);

        ///// <summary>
        ///// Event Handler for an event of type <see cref="ReceivedDamage"/>.
        ///// </summary>
        ///// <param name="publisher">Publisher of the message.</param>
        ///// <param name="target">Target of the message.</param>
        ///// <param name="damage">Damage received.</param>
        //void ReceivedDamage(object publisher, string target, int damage);

        /// <summary>
        /// Event Handler for an event of type <see cref="ScoreMultiplierCollected"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="damage">Damage received.</param>
        void ScoreMultiplierCollected(object publisher, string target, int scoreMultiplier);

        /// <summary>
        /// Event Handler for an event of type <see cref="HealthLevelChanged"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="healthLevel">New health level.</param>
        /// <param name="maxHealthLevel">Maximum health level.</param>
        void HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel);

    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IPlayerEventsPublisher"/>
    /// </summary>
    public interface IPlayerEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }

        ///// <summary>
        ///// Returns a reference to a delegate of type <see cref="ReceivedDamage"/>, to subscribe to.
        ///// </summary>
        //DamageReceived ReceivedDamage { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="ScoreMultiplierCollected"/>, to subscribe to.
        /// </summary>
        ScoreMultiplierCollected ScoreMultiplierCollected { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="ReceivedDamage"/>, to subscribe to.
        /// </summary>
        HealthLevelChanged HealthLevelChanged { get; }
    }
}
