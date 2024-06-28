using Common.Base;
using Common.Const;
using Common.Entity;
using DataAccess.Model.IDAO;
using DataAccess.Model.Util;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace WEB_CLIENT.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ICommonDAO<User> _daoUser;
        public AdminService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Index(string? name)
        {
            try
            {
                IQueryable<User> query = _daoUser.FindAll(u => u.IsDeleted == false && u.RoleName != UserConst.ROLE_ADMIN);
                if (name != null && name.Trim().Length > 0)
                {
                    query = query.Where(u => u.Username.ToLower().Contains(name.ToLower().Trim()));
                }
                List<User> list = await query.ToListAsync();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["list"] = list;
                data["name"] = name == null ? "" : name.Trim();
                return new ResponseBase<Dictionary<string, object>?>(data, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Detail(Guid UserID)
        {
            try
            {
                User? user = await _daoUser.GetById(UserID);
                if (user == null || user.RoleName == UserConst.ROLE_ADMIN)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found user", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<string> list = new List<string>();
                list.Add(UserConst.GENDER_MALE);
                list.Add(UserConst.GENDER_FEMALE);
                list.Add(UserConst.GENDER_OTHER);
                data["list"] = list;
                data["user"] = user;
                return new ResponseBase<Dictionary<string, object>?>(data, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
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

        public async Task<ResponseBase<List<string>?>> Create(User user)
        {
            List<string> list = Create();
            Regex regexPhone = new Regex(UserConst.FORMAT_PHONE);
            Regex regexUsername = new Regex(UserConst.FORMAT_USERNAME);
            Regex regexEmail = new Regex(UserConst.FORMAT_EMAIL);
            if (user.FullName == null)
            {
                return new ResponseBase<List<string>?>(list, "You have to input your name", (int)HttpStatusCode.Conflict);
            }
            if (user.Phone != null && (!regexPhone.IsMatch(user.Phone) || user.Phone.Length != UserConst.PHONE_LENGTH))
            {
                return new ResponseBase<List<string>?>(list, "Phone only number and length is " + UserConst.PHONE_LENGTH, (int)HttpStatusCode.Conflict);
            }
            if (!regexEmail.IsMatch(user.Email.Trim()))
            {
                return new ResponseBase<List<string>?>(list, "Invalid email", (int)HttpStatusCode.Conflict);
            }
            if (user.Username == null)
            {
                return new ResponseBase<List<string>?>(list, "You have to input username", (int)HttpStatusCode.Conflict);
            }
            if (!regexUsername.IsMatch(user.Username) || user.Username.Length > UserConst.MAX_LENGTH_USERNAME || user.Username.Length < UserConst.MIN_LENGTH_USERNAME)
            {
                return new ResponseBase<List<string>?>(list, "The username starts with alphabet , contains only alphabet and numbers, at least " + UserConst.MIN_LENGTH_USERNAME + " characters, max " + UserConst.MAX_LENGTH_USERNAME + " characters", (int)HttpStatusCode.Conflict);
            }
            try
            {
                if (await _daoUser.Any(u => u.Username == user.Username || u.Email == user.Email.Trim()))
                {
                    return new ResponseBase<List<string>?>(list, "Username or email has existed", (int)HttpStatusCode.Conflict);
                }
                string newPw = UserUtil.RandomPassword();
                string hashPw = UserUtil.HashPassword(newPw);
                // get body email
                string body = UserUtil.BodyEmailForRegister(newPw);
                // send email
                await UserUtil.sendEmail("Welcome to E-Learning", body, user.Email);
                user.Id = Guid.NewGuid();
                user.FullName = user.FullName.Trim();
                user.Image = UserConst.AVATAR;
                user.Address = null;
                user.Password = hashPw;
                user.RoleName = UserConst.ROLE_TEACHER;
                user.CreatedAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.IsDeleted = false;
                await _daoUser.Create(user);
                await _daoUser.Save();
                return new ResponseBase<List<string>?>(list, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<string>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
