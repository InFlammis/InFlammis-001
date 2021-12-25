﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts;
using NUnit.Framework;
using UnityEngine;

namespace FightshipArena.Assets.Tests.EditMode
{
    public class MyMonoBehaviourUnitTests
    {
        [Test]
        public void Check_that_all_MonoBehaviours_are_MyMonoBehaviour_instances()
        {
            var assembly = typeof(MyMonoBehaviour).Assembly;
            var mb = new MonoBehaviour();
            var allTypes = assembly.GetTypes();
            var allTypesInNamespace =
                allTypes.Where(x => 
                !String.IsNullOrEmpty(x.Namespace) 
                && x.Namespace.StartsWith("FightShipArena.Assets.Scripts")
                ).ToArray();
            var monoBehaviourTypes = allTypesInNamespace.Where(x => x.IsSubclassOf(typeof(MonoBehaviour))).ToArray();

            foreach (var type in monoBehaviourTypes)
            {
                Assert.That(type.GetInterfaces(), Does.Contain(typeof(IMyMonoBehaviour)));
            }
        }

    }
}
