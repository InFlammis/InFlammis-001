using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    /// <summary>
    /// Interface required for a Enemy Events Publisher.
    /// </summary>
    public interface IEnemyEventsPublisher : IEventPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);
    }

    /// <summary>
    /// Interface required by a Enemy Event Subscriber, in order to subscribe to messages published by a <see cref="IEnemyEventsPublisher"/>
    /// </summary>
    public interface IEnemyEventsSubscriber : IEventSubscriber
    {
        /// <summary>
        /// Event Handler for an event of type <see cref="HasDied"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void HasDied(object publisher, string target);
    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IEnemyEventsPublisher"/>
    /// </summary>
    public interface IEnemyEventsMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }
    }
}
