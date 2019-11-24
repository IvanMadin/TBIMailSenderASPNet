using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManaget.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.ClientServiceTests
{
    [TestClass]
    public class GetClientDataByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectClientDataById_WhenPassedValueIsCorrect()
        {
            var testFirstName = "TestName";
            var options = TestUtils.GetOptions(nameof(ReturnCorrectClientDataById_WhenPassedValueIsCorrect));
            var clientData = new ClientData() { FirstName = testFirstName };

            var mockValidation = new Mock<IValidation>().Object;

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.ClientDatas.AddAsync(clientData);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new ClientService(assertContext, mockValidation);

                var result = await sut.GetClientDataByIdAsync(clientData.Id);
                Assert.AreEqual(clientData.Id, result.Id);
            }
        }

        [TestMethod]
        public async Task ReturnClientDataNull_WhenPassedValueNotMatch()
        {
            var testFirstName = "TestName";
            var options = TestUtils.GetOptions(nameof(ReturnClientDataNull_WhenPassedValueNotMatch));
            var clientData = new ClientData() { FirstName = testFirstName };

            var mockValidation = new Mock<IValidation>().Object;

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.ClientDatas.AddAsync(clientData);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new ClientService(assertContext, mockValidation);
              
                var result = await sut.GetClientDataByIdAsync("invalid");
                Assert.IsNull(result);
            }
        }
    }
}
