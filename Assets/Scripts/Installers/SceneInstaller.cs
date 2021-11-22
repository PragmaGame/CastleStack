using Core;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EntityCollections>().AsSingle();
    }
}