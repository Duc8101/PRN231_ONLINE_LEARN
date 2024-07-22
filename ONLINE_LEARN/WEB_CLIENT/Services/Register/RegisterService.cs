using AutoMapper;
using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;
using Common.Enums;
using DataAccess.Extensions;
using DataAccess.Model.DAO;
using DataAccess.Model.Helper;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.Register
{
    public class RegisterService : BaseService, IRegisterService
    {
        private readonly DAOUser _daoUser;

        public RegisterService(IMapper mapper, DAOUser daoUser) : base(mapper)
        {
            _daoUser = daoUser;
        }

        public async Task<ResponseBase<bool>> Index(UserCreateDTO DTO)
        {
            try
            {
                if (_daoUser.isExist(DTO.Username, DTO.Email))
                {
                    return new ResponseBase<bool>(false, "Username or email has existed", (int)HttpStatusCode.Conflict);
                }
                string newPw = UserHelper.RandomPassword();
                string hashPw = UserHelper.HashPassword(newPw);
                // get body email
                string body = UserHelper.BodyEmailForRegister(newPw);
                // send email
                await UserHelper.sendEmail("Welcome to E-Learning", body, DTO.Email.Trim());
                User user = _mapper.Map<User>(DTO);
                user.Id = Guid.NewGuid();
                user.Image = UserInfo.Avatar.getDescription();                
                user.Password = hashPw;
                user.RoleName = Roles.Student.ToString();
                user.CreatedAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.IsDeleted = false;
                _daoUser.CreateUser(user);
                return new ResponseBase<bool>(true, "Register successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
