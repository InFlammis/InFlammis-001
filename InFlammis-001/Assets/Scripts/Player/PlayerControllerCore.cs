using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Enemies;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using FightShipArena.Assets.Scripts.MessageBroker;
using FightShipArena.Assets.Scripts.MessageBroker.Events;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Player
{
    public class PlayerControllerCore : IPlayerControllerCore
    {
        /// <inheritdoc/>
        public IPlayerController Parent { get; protected set; }

        /// <inheritdoc/>
        public Transform Transform { get; protected set; }

        /// <inheritdoc/>
        public Rigidbody2D RigidBody { get; protected set; }

        /// <inheritdoc/>
        public PlayerSettings InitSettings { get; set; }

        /// <inheritdoc/>
        public IHealthManager HealthManager { get; }

        /// <inheritdoc/>
        public WeaponBase[] Weapons { get; }

        /// <inheritdoc/>
        public Vector2 PlayerInput { get; set; }

        /// <inheritdoc/>
        public WeaponBase CurrentWeapon { get; set; }

        public Messenger Messenger { get; set; }

        /// <summary>
        /// Create a new instance of the PlayerController Core
        /// </summary>
        /// <param name="parent">Reference to the IPlayerController parent instance</param>
        public PlayerControllerCore(IPlayerController parent)
        {
            Parent = parent;
            Transform = parent.GameObject.transform;
            RigidBody = parent.GameObject.GetComponent<Rigidbody2D>();
            Messenger = GameObject.FindObjectOfType<Messenger>();

            HealthManager = parent.HealthManager;

            InitSettings = parent.InitSettings;
            Weapons = parent.Weapons.Select(x=>x.GetComponent<WeaponBase>()).ToArray();
            CurrentWeapon = Weapons[0];
        }



        /// <inheritdoc/>
        public void SetPlayerInput(Vector2 playerInput)
        {
            PlayerInput = playerInput;
        }

        /// <inheritdoc/>
        public void Move()
        {
            RigidBody.AddForce(PlayerInput * InitSettings.ForceMultiplier, ForceMode2D.Impulse);
            var speed = RigidBody.velocity.magnitude;

            //Limit speed
            if (speed > InitSettings.MaxSpeed)
            {
                RigidBody.velocity = RigidBody.velocity.normalized * InitSettings.MaxSpeed;
            }

            //Stop fightship when input is zero
            if (PlayerInput == Vector2.zero && speed != 0)
            {
                RigidBody.velocity *= InitSettings.Deceleration;
            }
        }

        /// <inheritdoc/>
        public void StartFiring()
        {
            CurrentWeapon.StartFiring();
        }

        /// <inheritdoc/>
        public void StopFiring()
        {
            CurrentWeapon.StopFiring();
        }

        /// <inheritdoc/>
        public void FireAlt()
        {

        }

        /// <inheritdoc/>
        public void OpenSelectionMenu()
        {

        }

        /// <inheritdoc/>
        public void HandleCollisionWithEnemy(IEnemyControllerCore enemyController)
        {
            var damage = enemyController.InitSettings.DamageAppliedOnCollision;
            HealthManager.Damage(damage);
        }

        /// <inheritdoc/>
        public void TurnLeft()
        {
            var rotation = Quaternion.Euler(0, 0, 90);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        /// <inheritdoc/>
        public void TurnRight()
        {
            var rotation = Quaternion.Euler(0, 0, -90);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        /// <inheritdoc/>
        public void TurnUp()
        {
            var rotation = Quaternion.Euler(0, 0, 0);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        /// <inheritdoc/>
        public void TurnDown()
        {
            var rotation = Quaternion.Euler(0, 0, 180);
            ((MonoBehaviour)Parent).StartCoroutine(DoRotatePlayer(rotation));
        }

        /// <summary>
        /// Execute a Rotate action on the player object, spanned on multiple frames
        /// </summary>
        /// <param name="quaternion">Rotation</param>
        /// <returns></returns>
        private IEnumerator DoRotatePlayer(Quaternion quaternion)
        {
            float tolerance = 0.95f;
            float rotationSpeed = 0.1f;

            while ( Mathf.Abs(Quaternion.Dot(Transform.rotation, quaternion) ) < tolerance)
            {
                Transform.rotation = Quaternion.Slerp(Transform.rotation, quaternion, rotationSpeed);
                yield return  null;
            }

            Transform.rotation = quaternion;
        }

        /// <inheritdoc/>
        public void AddMultiplier(int multiplier)
        {
            //ScoreMultiplierCollected?.Invoke(multiplier);
            (Messenger as IPlayerEventsMessenger).ScoreMultiplierCollected?.Invoke(this, null, multiplier);
        }

        /// <summary>
        /// EventHandler invoked when the player collides with a power-up
        /// </summary>
        /// <param name="powerUp">Power-up which the player is colliding with</param>
        public void HandleCollisionWithPowerUp(PowerUps.PowerUpBase powerUp)
        {

        }

    }
}
