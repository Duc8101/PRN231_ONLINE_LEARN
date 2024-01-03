using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class HomeService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<List<User>?>> Index()
        {
            try
            {
                List<User> list = await dao.getTop4Teacher();
                return new ResponseDTO<List<User>?>(list, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<User>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
