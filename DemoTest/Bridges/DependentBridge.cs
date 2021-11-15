namespace TestProject1.Bridges
{
    public class DependentBridge
    {
        private readonly Bridge _bridge;

        private readonly SomeOtherBridge _someOtherBridge;
        
        public DependentBridge(Bridge bridge, SomeOtherBridge someOtherBridge)
        {
            _bridge = bridge;
            _someOtherBridge = someOtherBridge;
        }

        public string GetBridgeName() => _bridge.GetName();
    }
}