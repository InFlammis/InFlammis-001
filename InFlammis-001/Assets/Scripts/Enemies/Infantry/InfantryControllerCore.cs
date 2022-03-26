using System;
using System.Collections;
using System.Linq;
using FightShipArena.Assets.Scripts.Enemies.Infantry.StateMachine;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.Player;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightShipArena.Assets.Scripts.Enemies.Infantry
{
    /// <summary>
    /// Specialization of a IEnemyControllerCore for an Infantry enemy type
    /// </summary>
    public class InfantryControllerCore : IEnemyControllerCore
    {
        /// <inheritdoc/>
        public event Action<IEnemyControllerCore> HasDied;

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
            //State = EnemyState.Idle;
            Parent = parent;
            Transform = parent.GameObject.transform;
            Rigidbody = parent.GameObject.GetComponent<Rigidbody2D>();
            HealthManager = healthManager;
            HealthManager.HasDied += HealthManager_HasDied;
            HealthManager.HealthLevelChanged += HealthManager_HealthLevelChanged;

            InitSettings = settings;
            Weapons = parent.Weapons.Select(x => x.GetComponent<WeaponBase>()).ToArray();
            CurrentWeapon = Weapons[0];

            _stateFactory = new StateFactory(this);
            CurrentState = _stateFactory.IdleState;
        }

        /// <summary>
        /// EventHandler for the HasDied event of the player
        /// </summary>
        private void Player_HasDied()
        {
            ChangeState(_stateFactory.IdleState);
        }

        /// <summary>
        /// EventHandler for the HealthLevelChanged event of the HealthManager
        /// </summary>
        /// <param name="value">The new health level</param>
        /// <param name="maxValue">The maximum health level</param>
        private void HealthManager_HealthLevelChanged(int value, int maxValue) { }

        /// <summary>
        /// EventHandler for the HasDied event of the HealthManager
        /// </summary>
        private void HealthManager_HasDied()
        {
            ChangeState(_stateFactory.IdleState);
            HasDied?.Invoke(this);
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
                PlayerControllerCore.HealthManager.HasDied += Player_HasDied;
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
    }
}
