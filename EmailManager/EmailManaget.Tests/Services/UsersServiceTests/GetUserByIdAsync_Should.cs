using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManaget.Tests.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManaget.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class GetUserByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectUser_WhenPassedValueIsCorrect()
        {
            //    var testUsername = "TestName";
            //    var newUser = new User() { UserName = testUsername };
            //    var options = TestUtils.GetOptions(nameof(ReturnCorrectUser_WhenPassedValueIsCorrect));
            //    using (var arrangeContext = new EmailManagerDbContext(options))
            //    {
            //        await arrangeContext.Users.AddAsync(newUser);
            //        await arrangeContext.SaveChangesAsync();
            //    }

            //    using (var assertContext = new EmailManagerDbContext(options))
            //    {
            //        var mockedUserManager = new Mock<UserManager<User>>();
            //        var sut = new UsersService(assertContext, mockedUserManager.Object);

            //        var result = await sut.GetUserByIdAsync(newUser.Id);
            //        Assert.AreEqual(newUser.Id, result.Id);
            //    }
        }

        [TestMethod]
        public async Task ReturnNull_WhenPassedValueNotMatch()
        {
            //var testUsername = "TestName";
            //var newUser = new User() { UserName = testUsername };
            //var options = TestUtils.GetOptions(nameof(ReturnNull_WhenPassedValueNotMatch));

            //using (var arrangeContext = new EmailManagerDbContext(options))
            //{
            //    arrangeContext.Users.Add(newUser);
            //    arrangeContext.SaveChanges();
            //}

            //using (var assertContext = new EmailManagerDbContext(options))
            //{
                ////var mockUserManager = new Mock<UserManager<User>>(
                ////    new Mock<IUserStore<User>>().Object);
                ////mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                ////     .Returns(Task.FromResult(It.IsAny<User>()));

                ////var sut = new UsersService(assertContext, mockUserManager.Object);
            //    var mockedUserManager = new Mock<UserManager<User>>().Object;
            //    var sut = new UsersService(assertContext, mockedUserManager);

            //    var result = await sut.GetUserByIdAsync("invalid");

            //    Assert.IsNull(result);
            //}
        }

        [TestMethod]
        public async Task ReturnRoles_Successfully()
        {
            //var options = TestUtils.GetOptions(nameof(ReturnRoles_Successfully));
            //using (var arrangeContext = new EmailManagerDbContext(options))
            //{
            //    await arrangeContext.Roles.AddAsync(new IdentityRole { Name = "Manager" });

            //    await arrangeContext.SaveChangesAsync();
            //}

            //using (var assertContext = new EmailManagerDbContext(options))
            //{
            //    var sut = new UsersService(assertContext, mockUserManager.Object);

            //    var roles = await sut.GetRolesAsync();

            //    Assert.AreEqual();
            //}
        }
    }
}
