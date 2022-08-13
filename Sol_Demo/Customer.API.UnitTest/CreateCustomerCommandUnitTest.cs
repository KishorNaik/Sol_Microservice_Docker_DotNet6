using Customer.API.Applications.Features.Command;
using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DataService.Command;
using Framework.Model.Response;
using MediatR;

namespace Customer.API.UnitTest
{
    public class CreateCustomerCommandUnitTest
    {
        private readonly Mock<IMapper> mapperMock = null;
        private readonly Mock<IMediator> mediatorMock = null;
        private readonly Mock<IHashPasswordRule> hashPasswordRuleMock = null;

        private IRequestHandler<CreateCustomerCommand, Results<bool>> createCustomerCommandHandler = null;

        public CreateCustomerCommandUnitTest()
        {
            mapperMock = new Mock<IMapper>();
            mediatorMock = new Mock<IMediator>();
            hashPasswordRuleMock = new Mock<IHashPasswordRule>();

            createCustomerCommandHandler = new CreateCustomerCommandHandler(mediatorMock?.Object!, mapperMock?.Object!, hashPasswordRuleMock?.Object!);
        }

        private CreateCustomerCommand Data => new CreateCustomerCommand
        {
            FullName = "Kishor Naik",
            EmailId = "kishor@gmail.com",
            Password = "pass@123",
            MobileNo = "1111111111"
        };

        [Fact]
        public async void Create_Customer_Success()
        {
            hashPasswordRuleMock
                .Setup((e) => e.CreatePasswordAsync(It.IsAny<string>()))
                .ReturnsAsync((salt: "6lM8YGNBzM2bE6UJgdmWZC5bBtk76SBEZvycl63UWzc=", hash: "+ubvJ+FqXWrKYh0Ne0/pk1iXiPsJK0wiUWnPbgjqVnc="));

            mapperMock
                .Setup((e) => e.Map<CreateCustomerDataServiceCommand>(It.IsAny<CreateCustomerCommand>()))
                .Returns(new CreateCustomerDataServiceCommand());

            mediatorMock
                .Setup((e) => e.Send<bool>(It.IsAny<CreateCustomerDataServiceCommand>(), new CancellationToken()))
                .ReturnsAsync(true);

            Results<bool> results = await createCustomerCommandHandler.Handle(Data, new CancellationToken());

            Assert.True(results.Success);
        }

        [Fact]
        public async void Create_Customer_HasPassword_Failed()
        {
            hashPasswordRuleMock
               .Setup((e) => e.CreatePasswordAsync(It.IsAny<string>()))
               .Throws<Exception>();

            mapperMock
                .Setup((e) => e.Map<CreateCustomerDataServiceCommand>(It.IsAny<CreateCustomerCommand>()))
                .Returns(new CreateCustomerDataServiceCommand());

            mediatorMock
                .Setup((e) => e.Send<bool>(It.IsAny<CreateCustomerDataServiceCommand>(), new CancellationToken()))
                .ReturnsAsync(true);

            Results<bool> results;

            results = await createCustomerCommandHandler.Handle(Data, new CancellationToken());
            if (results.Success == false)
            {
                Assert.False(results.Success);
            }
        }

        [Fact]
        public async void Create_Customer_DataService_Failed()
        {
            hashPasswordRuleMock
               .Setup((e) => e.CreatePasswordAsync(It.IsAny<string>()))
               .ReturnsAsync((salt: "6lM8YGNBzM2bE6UJgdmWZC5bBtk76SBEZvycl63UWzc=", hash: "+ubvJ+FqXWrKYh0Ne0/pk1iXiPsJK0wiUWnPbgjqVnc="));

            mapperMock
                .Setup((e) => e.Map<CreateCustomerDataServiceCommand>(It.IsAny<CreateCustomerCommand>()))
                .Returns(new CreateCustomerDataServiceCommand());

            mediatorMock
                .Setup((e) => e.Send<bool>(It.IsAny<CreateCustomerDataServiceCommand>(), new CancellationToken()))
                .Throws<Exception>();

            Results<bool> results;

            results = await createCustomerCommandHandler.Handle(Data, new CancellationToken());
            if (results.Success == false)
            {
                Assert.False(results.Success);
            }
        }
    }
}