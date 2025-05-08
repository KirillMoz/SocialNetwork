using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class FriendServiceTest
    {
        private FriendService _friendService;

        [SetUp]
        public void SetUp()
        {
            _friendService = new FriendService();
        }

        [Test]
        public void AddFriend()
        {
            FriendAddData friendAddData = new FriendAddData();
            friendAddData.FriendEmail = "go@google.ru";
            friendAddData.UserEmail = "go@google1.ru";
            _friendService.AddFriend(friendAddData);

            Assert.That(friendAddData.FriendEmail, Is.EqualTo("test@Yandex.ru"));
        }
    }
}
