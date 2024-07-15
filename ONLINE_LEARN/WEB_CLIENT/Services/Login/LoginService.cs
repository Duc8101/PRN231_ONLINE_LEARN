using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly DAOUser _daoUser;

        public LoginService(DAOUser daoUser)
        {
            _daoUser = daoUser;
        }

        public ResponseBase<User?> Index(Guid userId)
        {
            try
            {
                User? user = _daoUser.getUser(userId);
                if (user == null)
                {
                    return new ResponseBase<User?>("Not found user", (int)HttpStatusCode.NotFound);
                }
                return new ResponseBase<User?>(user);
            }
            catch (Exception ex)
            {
                return new ResponseBase<User?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<User?> Index(LoginDTO DTO)
        {
            try
            {
                User? user = _daoUser.getUser(DTO);
                if (DTO.Password == null || user == null)
                {
                    return new ResponseBase<User?>("Username or password incorrect", (int)HttpStatusCode.Conflict);
                }
                return new ResponseBase<User?>(user);
            }
            catch (Exception ex)
            {
                return new ResponseBase<User?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
