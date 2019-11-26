using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.DTOs;
using EmailManaget.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.AttachmentsServiceTests
{
    [TestClass]
    public class GetEmailAttachmentsByEmailIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnAttchmentsType_Successfully()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAttchmentsType_Successfully));

            var testAttachmentId = "Attachment";

            var newAttachment = new EmailAttachments() { Id = testAttachmentId };

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.EmailAttachments.AddAsync(newAttachment);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new AttachmentsService(assertContext);
                var result = await sut.GetEmailAttachmentsByEmailIdAsync(testAttachmentId);

                Assert.IsInstanceOfType(result, typeof(ICollection<EmailAttachmentsDTO>));
            }
        }

        [TestMethod]
        public async Task ReturnAttachments_WhenValuIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnAttachments_WhenValuIsNull));

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new AttachmentsService(assertContext);
                var result = await sut.GetEmailAttachmentsByEmailIdAsync("test");

                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
