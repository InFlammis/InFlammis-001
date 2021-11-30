﻿using FightShipArena.Assets.Scripts.Managers.GameManagement;
using FightShipArena.Assets.Scripts.Managers.GameManagement.StateMachine;
using FightShipArena.Assets.Scripts.Managers.Levels;
using Moq;
using NUnit.Framework;
using UnityEngine.SceneManagement;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement.StateMachine
{
    [TestFixture]
    public class PlayUnitTests
    {
        [Test]
        public void OnEnter_calls_LoadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Play>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.OnEnter()).CallBase();

            var state = stateMock.Object;

            //act
            state.OnEnter();

            //assert
            sceneManagerWrapperMock.Verify(x => x.LoadSceneAsync(state._sceneName, LoadSceneMode.Additive), Times.Once);
        }

        [Test]
        public void OnExit_calls_UnloadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Play>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.OnExit()).CallBase();

            var state = stateMock.Object;

            //act
            state.OnExit();

            //assert
            sceneManagerWrapperMock.Verify(x => x.UnloadSceneAsync(state._sceneName), Times.Once);
        }

        [Test]
        public void OnActivate_calls_EnablePlayerInput_on_LevelManager()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var levelManagerMock = new Mock<ILevelMockManager>();
            var levelManager = levelManagerMock.Object;

            var state = new PlayMock(gameManagerCore, sceneManagerWrapper);

            state.SetLevelManager(levelManager);

            //act
            state.OnActivate();

            //assert
            levelManagerMock.Verify(x => x.EnablePlayerInput(), Times.Once);
        }

        [Test]
        public void OnDeactivate_calls_DisablePlayerInput_on_LevelManager()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var levelManagerMock = new Mock<ILevelMockManager>();
            var levelManager = levelManagerMock.Object;

            var state = new PlayMock(gameManagerCore, sceneManagerWrapper);

            state.SetLevelManager(levelManager);

            //act
            state.OnDeactivate();

            //assert
            levelManagerMock.Verify(x => x.DisablePlayerInput(), Times.Once);
        }

        [Test]
        public void PauseResumeGame_raises_ResumeGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new Play(gameManagerCore, sceneManagerWrapper);
            var eventCalled = false;

            state.PauseGameEvent += (sender, args) => { eventCalled = true; };

            //act
            state.PauseResumeGame();

            //assert
            Assert.That(eventCalled, Is.True);
        }



        public class PlayMock : Play
        {
            public PlayMock(
                IGameManager gameManager,
                IUnitySceneManagerWrapper sceneManagerWrapper
            ) : base(gameManager, sceneManagerWrapper) { }

            public void SetLevelManager(ILevelMockManager levelManager)
            {
                this._levelManager = levelManager;
            }
        }
    }
}