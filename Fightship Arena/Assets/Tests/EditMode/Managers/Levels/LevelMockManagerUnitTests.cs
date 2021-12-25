﻿using FightShipArena.Assets.Scripts.Managers.Levels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightshipArena.Assets.Tests.EditMode.Managers.Levels
{
    public class LevelMockManagerUnitTests
    {
        [Test]
        public void OnAwake_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<ILevelMockManagerCore>();
            var core = coreMock.Object;

            var gameObject = new GameObject("LevelManager");
            var levelManager = gameObject.AddComponent<LevelMockManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnAwake();

            //assert
            coreMock.Verify(x => x.OnAwake(), Times.Once);
        }

        [Test]
        public void OnStart_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<ILevelMockManagerCore>();
            var core = coreMock.Object;

            var gameObject = new GameObject("LevelManager");
            var levelManager = gameObject.AddComponent<LevelMockManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnStart();

            //assert
            coreMock.Verify(x=>x.OnStart(), Times.Once);
        }

        [Test]
        public void Move_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<ILevelMockManagerCore>();
            var core = coreMock.Object;

            var gameObject = new GameObject("LevelManager");
            var levelManager = gameObject.AddComponent<LevelMockManagerMock>();
            levelManager.SetCore(core);

            var context = new InputAction.CallbackContext();
            //act
            levelManager.Move(context);

            //assert
            coreMock.Verify(x => x.Move(context), Times.Once);
        }

        [Test]
        public void DisablePlayerInput_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<ILevelMockManagerCore>();
            var core = coreMock.Object;

            var gameObject = new GameObject("LevelManager");
            var levelManager = gameObject.AddComponent<LevelMockManagerMock>();

            levelManager.SetCore(core);

            //act
            levelManager.DisablePlayerInput();

            //assert
            coreMock.Verify(x => x.DisablePlayerInput(), Times.Once);
        }

        [Test]
        public void EnablePlayerInput_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<ILevelMockManagerCore>();
            var core = coreMock.Object;

            var gameObject = new GameObject("LevelManager");
            var levelManager = gameObject.AddComponent<LevelMockManagerMock>();

            levelManager.SetCore(core);

            //act
            levelManager.EnablePlayerInput();

            //assert
            coreMock.Verify(x => x.EnablePlayerInput(), Times.Once);
        }

    }
    public class LevelMockManagerMock : LevelMockManager
    {
        public void SetCore(ILevelMockManagerCore core)
        {
            this.Core = core;
        }
    }

}
