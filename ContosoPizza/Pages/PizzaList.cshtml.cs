using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        //BindProperty attribute binds NewPizza property to the Razor Page
        //When an HTTP POST request is made, NewPizza will be populated with user input from the page
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            //ModelState.IsValid property validatess user input by checking against Pizza class model
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            //NewPizza property is used to add new pizza to service object
            _service.AddPizza(NewPizza);

            //Redirect use to Get page handler and rerender page with updated list of pizzas
            return RedirectToAction("Get");
        }

        //PageHandler to delete pizzas; asp-page-handler="Delete"
        //id parameter is bound to id route value in url by the asp-route-id attribute
        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            //RedirectToAction method redirects user to Get page handler, re-rendering the page with updated list of pizzas
            return RedirectToAction("Get");
        }
    }
}
