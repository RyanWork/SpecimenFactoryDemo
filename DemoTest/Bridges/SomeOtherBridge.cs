namespace TestProject1.Bridges
{
    public class SomeOtherBridge
    {
        private readonly Bridge _bridge;

        public SomeOtherBridge()
        {
        }
        
        public SomeOtherBridge(Bridge bridge)
        {
            _bridge = bridge;
        }
    }
}