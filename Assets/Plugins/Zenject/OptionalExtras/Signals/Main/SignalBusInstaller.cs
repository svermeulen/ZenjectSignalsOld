using System;
using ModestTree;

namespace Zenject
{
    // Note that you only need to install this once
    public class SignalBusInstaller : Installer<SignalBusInstaller>
    {
        public override void InstallBindings()
        {
            Assert.That(!Container.HasBinding<SignalBus>(), "Detected multiple SignalBus bindings.  SignalBusInstaller should only be installed once");

            Container.BindInterfacesAndSelfTo<SignalBus>().AsSingle().CopyIntoAllSubContainers();
            Container.Bind<SignalDeclarationAsyncInitializer>().AsSingle().CopyIntoAllSubContainers().NonLazy();

            Container.BindMemoryPool<SignalSubscription, SignalSubscription.Pool>();

            // Dispose last to ensure that we don't remove SignalSubscription before the user does
            Container.BindLateDisposableExecutionOrder<SignalBus>(-999);
        }
    }
}
