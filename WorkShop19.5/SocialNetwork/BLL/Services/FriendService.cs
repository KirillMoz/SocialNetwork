using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        IFriendRepository friendRepository;
        IUserRepository userRepository;

        public FriendService()
        {
            friendRepository = new FriendRepository();
            userRepository = new UserRepository();
        }

        public void AddFriend(FriendAddData friendAddData) 
        {
            if (!new EmailAddressAttribute().IsValid(friendAddData.FriendEmail) &&
                !new EmailAddressAttribute().IsValid(friendAddData.UserEmail))
                throw new ArgumentNullException();

            var findUserEntity = this.userRepository.FindByEmail(friendAddData.FriendEmail);
            if (findUserEntity is null)
                throw new UserNotFoundException();

            var userEntity = this.userRepository.FindByEmail(friendAddData.UserEmail);
            if (userEntity is null)
                throw new UserNotFoundException();

            var FriendEntity = new FriendEntity()
            {
                user_id = this.userRepository.FindByEmail(friendAddData.UserEmail).id,
                friend_id = this.userRepository.FindByEmail(friendAddData.FriendEmail).id
            };

            if (this.friendRepository.Create(FriendEntity) == 0)
                throw new Exception();
        }
    }
}
