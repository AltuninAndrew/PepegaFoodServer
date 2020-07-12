using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Contracts
{
    public static class ApiRoutes
    {
        public static class Identity
        {
            public const string Login = "api/identity/login";
            public const string Register = "api/identity/regist";
            public const string GetUserInfo = "api/identity/get_user_info/{username}";
            public const string CheckJWT = "api/identity/check_jwt";
        }


        public static class ClientData
        {
            public const string ChangePassword = "api/ClientData/change_password/{username}";
            public const string ChangeEmail = "api/ClientData/change_email/{username}";
            public const string ChangeFirstName = "api/ClientData/change_first_name/{username}";
            public const string ChangeLastName = "api/ClientData/change_last_name/{username}";
            public const string ChangePhone = "api/ClientData/change_phone/{username}";
            public const string ChangeAddress = "api/ClientData/change_address/{username}";
            public const string DeleteUser = "api/ClientData/delete_user/{username}";
        }

    }
}
