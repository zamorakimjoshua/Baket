using ClassroomManagementServices;
using Microsoft.AspNetCore.Mvc;
using ClassroomManagementModels;

namespace CRMAPI3.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class RoomManagementController : ControllerBase
    {
        UserGetServices userGetServices;
        UserTransactionServices userTransactionServices;

        public RoomManagementController()
        {
            userGetServices = new UserGetServices();
            userTransactionServices = new UserTransactionServices();
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var user = userGetServices.GetAllUsers();

            List<User> users = new List<User>();

            foreach (var Users in user)
            {
                users.Add(new User { prof = Users.prof, roomNum = Users.roomNum, status = Users.status });
            }
            return users;
        }

        [HttpPost]
        public JsonResult AddUser(User request)
        {
            var result = userTransactionServices.CreateUser(request.prof, request.roomNum, request.status);

            return new JsonResult(result);
        }

        [HttpPatch]
        public JsonResult UpdateUSer(User request)
        {
            var result = userTransactionServices.UpdateUser(request.prof, request.roomNum, request.status);

            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult DeleteUser(User request)
        {
            var deleteuser = new ClassroomManagementModels.User
            {
                prof = request.prof
            };

            var result = userTransactionServices.DeleteUser(deleteuser);

            return new JsonResult(result);
        }


    }
}