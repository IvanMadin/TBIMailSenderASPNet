using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManaget.Tests.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var testUsername = "TestName";

            var newUser = new User() { UserName = testUsername };

            var options = TestUtils.GetOptions(nameof(ReturnCorrectUser_WhenPassedValueIsCorrect));

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Users.AddAsync(newUser);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var userStoreMock = new Mock<IUserStore<User>>();

                var mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
                var sut = new UsersService(assertContext, mockUserManager.Object);

                var result = await sut.GetUserByIdAsync(newUser.Id);

                Assert.AreEqual(newUser.Id, result.Id);
            }
        }

        [TestMethod]
        public async Task ReturnNull_WhenPassedValueNotMatch()
        {

            var options = TestUtils.GetOptions(nameof(ReturnNull_WhenPassedValueNotMatch));

            var testUserId = "testUserId";

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var userStoreMock = new Mock<IUserStore<User>>();

                var mockUserManager = new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

                var usersService = new UsersService(assertContext, mockUserManager.Object);

                var sut = await usersService.GetUserByIdAsync(testUserId);

                Assert.IsNull(sut);
            }
        }

        [TestMethod]
        public async Task ReturnRoles_Successfully()
        {
            var options = TestUtils.GetOptions(nameof(ReturnRoles_Successfully));

            var testUserRole = "Manager";
            var testRoleId = "TestRoleId";

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Roles.AddAsync(new IdentityRole { Id = testRoleId, Name = testUserRole });

                await arrangeContext.SaveChangesAsync();
            }

            var userStoreMock = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);



            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new UsersService(assertContext, mockUserManager.Object);

                var roles = await sut.GetRolesAsync();

                Assert.AreEqual(testRoleId, roles.First().Id);
            }
        }

        [TestMethod]
        public async Task ReturnRolesType_Successfully()
        {
            var options = TestUtils.GetOptions(nameof(ReturnRolesType_Successfully));

            var testUserRole = "Manager";
            var testRoleId = "TestRoleId";

            using (var arrangeContext = new EmailManagerDbContext(options))
            {
                await arrangeContext.Roles.AddAsync(new IdentityRole { Id = testRoleId, Name = testUserRole });

                await arrangeContext.SaveChangesAsync();
            }

            var userStoreMock = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);



            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new UsersService(assertContext, mockUserManager.Object);

                var roles = await sut.GetRolesAsync();

                Assert.IsInstanceOfType(roles, typeof(ICollection<RoleDTO>));
            }
        }

        [TestMethod]
        public async Task ReturnRoles_WhenValuIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ReturnRoles_WhenValuIsNull));

            var userStoreMock = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new EmailManagerDbContext(options))
            {
                var sut = new UsersService(assertContext, mockUserManager.Object);

                var roles = await sut.GetRolesAsync();

                Assert.AreEqual(0, roles.Count());
            }
        }

        [TestMethod]
        public async Task ReturnCorrectUser_IsCalledOnce()
        {
            var mockRoles = new Mock<ICollection<RoleDTO>>();

            var usersService = new Mock<IRolesService>();
            usersService.Setup(x => x.GetRolesAsync()).ReturnsAsync(mockRoles.Object);

            var sut = await usersService.Object.GetRolesAsync();

            usersService.Verify(x => x.GetRolesAsync(), Times.Once);
        }
    }
}
