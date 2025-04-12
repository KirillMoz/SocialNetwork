using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class FriendAddView
    {
        UserService userService;
        FriendService friendService;

        public FriendAddView(UserService userService, FriendService friendService)
        {
            this.userService = userService;
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            var friendAddData = new FriendAddData();
            Console.Write("Введите почтовый адрес друга: ");
            friendAddData.FriendEmail = Console.ReadLine();

            friendAddData.UserEmail = user.Email;

            try
            {
                friendService.AddFriend(friendAddData);
                HelperMessage.Show(true, "Друг успешно добавлен!");

                user = userService.FindById(user.Id);
            }
 
            catch (UserNotFoundException)
            {
                HelperMessage.Show(false, "Пользователь не найден!");
            }

            catch (ArgumentNullException)
            {
                HelperMessage.Show(false, "Введите корректное значение!");
            }

            catch (Exception)
            {
                HelperMessage.Show(false, "Произошла ошибка при добавлении друга!");
            }
        }
    }
}
