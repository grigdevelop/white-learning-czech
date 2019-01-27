using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalWebsite.Domain.Core.Data;
using PersonalWebsite.Tests.Lib;
using WhileLearningCzech.Domain.Services.Users;

namespace PersonalWebsite.Tests.Services
{
    public class UserServiceTests : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            
            CleanDatabase();
        }

        [TestMethod]
        public async Task ShouldGetUser()
        {
            // mock data
            AddEntityItem(new User{PasswordHash = "password", Username = "greg"});

            IUserService userService = GetService<IUserService>();
            var user = await userService.GetUser("greg", "password");       
            
            Assert.IsNotNull(user);
        }
    }
}
