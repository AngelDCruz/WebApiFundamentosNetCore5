using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiFundamentos.Seguridad
{
    public class ClaimsSistema
    {
        public const string Administrador = "Administrador";
        public const string Normal = "Normal";

        public static Dictionary<string, Claim> Claims()
        {
            Dictionary<string, Claim> listaDiccionario  = new Dictionary<string, Claim>();
            listaDiccionario.Add(Administrador, new Claim($@"es{Administrador}", "1"));
            listaDiccionario.Add(Normal, new Claim($@"es{Normal}", "1"));

            return listaDiccionario;
        }

        public static Claim BuscarClaim(string nombreClaim)
        {
            Dictionary<string, Claim> listaDiccionario = ClaimsSistema.Claims();

            return listaDiccionario[nombreClaim];
        }
    }
}
