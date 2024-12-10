using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSystemApp;
using UserManagementApp;


namespace TestJednostkowy
{

    public class JobSystemLibraryTest
    {
        [Fact]
        public void IsAdmin_ShouldReturnTrueIfUserIsAdmin()
        {
            UserManager userManager = new UserManager();
            var user = new User
            {
                Id = 999,
                Username = "Test",
                Password = "Test",
                Role = "admin"
            };

            var result = userManager.IsAdmin(user);

            Assert.True(result);
        }
    }
}
