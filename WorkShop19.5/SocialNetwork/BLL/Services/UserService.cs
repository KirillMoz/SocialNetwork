using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        const int max_len_password = 8;
        MessageService messageService;
        IUserRepository userRepository;
        public UserService() {
            userRepository = new UserRepository();
            messageService = new MessageService();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if ((String.IsNullOrEmpty(userRegistrationData.firstname)) || 
                (String.IsNullOrEmpty(userRegistrationData.lastname)) || 
                (String.IsNullOrEmpty(userRegistrationData.email)) ||
                (String.IsNullOrEmpty(userRegistrationData.password)))
                throw new ArgumentNullException();

            if (userRegistrationData.password.Length < max_len_password)
                throw new ArgumentNullException();

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.email))
                throw new ArgumentNullException();

            if (userRepository.FindByEmail(userRegistrationData.email) != null)
                throw new ArgumentException();

            UserEntity userEntity = new UserEntity()
            {
                firstname = userRegistrationData.firstname,
                lastname = userRegistrationData.lastname,
                password = userRegistrationData.password,
                email = userRegistrationData.email
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new ArgumentException();
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageService.GetIncomingMessagesByUserId(userEntity.id);

            var outgoingMessages = messageService.GetOutcomingMessagesByUserId(userEntity.id);

            return new User(userEntity.id, 
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book, 
                          incomingMessages,
                          outgoingMessages);
        }
    
        public User FindByEmail(string Email)
        {
            UserEntity findUserEntity = userRepository.FindByEmail(Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if (this.userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }
    }
}
