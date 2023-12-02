using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStore.Web.Models;

namespace PizzaStore.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IHttpClientFactory _httpClientFactory;

		public IndexModel(
			ILogger<IndexModel> logger,
			IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		// Adds the data model and binds the form data to the model properties
		// Enumerable since an array is expected as a response
		[BindProperty]
		public IList<PizzaModel> PizzaModels { get; set; }

		// OnGet() is async since HTTP operations should be performed async
		public async Task OnGet()
		{
			// Create the HTTP client using the FruitAPI named factory
			var httpClient = _httpClientFactory.CreateClient("PizzaAPI");

			// Execute the GET operation and store the response, the empty parameter
			// in GetAsync doesn't modify the base address set in the client factory 
			using HttpResponseMessage response = await httpClient.GetAsync("/pizzas");

			// If the operation is successful deserialize the results into the data model
			if (response.IsSuccessStatusCode)
			{
				PizzaModels = await response.Content.ReadFromJsonAsync<IList<PizzaModel>>();
			}
		}
	}
}
