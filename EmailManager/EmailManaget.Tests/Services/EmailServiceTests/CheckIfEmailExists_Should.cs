using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.Providers;
using EmailManaget.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.EmailServiceTests
{
    [TestClass]
    public class CheckIfEmailExists_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEmail_WhenExists()
        {
            var testId = "TestId";
            var options = TestUtils.GetOptions(nameof(ReturnCorrectEmail_WhenExists));
            var email = new ClientEmail() { OriginalMailId = testId };

            var mockEmailFactory = new Mock<IEmailFactory>().Object;
            var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            var mockEncryptHelper = new Mock<EncryptingHelper>().Object;

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Emails.AddAsync(email);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

                var result = await sut.CheckIfEmailExists(testId);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public async Task ReturnEmails_WhenExistsIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnEmails_WhenExistsIsNull));

            var mockEmailFactory = new Mock<IEmailFactory>().Object;
            var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            var mockEncryptHelper = new Mock<EncryptingHelper>().Object;
            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

                var result = await sut.CheckIfEmailExists("TestId");

                Assert.AreEqual(false, result);
            }
        }
    }
}
