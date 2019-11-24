using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManaget.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.EmailStatusServiceTests
{
    [TestClass]
    public class GetEmailStatusByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEmailStatusById_WhenPassedValueIsCorrect()
        {
            var testStatus = "TestStatus";
            var options = TestUtils.GetOptions(nameof(ReturnCorrectEmailStatusById_WhenPassedValueIsCorrect));
            var status = new StatusEmail() { StatusType = testStatus };

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.StatusEmails.AddAsync(status);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new EmailStatusService(assertContext);

                var result = await sut.GetEmailStatusByIdAsync(status.Id);
                Assert.AreEqual(status.Id, result.Id);
            }
        }
    }
}
