using DataAccess.Base;
using DataAccess.Const;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using DataAccess.Model.IDAO;
using DataAccess.Model.Util;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ProfileService : IProfileService
    {
        private readonly ICommonDAO<User> _daoUser;
        public ProfileService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID)
        {
            try
            {
                User? user = await _daoUser.Get(u => u.Id == UserID && u.IsDeleted == false);
                if (user == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found user", (int)HttpStatusCode.NotFound);
                }
                List<string> list = UserUtil.getAllGender();
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["user"] = user;
                result["list"] = list;
                return new ResponseBase<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID, ProfileDTO DTO, string valueImg)
        {
            try
            {
                User? user = await _daoUser.Get(u => u.Id == UserID && u.IsDeleted == false);
                if (user == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found user", (int)HttpStatusCode.NotFound);
                }
                List<string> list = UserUtil.getAllGender();
                Dictionary<string, object> result = new Dictionary<string, object>();
                user.FullName = DTO.FullName == null || DTO.FullName.Trim().Length == 0 ? "" : DTO.FullName.Trim();
                user.Phone = DTO.Phone;
                user.Image = DTO.Image == null || DTO.Image.Trim().Length == 0 ? valueImg : "/img/" + DTO.Image.Trim();
                user.Address = DTO.Address == null || DTO.Address.Trim().Length == 0 ? null : DTO.Address.Trim();
                user.Email = DTO.Email.Trim();
                user.Gender = DTO.Gender.Trim();
                result["user"] = user;
                result["list"] = list;
                if (DTO.FullName == null || DTO.FullName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(result, "You have to input your name", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Phone != null && DTO.Phone.Length != UserConst.PHONE_LENGTH)
                {
                    return new ResponseBase<Dictionary<string, object>?>(result, "Phone must be " + UserConst.PHONE_LENGTH + " numbers", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Address != null && DTO.Address.Trim().Length > UserConst.MAX_ADDRESS_LENGTH)
                {
                    return new ResponseBase<Dictionary<string, object>?>(result, "Address max " + UserConst.MAX_ADDRESS_LENGTH + " characters", (int)HttpStatusCode.Conflict);
                }
                if (await _daoUser.Any(u => u.Id == user.Id || u.Email == user.Email.Trim()))
                {
                    return new ResponseBase<Dictionary<string, object>?>(result, "Email existed", (int)HttpStatusCode.Conflict);
                }
                user.UpdateAt = DateTime.Now;
                await _daoUser.Update(user);
                await _daoUser.Save();
                result["user"] = user;
                return new ResponseBase<Dictionary<string, object>?>(result, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
