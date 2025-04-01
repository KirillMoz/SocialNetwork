using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities;

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
    }
}
