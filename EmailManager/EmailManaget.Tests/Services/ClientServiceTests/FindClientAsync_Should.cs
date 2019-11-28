using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManaget.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.ClientServiceTests
{
    [TestClass]
    public class FindClientAsync_Should
    {
        [TestMethod]
        public async Task FindClient_Successfully()
        {
            var firstName = "FirstName";
            var lastName = "LastName";
            var egn = "1234567899";

            var options = TestUtils.GetOptions(nameof(FindClient_Successfully));
            var clientData = new ClientData() { FirstName = firstName, 
                LastName= lastName, 
                EGN= egn
            };

            var mockValidation = new Mock<IValidation>().Object;
            var mockEncryption = new Mock<IEncryptingHelper>();

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.ClientDatas.AddAsync(clientData);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new ClientService(assertContext, mockValidation, mockEncryption.Object);
                mockEncryption.Setup(e => e.Encrypt(lastName)).Returns(lastName);
                mockEncryption.Setup(e => e.Encrypt(egn)).Returns(egn);
                mockEncryption.Setup(e => e.Decrypt(lastName)).Returns(lastName);
                mockEncryption.Setup(e => e.Decrypt(egn)).Returns(egn);
                var data = await sut.FindClientAsync(firstName, lastName, egn);

                Assert.AreEqual(clientData.Id, data.Id);
                Assert.AreEqual(firstName, data.FirstName);
                Assert.AreEqual(lastName, data.LastName);
            }
        }

        [TestMethod]
        public async Task ReturnClientDataType_Successfully()
        {
            var firstName = "FirstName";
            var lastName = "LastName";
            var egn = "1234567899";

            var options = TestUtils.GetOptions(nameof(FindClient_Successfully));
            var clientData = new ClientData() { FirstName = firstName, LastName = lastName, EGN = egn };

            var mockValidation = new Mock<IValidation>().Object;
            var mockEncryption = new Mock<IEncryptingHelper>();

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.ClientDatas.AddAsync(clientData);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new ClientService(assertContext, mockValidation, mockEncryption.Object);
                mockEncryption.Setup(e => e.Encrypt(lastName)).Returns(lastName);
                mockEncryption.Setup(e => e.Encrypt(egn)).Returns(egn);
                mockEncryption.Setup(e => e.Decrypt(lastName)).Returns(lastName);
                mockEncryption.Setup(e => e.Decrypt(egn)).Returns(egn);
                var data = await sut.FindClientAsync(firstName, lastName, egn);

                Assert.IsInstanceOfType(data, typeof(ClientDataDTO));
            }
        }

       
        [TestMethod]
        public async Task Return_Null()
        {
            var options = TestUtils.GetOptions(nameof(Return_Null));

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var mockValidation = new Mock<IValidation>().Object;
                var mockEncryption = new Mock<IEncryptingHelper>().Object;
                var sut = new ClientService(assertContext, mockValidation, mockEncryption);

                var data = await sut.FindClientAsync("FirstName", "LastName", "1231231231");

                Assert.IsNull(data);
            }
        }
    }
}
