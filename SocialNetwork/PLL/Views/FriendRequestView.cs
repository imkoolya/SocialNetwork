using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class FriendRequestView
    {
        UserService userService;
        FriendService friendService;
        public FriendRequestView(FriendService friendService, UserService userService)
        {
            this.friendService = friendService;
            this.userService = userService;
        }

        public void Show(User user)
        {
            var friendRequestData = new FriendRequestData();

            Console.Write("Введите почтовый адрес получателя: ");
            friendRequestData.FriendEmail = Console.ReadLine();

            friendRequestData.UserId = user.Id;
            
            try
            {
                friendService.SendRequest(friendRequestData);

                SuccessMessage.Show("Запрос успешно отправлен!");

                user = userService.FindById(user.Id);
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при отправке Запроса!");
            }

        }
    }
}