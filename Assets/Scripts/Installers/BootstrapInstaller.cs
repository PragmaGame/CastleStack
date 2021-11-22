using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoaderService>().AsSingle().NonLazy();
    }
}
