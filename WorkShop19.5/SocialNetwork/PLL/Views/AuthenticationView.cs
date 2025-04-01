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
    public class AuthenticationView
    {
        UserService userService;
        public AuthenticationView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show()
        {
            var authenticationData = new UserAuthenticationData();

            Console.WriteLine("Введите почтовый адрес:");
            authenticationData.Email = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            authenticationData.Password = Console.ReadLine();

            try
            {
                var user = this.userService.Authenticate(authenticationData);

                HelperMessage.Show(true, "Вы успешно вошли в социальную сеть!");
                HelperMessage.Show(true, "Добро пожаловать " + user.FirstName);

                Program.userMenuView.Show(user);
            }

            catch (WrongPasswordException)
            {
                HelperMessage.Show(false, "Пароль не корректный!");
            }

            catch (UserNotFoundException)
            {
                HelperMessage.Show(false, "Пользователь не найден!");
            }

        }
    }
}
