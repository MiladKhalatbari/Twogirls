using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TwoGirls.Core.Convertors
{
    public static class RenderViewToString
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;

            using var sw = new StringWriter();
            var viewResult = FindView(controller, viewName);
            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                sw,
                new HtmlHelperOptions()
            );
            viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }

        private static ViewEngineResult FindView(Controller controller, string viewName)
        {
            var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
            if (!viewResult.Success)
            {
                throw new FileNotFoundException($"View '{viewName}' not found.");
            }
            return viewResult;
        }
    }
}