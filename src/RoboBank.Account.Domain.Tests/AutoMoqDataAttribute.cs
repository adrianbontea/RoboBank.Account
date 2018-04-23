using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace RoboBank.Account.Domain.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : 
            base (() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
