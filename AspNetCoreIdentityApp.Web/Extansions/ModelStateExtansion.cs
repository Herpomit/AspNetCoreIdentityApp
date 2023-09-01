using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreIdentityApp.Web.Extansions
{
    public static class ModelStateExtansion
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState, List<string> Erros)
        {
            Erros.ForEach(e =>
            {
                modelState.AddModelError(string.Empty, e);
            });


        }
    }
}
