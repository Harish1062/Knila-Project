using System.Collections.Generic;

namespace KnilaProject.Models
{
    public class StaticUserList
    {
        public static List<UserModel> Users = new(){ new UserModel(){ Username="hvyasarao@ebintl.com",Password="Admin@1234",Role="Admin"}};
    }
}
