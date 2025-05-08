using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.PLL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
       private UserService _userService;

       [SetUp]
       public void SetUp()
       {
          _userService = new UserService();
       }

       [Test]
       public void FindByEmailUserExists()
       {
          UserRegistrationData registrationData = new UserRegistrationData()
          {
             email = "test@gmail.com",
             firstname = "TestFName",
             lastname = "TestLName",
             password = "TestPwd000",
          };

          _userService.Register(registrationData);
          User user = _userService.FindByEmail("test@gmail.com");

          Assert.NotNull(user);
          Assert.That(user.FirstName, Is.EqualTo("TestFirstname"));
          Assert.That(user.LastName, Is.EqualTo("TestLastname"));
          Assert.That(user.Email, Is.EqualTo("test@Yandex.ru"));
       }

       [Test]
       public void FindByEmail_UserDoesNotExist_ThrowsUserNotFoundException()
       {
          Assert.Throws<UserNotFoundException>(() => _userService.FindByEmail("testtest@yandex.ru"));
       }
    }
}