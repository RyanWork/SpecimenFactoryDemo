using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using TestProject1.Bridges;
using Xunit;

namespace TestProject1
{
    public class DependentBridgeTest
    {
        private readonly IFixture _fixture;

        public DependentBridgeTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Customizations.Add(new AutoMockSpecimenFactory());
        }
        
        [Fact]
        public void GetBridge_UsingAutoMockSpecimenFactory_MocksConstructorAndAllowsSetup()
        {
            var mockBridge = _fixture.Create<Mock<Bridge>>();
            var sut = _fixture.Create<DependentBridge>();
            mockBridge.Setup(x => x.GetName())
                .Returns("World!");
            
            var result = sut.GetBridgeName();

            result.Should().Be("World!");
        }
        
        [Fact]
        public void GetBridge_UsingAutoMockSpecimenFactoryAndDuplicateMockBridges_MocksConstructorAndAllowsSetup()
        {
            var mockBridge = _fixture.Create<Mock<Bridge>>();
            _fixture.Create<Mock<Bridge>>();
            var sut = _fixture.Create<DependentBridge>();
            mockBridge.Setup(x => x.GetName())
                .Returns("World!");
            
            var result = sut.GetBridgeName();

            result.Should().Be("World!");
        }
        
        [Fact]
        public void GetBridge_UsingAutoMockSpecimenFactoryAndDuplicateSuts_MocksConstructorAndAllowsSetup()
        {
            var mockBridge = _fixture.Create<Mock<Bridge>>();
            var sut = _fixture.Create<DependentBridge>();
            _fixture.Create<DependentBridge>();
            mockBridge.Setup(x => x.GetName())
                .Returns("World!");
            
            var result = sut.GetBridgeName();

            result.Should().Be("World!");
        }
        
        [Fact]
        public void GetBridge_UsingAutoMockSpecimenFactoryAndDuplicateSutsAndBridges_MocksConstructorAndAllowsSetup()
        {
            var mockBridge = _fixture.Create<Mock<Bridge>>();
            _fixture.Create<Mock<Bridge>>();
            var sut = _fixture.Create<DependentBridge>();
            _fixture.Create<DependentBridge>();
            mockBridge.Setup(x => x.GetName())
                .Returns("World!");
            
            var result = sut.GetBridgeName();

            result.Should().Be("World!");
        }
    }
}