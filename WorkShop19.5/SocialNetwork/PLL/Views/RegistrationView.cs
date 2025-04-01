using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class RegistrationView
    {
        UserService userService;
        public RegistrationView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show()
        {
            var userRegistrationData = new UserRegistrationData();

            Console.WriteLine("Для создания нового профиля введите ваше имя:");
            userRegistrationData.firstname = Console.ReadLine();

            Console.Write("Ваша фамилия:");
            userRegistrationData.lastname = Console.ReadLine();

            Console.Write("Пароль:");
            userRegistrationData.password = Console.ReadLine();

            Console.Write("Почтовый адрес:");
            userRegistrationData.email = Console.ReadLine();

            try
            {
                this.userService.Register(userRegistrationData);

                HelperMessage.Show(true, "Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
            }

            catch (ArgumentNullException)
            {
                HelperMessage.Show(false, "Введите корректное значение.");
            }

            catch (Exception)
            {
                HelperMessage.Show(false, "Произошла ошибка при регистрации.");
            }
        }
    }
}
