using System;
using System.Collections;
using System.Linq;
using FightShipArena.Assets.Scripts.Enemies.Infantry.StateMachine;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.MessageBroker.Player;
using FightShipArena.Assets.Scripts.Player;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightShipArena.Assets.Scripts.Enemies.Infantry
{
    /// <summary>
    /// Specialization of a IEnemyControllerCore for an Infantry enemy type
    /// </summary>
    public class InfantryControllerCore : 
        IEnemyControllerCore,
        IHealthManagerEventsSubscriber,
        IPlayerEventsSubscriber
    {
        public Messenger Messenger { get; set; }

        private string _Target;

        /// <inheritdoc/>
        public IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <inheritdoc/>
        public IEnemyController Parent { get; protected set; }

        /// <inheritdoc/>
        public Transform Transform { get; protected set; }

        /// <inheritdoc/>
        public Rigidbody2D Rigidbody { get; protected set; }

        /// <inheritdoc/>
        public EnemySettings InitSettings { get; protected set; }

        /// <inheritdoc/>
        public IHealthManager HealthManager { get; }

        /// <summary>
        /// Collection of weapons available for the Enemy
        /// </summary>
        public WeaponBase[] Weapons { get; }

        /// <summary>
        /// Current weapon in use by the enemy
        /// </summary>
        public WeaponBase CurrentWeapon { get; set; }

        /// <summary>
        /// Current state the enemy is in
        /// </summary>
        public IInfantryState CurrentState { get; protected set; }

        /// <summary>
        /// Instance of the state factory
        /// </summary>
        private StateFactory _stateFactory;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="parent">The IEnemyControlle parent</param>
        /// <param name="healthManager">The healthManager instance</param>
        /// <param name="settings">The initial settings</param>
        public InfantryControllerCore(IEnemyController parent, IHealthManager healthManager, EnemySettings settings)
        {
            this.Messenger = GameObject.FindObjectOfType<Messenger>();
            Parent = parent;
            Transform = parent.GameObject.transform;
            Rigidbody = parent.GameObject.GetComponent<Rigidbody2D>();
            _Target = parent.GameObject.GetInstanceID().ToString();

            HealthManager = healthManager;

            SubscribeToHealthManagerEvents();

            SubscribeToPlayerEvents();

            InitSettings = settings;
            Weapons = parent.Weapons.Select(x => x.GetComponent<WeaponBase>()).ToArray();
            CurrentWeapon = Weapons[0];

            _stateFactory = new StateFactory(this);
            CurrentState = _stateFactory.IdleState;
        }

        private void SubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            var subscriber = (this as IHealthManagerEventsSubscriber);
            messenger.HasDied.AddListener(subscriber.HasDied);
            messenger.HealthLevelChanged.AddListener(subscriber.HealthLevelChanged);
        }

        private void UnsubscribeToHealthManagerEvents()
        {
            var messenger = (Messenger as IHealthManagerEventsMessenger);
            var subscriber = (this as IHealthManagerEventsSubscriber);
            messenger.HasDied.RemoveListener(subscriber.HasDied);
            messenger.HealthLevelChanged.RemoveListener(subscriber.HealthLevelChanged);
        }

        private void SubscribeToPlayerEvents()
        {
            var messenger = (Messenger as IPlayerEventsMessenger);
            var subscriber = (this as IPlayerEventsSubscriber);

            messenger.HasDied.AddListener(subscriber.HasDied);
        }

        private void UnsubscribeToPlayerEvents()
        {
            var messenger = (Messenger as IPlayerEventsMessenger);
            var subscriber = (this as IPlayerEventsSubscriber);

            messenger.HasDied.RemoveListener(subscriber.HasDied);
        }

        /// <summary>
        /// Manage collisions with the player
        /// </summary>
        public void HandleCollisionWithPlayer()
        {
            HealthManager.Kill();
        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        public void Move()
        {
            CurrentState.Move();
            CurrentState.Rotate();
        }

        /// <summary>
        /// Invoked on Start by the parent MonoBehaviour
        /// </summary>
        public void OnStart()
        {
            if(PlayerControllerCore != null)
            {
                ChangeState(_stateFactory.SeekState);
            }
            else
            {
                ChangeState(_stateFactory.IdleState);
            }
        }

        /// <summary>
        /// EventHandler for the Change state invokation from the current state
        /// </summary>
        /// <param name="newState">The new state to enable</param>
        protected void ChangeState(IInfantryState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.OnExit();
                CurrentState.ChangeState -= CurrentStateOnChangeState;
            }

            if (CurrentState == newState)
            {
                return;
            }


            CurrentState = newState;
            CurrentState.ChangeState += CurrentStateOnChangeState;
            CurrentState.OnEnter();
        }

        /// <summary>
        /// EventHandler for the Change state invokation from the current state
        /// </summary>
        /// <param name="newState">The new state to enable</param>
        private void CurrentStateOnChangeState(IInfantryState state)
        {
            ChangeState(state);
        }

        void IHealthManagerEventsSubscriber.HasDied(object publisher, string target)
        {
            if (target != _Target)
            {
                return;
            }

            ChangeState(_stateFactory.IdleState);

            UnsubscribeToPlayerEvents();
            UnsubscribeToHealthManagerEvents();

            (Messenger as IEnemyEventsPublisher).PublishHasDied(this.Parent, $"Infantry,{this.Parent.GameObject.GetInstanceID()}");
        }

        void IHealthManagerEventsSubscriber.HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }

        void IPlayerEventsSubscriber.HasDied(object publisher, string target)
        {
            ChangeState(_stateFactory.IdleState);
        }

        void IPlayerEventsSubscriber.ScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
        }

        void IPlayerEventsSubscriber.HealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
        }
    }
}
