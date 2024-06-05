using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace MyApp.Namespace
{
    [Route("api/Customer")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private ISuperAdminRepository _superAdminRepository;

        public SuperAdminController(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }


    }
}
