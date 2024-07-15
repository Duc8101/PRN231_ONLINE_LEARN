using AutoMapper;
using Common.Base;
using Common.Const;
using Common.DTO.UserDTO;
using Common.Entity;
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
                if (user == null || user.RoleName == UserConst.ROLE_ADMIN)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found user", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<string> list = new List<string>();
                list.Add(UserConst.GENDER_MALE);
                list.Add(UserConst.GENDER_FEMALE);
                list.Add(UserConst.GENDER_OTHER);
                data["list"] = list;
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
            List<string> list = new List<string>();
            list.Add(UserConst.GENDER_MALE);
            list.Add(UserConst.GENDER_FEMALE);
            list.Add(UserConst.GENDER_OTHER);
            return list;
        }

        public async Task<ResponseBase<List<string>?>> Create(UserCreateDTO DTO)
        {
            List<string> list = Create();
            Regex regexPhone = new Regex(UserConst.FORMAT_PHONE);
            Regex regexUsername = new Regex(UserConst.FORMAT_USERNAME);
            Regex regexEmail = new Regex(UserConst.FORMAT_EMAIL);
            if (DTO.FullName == null)
            {
                return new ResponseBase<List<string>?>(list, "You have to input your name", (int)HttpStatusCode.Conflict);
            }
            if (DTO.Phone != null && (!regexPhone.IsMatch(DTO.Phone) || DTO.Phone.Length != UserConst.PHONE_LENGTH))
            {
                return new ResponseBase<List<string>?>(list, "Phone only number and length is " + UserConst.PHONE_LENGTH, (int)HttpStatusCode.Conflict);
            }
            if (!regexEmail.IsMatch(DTO.Email.Trim()))
            {
                return new ResponseBase<List<string>?>(list, "Invalid email", (int)HttpStatusCode.Conflict);
            }
            if (DTO.Username == null || DTO.Username.Length == 0)
            {
                return new ResponseBase<List<string>?>(list, "You have to input username", (int)HttpStatusCode.Conflict);
            }
            if (!regexUsername.IsMatch(DTO.Username) || DTO.Username.Length > UserConst.MAX_LENGTH_USERNAME || DTO.Username.Length < UserConst.MIN_LENGTH_USERNAME)
            {
                return new ResponseBase<List<string>?>(list, "The username starts with alphabet , contains only alphabet and numbers, at least " + UserConst.MIN_LENGTH_USERNAME + " characters, max " + UserConst.MAX_LENGTH_USERNAME + " characters", (int)HttpStatusCode.Conflict);
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
                user.Image = UserConst.AVATAR;
                user.Password = hashPw;
                user.RoleName = UserConst.ROLE_TEACHER;
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
