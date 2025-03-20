using EnglishNow.Web.Models.Menu;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Components
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menu = new MenuViewModel
            {
                Ativo = ViewData["Menu"] as Menu? ?? Menu.Home
            };

            return View(menu);
        }
    }
}
