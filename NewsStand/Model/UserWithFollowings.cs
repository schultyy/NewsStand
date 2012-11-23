using System.Collections.ObjectModel;

namespace NewsStand.Model
{
    public class UserWithFollowings : User
    {
        public ObservableCollection<User> Followings { get; set; }
    }
}