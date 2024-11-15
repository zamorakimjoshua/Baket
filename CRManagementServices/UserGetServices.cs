using ClassroomManagementData;
using ClassroomManagementModels;


namespace ClassroomManagementServices
{
    public class UserGetServices
    {

        public List<User> GetAllUsers()
        {
            UserData userData = new UserData();

            return userData.GetUsers();

        }

        public List<User> GetUsers(string prof)
        {
            List<User> users = new List<User>();

            foreach (var user in GetAllUsers())
            {
                if (user.status == prof)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public User GetUsers(string prof, string roomNum, string status)
        {
            User foundUser = new User();

            foreach (var user in GetAllUsers())
            {
                if (user.prof == prof && user.roomNum == roomNum && user.status == status)

                {
                    foundUser = user;
                }
            }

            return foundUser;
        }
    }
}