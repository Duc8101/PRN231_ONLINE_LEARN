using AutoMapper;

namespace WEB_CLIENT.Services.Base
{
    public class BaseService
    {
        internal readonly IMapper _mapper;
        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
