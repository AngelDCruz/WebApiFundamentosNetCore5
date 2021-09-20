

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Servicios
{
    public class UsuarioAutenticadoServices
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioAutenticadoServices(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IdentityUser> ObtenerUsuario()
        {
            string email = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault().Value;
            IdentityUser usuario = await _userManager.FindByEmailAsync(email);
            return usuario;
        }

    }
}
