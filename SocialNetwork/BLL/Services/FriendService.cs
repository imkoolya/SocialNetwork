using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        IFriendRepository friendRepository;
        IUserRepository userRepository;

        public FriendService()
        {
            userRepository = new UserRepository();
            friendRepository = new FriendRepository();
        }

        public void SendRequest(FriendRequestData friendRequestData)
        {
            if (String.IsNullOrEmpty(friendRequestData.FriendEmail))
                throw new ArgumentNullException();

            var findUserEntity = this.userRepository.FindByEmail(friendRequestData.FriendEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var friendRequest = new FriendEntity()
            {
                user_id = friendRequestData.UserId,
                friend_id = findUserEntity.id
            };

            if (this.friendRepository.Create(friendRequest) == 0)
                throw new Exception();
        }
    }
}
