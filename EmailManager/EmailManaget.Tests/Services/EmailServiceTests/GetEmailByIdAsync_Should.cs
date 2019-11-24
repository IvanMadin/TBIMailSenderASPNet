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
    public class GetEmailByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEmailById_WhenPassedValueIsCorrect()
        {
            var testBody = "TestBody";
            var options = TestUtils.GetOptions(nameof(ReturnCorrectEmailById_WhenPassedValueIsCorrect));
            var email = new ClientEmail() { Body = testBody };

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

                var result = await sut.GetEmailByIdAsync(email.Id);
                Assert.AreEqual(email.Id, result.Id);
            }
        }

        [TestMethod]
        public async Task ReturnEmailIdNull_WhenPassedValueNotMatch()
        {
            //var testBody = "TestBody";
            //var options = TestUtils.GetOptions(nameof(ReturnEmailIdNull_WhenPassedValueNotMatch));
            //var newEmail = new ClientEmail() { Body = testBody };

            //var mockEmailFactory = new Mock<IEmailFactory>().Object;
            //var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            //var mockEncryptHelper = new Mock<EncryptingHelper>().Object;

            //using (var arrangeContext = new EmailManagerDbContext(options))
            //{
            //    await arrangeContext.Emails.AddAsync(newEmail);
            //    await arrangeContext.SaveChangesAsync();
            //}

            //using (var assertContext = new EmailManagerDbContext(options))
            //{
            //    var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

            //    // DECRYPTING ??
            //    var result = await sut.GetEmailByIdAsync("asd");
            //    Assert.IsNull(result);
            //}
        }
    }
}
