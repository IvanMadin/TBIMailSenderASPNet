using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.DTOs;
using EmailManager.Service.Factories;
using EmailManager.Service.Providers;
using EmailManaget.Tests.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.EmailServiceTests
{
    [TestClass]
    public class GetAllEmailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnEmailsType_Successfully()
        {
            var testBody = "TestBody";
            var options = TestUtils.GetOptions(nameof(ReturnEmailsType_Successfully));
            var firstEmail = new ClientEmail() { Body = testBody };

            var mockEmailFactory = new Mock<IEmailFactory>().Object;
            var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            var mockEncryptHelper = new Mock<EncryptingHelper>().Object;

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Emails.AddAsync(firstEmail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

                var result = await sut.GetAllEmailsAsync();

                Assert.IsInstanceOfType(result, typeof(ICollection<EmailDTO>));
            }
        }

        [TestMethod]
        public async Task ReturnCollectionOfEmail()
        {
            var testBody = "TestBody";
            var options = TestUtils.GetOptions(nameof(ReturnCollectionOfEmail));
            var firstEmail = new ClientEmail() { Body = testBody };
            var secondEmail = new ClientEmail() { Body = "SecondBodyName" };
            var thirdEmail = new ClientEmail() { Body = "ThirdBodyName" };

            var mockEmailFactory = new Mock<IEmailFactory>().Object;
            var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            var mockEncryptHelper = new Mock<EncryptingHelper>().Object;


            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Emails.AddAsync(firstEmail);
                await arrangeContext.Emails.AddAsync(secondEmail);
                await arrangeContext.Emails.AddAsync(thirdEmail);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

                var result = await sut.GetAllEmailsAsync();

                Assert.AreEqual(3, result.Count);
            }
        }

        [TestMethod]
        public async Task ReturnEmails_WhenValuIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnEmails_WhenValuIsNull));

            var mockEmailFactory = new Mock<IEmailFactory>().Object;
            var mockEmailStatus = new Mock<IEmailStatusService>().Object;
            var mockEncryptHelper = new Mock<EncryptingHelper>().Object;
            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailFactory, mockEncryptHelper, mockEmailStatus);

                var result = await sut.GetAllEmailsAsync();

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
