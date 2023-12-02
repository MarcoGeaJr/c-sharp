using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStore.Web.Models;
using System.Text;
using System.Text.Json;

namespace PizzaStore.Web.Pages
{
	public class EditPizzaModel : PageModel
	{
		// IHttpClientFactory set using dependency injection 
		private readonly IHttpClientFactory _httpClientFactory;

		public EditPizzaModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

		// Add the data model and bind the form data to the page model properties
		[BindProperty]
		public PizzaModel PizzaModels { get; set; }

		// Retrieve the data to populate the form for editing
		public async Task OnGet(int id)
		{
			// Create the HTTP client using the PizzaAPI named factory
			var httpClient = _httpClientFactory.CreateClient("PizzaAPI");

			// Retrieve record information to populate the form
			using HttpResponseMessage response = await httpClient.GetAsync($"/pizzas/{id}");

			if (response.IsSuccessStatusCode)
			{
				// Deserialize the response to populate the form
				PizzaModels = await response.Content.ReadFromJsonAsync<PizzaModel>();
			}
		}


		// OnPost() is async since HTTP operations should be performed async
		public async Task<IActionResult> OnPost()
		{
			// Serialize the information to be added to the database
			var jsonContent = new StringContent(JsonSerializer.Serialize(PizzaModels),
				Encoding.UTF8,
				"application/json");

			// Create the HTTP client using the PizzaAPI named factory
			var httpClient = _httpClientFactory.CreateClient("PizzaAPI");

			// Execute the POST request and store the response. The parameters in PostAsync 
			// direct the POST to use the base address and passes the serialized data to the API
			using HttpResponseMessage response = await httpClient.PutAsync($"/pizzas/{PizzaModels.Id}", jsonContent);

			// Return to the home (Index) page and add a temporary success/failure 
			// message to the page.
			if (response.IsSuccessStatusCode)
			{
				TempData["success"] = "Data was updated successfully.";
				return RedirectToPage("Index");
			}
			else
			{
				TempData["failure"] = "Operation was not successful";
				return RedirectToPage("Index");
			}

		}
	}
}
