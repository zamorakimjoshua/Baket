namespace ClassroomManagementServices
{
    public class UserValidationServices
    {

        UserGetServices getservices = new UserGetServices();

        public bool CheckIfProfExists(string prof)
        {
            bool result = getservices.GetUsers(prof) != null;
            return result;
        }

        public bool CheckIfUserExists(string prof, string roomNum, string status)
        {
            bool result = getservices.GetUsers(prof, roomNum, status) != null;
            return result;
        }

    }
}
