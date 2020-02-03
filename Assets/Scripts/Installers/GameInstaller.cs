using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UserInputManager>().AsSingle().NonLazy();
        Container.Bind<CollectiblesMessage>().AsSingle().NonLazy();
        Container.Bind<PlayerMovementHandler>().AsSingle().NonLazy();
        Container.Bind<PlayerMouseMovementHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerHealthHandler>().AsSingle().NonLazy();
        Container.Bind<PlayerCollectibleHandler>().AsSingle().NonLazy();
        Container.Bind<PlayerMountHandler>().AsSingle().NonLazy();
        Container.Bind<InventoryHandler>().AsSingle().NonLazy();
        Container.Bind<ToastMessage>().AsSingle().NonLazy();
        Container.Bind<HoverCraftMovementHandler>().AsSingle().NonLazy();
    }
}