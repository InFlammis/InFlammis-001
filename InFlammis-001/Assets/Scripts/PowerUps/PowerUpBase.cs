﻿using FightShipArena.Assets.Scripts.MessageBroker;
using System;
using System.Collections;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.PowerUps
{
    /// <summary>
    /// Abstract class for a generic power-up
    /// </summary>
    public abstract class PowerUpBase : MyMonoBehaviour
    {
        /// <summary>
        /// Power-up type
        /// </summary>
        public PowerUpType PowerUpType;

        /// <summary>
        /// Power-up initial settings
        /// </summary>
        public PowerUpSettings InitSettings;

        [SerializeField] Messenger _Messenger;
        public IMessenger Messenger => _Messenger;

        void Awake()
        {
            if (InitSettings == null)
            {
                throw new NullReferenceException($"InitSettings cannot be null for Power-up {this.GetType().Name}");
            }

            if (InitSettings.PowerUpType != PowerUpType)
            {
                throw new Exception(
                    $"Power-up Settings of type {InitSettings.PowerUpType.ToString()} not compatible with Power-up of type {PowerUpType}");
            }

            StartCoroutine(FloatAround());
            StartCoroutine(StartCountdown());
        }

        private void Start()
        {
            _Messenger = GameObject.FindObjectOfType<Messenger>();
        }

        /// <summary>
        /// Countdown before the Power-up destruction
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartCountdown()
        {
            yield return new WaitForSeconds(InitSettings.Duration);

            GameObject.Destroy(this.gameObject);
        }

        /// <summary>
        /// Control how the power-up moves around during its lifetime.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FloatAround()
        {
            var scaleIn = 5;
            var scaleOut = 0.01f;
            var twoPi = 2 * Mathf.PI;
            var rate = 0.05f;

            var seed = UnityEngine.Random.value * twoPi;
            while (true)
            {
                var angle = seed + twoPi * Mathf.PerlinNoise((transform.position.x) * scaleIn, (transform.position.y) * scaleIn);
                var x = Mathf.Cos(angle) * scaleOut;
                var y = Mathf.Sin(angle) * scaleOut;

                var variation = new Vector3(x, y, 0);
                transform.position += variation;

                yield return new WaitForSeconds(rate);
            }
        }
    }
}
