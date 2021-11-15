namespace TestProject1.Bridges
{
    public class DependentBridge
    {
        private readonly Bridge _bridge;

        private readonly SomeOtherBridge _someOtherBridge;

        private readonly IBridge _bridgeWithAnInterface;
        
        public DependentBridge(Bridge bridge, SomeOtherBridge someOtherBridge, IBridge bridgeWithAnInterface)
        {
            _bridge = bridge;
            _someOtherBridge = someOtherBridge;
            _bridgeWithAnInterface = bridgeWithAnInterface;
        }

        public string GetBridgeName() => _bridge.GetName();
    }
}