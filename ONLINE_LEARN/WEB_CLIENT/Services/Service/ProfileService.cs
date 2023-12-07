using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model;
using WEB_CLIENT.Model.DAO;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ProfileService : IProfileService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid UserID)
        {   
            try
            {
                User? user = await dao.getUser(UserID);
                if (user == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null , "Not found user", (int) HttpStatusCode.NotFound);
                }
                List<string> list = UserUtil.getAllGender();
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["user"] = user;
                result["list"] = list;
                return new ResponseDTO<Dictionary<string, object>?>(result, string.Empty);
            }catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid UserID, ProfileDTO DTO, string valueImg)
        {
            try
            {
                User? user = await dao.getUser(UserID);
                if (user == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found user", (int) HttpStatusCode.NotFound);
                }
                List<string> list = UserUtil.getAllGender();
                Dictionary<string, object> result = new Dictionary<string, object>();  
                user.FullName = DTO.FullName == null || DTO.FullName.Trim().Length == 0 ? "": DTO.FullName.Trim();
                user.Phone = DTO.Phone;
                user.Image = DTO.Image == null || DTO.Image.Trim().Length == 0 ? valueImg : "/img/" + DTO.Image.Trim();
                user.Address = DTO.Address == null || DTO.Address.Trim().Length == 0 ? null : DTO.Address.Trim();
                user.Email = DTO.Email.Trim();
                user.Gender = DTO.Gender.Trim();
                result["user"] = user;
                result["list"] = list;
                if(DTO.FullName == null || DTO.FullName.Trim().Length == 0)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result, "You have to input your name", (int) HttpStatusCode.Conflict);
                }
                if(DTO.Phone != null && DTO.Phone.Length != UserConst.PHONE_LENGTH)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result, "Phone must be " + UserConst.PHONE_LENGTH + " numbers", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Address != null && DTO.Address.Trim().Length > UserConst.MAX_ADDRESS_LENGTH)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result, "Address max " + UserConst.MAX_ADDRESS_LENGTH + " characters", (int)HttpStatusCode.Conflict);
                }
                if (await dao.isExist(user.Id, user.Email))
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result, "Email existed", (int)HttpStatusCode.Conflict);
                }
                user.UpdateAt = DateTime.Now;
                await dao.UpdateUser(user);   
                result["user"] = user;
                return new ResponseDTO<Dictionary<string, object>?>(result, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
