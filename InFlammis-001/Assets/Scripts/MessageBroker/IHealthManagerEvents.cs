using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{

    /// <summary>
    /// Interface required for a HealthManager Events Publisher.
    /// </summary>
    public interface IHealthManagerEventsPublisher : IEventPublisher
    {
        /// <summary>
        /// Publish an event of type <see cref="HasDied"/>
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void PublishHasDied(object publisher, string target);

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
    /// Interface required by a HealthManager Event Subscriber, in order to subscribe to messages published by a <see cref="IHealthManagerEventsPublisher"/>
    /// </summary>
    public interface IHealthManagerEventsSubscriber : IEventSubscriber
    {
        /// <summary>
        /// Event Handler for an event of type <see cref="HasDied"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        void HasDied(object publisher, string target);

        /// <summary>
        /// Event Handler for an event of type <see cref="HealthLevelChanged"/>.
        /// </summary>
        /// <param name="publisher">Publisher of the message.</param>
        /// <param name="target">Target of the message.</param>
        /// <param name="healthLevel">New health level.</param>
        /// <param name="maxHealthLevel">Maximum health level.</param>
        void HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel);

        //public virtual void Subscribe()
        //{
        //    Messenger.HasDied.AddListener(this.HasDied);
        //    Messenger.HealthLevelChanged.AddListener(this.HealthLevelChanged);
        //}

        //public virtual void Unsubscribe()
        //{
        //    Messenger.HasDied.RemoveListener(this.HasDied);
        //    Messenger.HealthLevelChanged.RemoveListener(this.HealthLevelChanged);
        //}


    }

    /// <summary>
    /// Interface required by the Message Broker to support messages published by a <see cref="IHealthManagerEventsPublisher"/>
    /// </summary>
    public interface IHealthManagerEventsMessenger : IEventMessenger
    {
        /// <summary>
        /// Returns a reference to a delegate of type <see cref="HasDied"/>, to subscribe to.
        /// </summary>
        HasDied HasDied { get; }

        /// <summary>
        /// Returns a reference to a delegate of type <see cref="ReceivedDamage"/>, to subscribe to.
        /// </summary>
        HealthLevelChanged HealthLevelChanged { get; }
    }
}
