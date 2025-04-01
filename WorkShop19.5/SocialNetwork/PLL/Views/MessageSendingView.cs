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
    public class MessageSendingView
    {
        UserService userService;
        MessageService messageService;
        public MessageSendingView(MessageService messageService, UserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        public void Show(User user)
        {
            var messageSendingData = new MessageSendingData();

            Console.Write("Введите почтовый адрес получателя: ");
            messageSendingData.RecipientEmail = Console.ReadLine();

            Console.WriteLine("Введите сообщение (не больше 5000 символов): ");
            messageSendingData.Content = Console.ReadLine();

            messageSendingData.SenderId = user.Id;

            try
            {
                messageService.SendMessage(messageSendingData);

                HelperMessage.Show(true, "Сообщение успешно отправлено!");

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
                HelperMessage.Show(false, "Произошла ошибка при отправке сообщения!");
            }

        }
    }
}
