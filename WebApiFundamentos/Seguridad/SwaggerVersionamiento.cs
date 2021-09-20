using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace WebApiFundamentos.Seguridad
{
    public class SwaggerVersionamiento : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceControlador = controller.ControllerType.Namespace; // Controllers.V1
            var versionAPI = namespaceControlador.Split('.').Last().ToLower(); // v1
            controller.ApiExplorer.GroupName = versionAPI;
        }
    }
}
