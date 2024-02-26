using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services
{
    public class LoginService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<User?>> Index(Guid UserID)
        {
            try
            {
                User? user = await dao.getUser(UserID);
                if (user == null)
                {
                    return new ResponseDTO<User?>(null, "Not found user", (int)HttpStatusCode.NotFound);
                }
                return new ResponseDTO<User?>(user, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<User?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDTO<User?>> Index(LoginDTO DTO)
        {
            try
            {
                User? user = await dao.getUser(DTO);
                if (user == null)
                {
                    return new ResponseDTO<User?>(null, "Username or password incorrect", (int)HttpStatusCode.Conflict);
                }
                return new ResponseDTO<User?>(user, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<User?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
