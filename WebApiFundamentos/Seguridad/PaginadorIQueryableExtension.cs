using System.Collections.Generic;
using System.Linq;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Seguridad
{
    public static class PaginadorIQueryableExtension 
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginador paginador)
        {
            return queryable.Skip((paginador.Pagina - 1) * paginador.RegistrosPagina).Take(paginador.RegistrosPagina);
        }
    }
}
