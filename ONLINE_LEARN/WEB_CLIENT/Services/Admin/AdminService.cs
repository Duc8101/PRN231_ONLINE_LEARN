using AutoMapper;
using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;
using Common.Enums;
using DataAccess.Extensions;
using DataAccess.Model.DAO;
using DataAccess.Model.Helper;
using System.Net;
using System.Text.RegularExpressions;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.Admin
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly DAOUser _daoUser;

        public AdminService(IMapper mapper, DAOUser daoUser) : base(mapper)
        {
            _daoUser = daoUser;
        }

        public ResponseBase<Dictionary<string, object>?> Index(string? name)
        {
            try
            {
                List<User> list = _daoUser.getListUser(name);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["list"] = list;
                data["name"] = name == null ? "" : name.Trim();
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Detail(Guid userId)
        {
            try
            {
                User? user = _daoUser.getUser(userId);
                if (user == null || user.RoleName == Roles.Admin.ToString())
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found user", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["list"] = UserHelper.Genders;
                data["user"] = user;
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public List<string> Create()
        {
            return UserHelper.Genders;
        }

        public async Task<ResponseBase<List<string>?>> Create(UserCreateDTO DTO)
        {
            List<string> list = Create();
            Regex regexPhone = new Regex(UserInfo.Format_Phone.getDescription());
            Regex regexUsername = new Regex(UserInfo.Format_Username.getDescription());
            Regex regexEmail = new Regex(UserInfo.Format_Email.getDescription());
            if (DTO.FullName == null)
            {
                return new ResponseBase<List<string>?>(list, "You have to input your name", (int)HttpStatusCode.Conflict);
            }
            if (DTO.Phone != null && (!regexPhone.IsMatch(DTO.Phone) || DTO.Phone.Length != (int) UserInfo.Phone_Length))
            {
                return new ResponseBase<List<string>?>(list, "Phone only number and length is " + (int)UserInfo.Phone_Length, (int)HttpStatusCode.Conflict);
            }
            if (!regexEmail.IsMatch(DTO.Email.Trim()))
            {
                return new ResponseBase<List<string>?>(list, "Invalid email", (int)HttpStatusCode.Conflict);
            }
            if (DTO.Username == null || DTO.Username.Length == 0)
            {
                return new ResponseBase<List<string>?>(list, "You have to input username", (int)HttpStatusCode.Conflict);
            }
            if (!regexUsername.IsMatch(DTO.Username) || DTO.Username.Length > (int)UserInfo.Max_Length_Username || DTO.Username.Length < (int)UserInfo.Min_Length_Username)
            {
                return new ResponseBase<List<string>?>(list, "The username starts with alphabet , contains only alphabet and numbers, at least " + (int)UserInfo.Min_Length_Username + " characters, max " + (int)UserInfo.Max_Length_Username + " characters", (int)HttpStatusCode.Conflict);
            }
            try
            {
                if (_daoUser.isExist(DTO.Username, DTO.Email))
                {
                    return new ResponseBase<List<string>?>(list, "Username or email has existed", (int)HttpStatusCode.Conflict);
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
                user.RoleName = Roles.Teacher.ToString();
                user.CreatedAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.IsDeleted = false;
                _daoUser.CreateUser(user);
                return new ResponseBase<List<string>?>(list, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<string>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
