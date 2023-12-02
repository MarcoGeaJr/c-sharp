namespace PizzaStore.API.API.Requests;

public class PostPizzaRequest
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public bool IsGlutenFree { get; set; }

	public bool IsValid()
	{
		bool isNameValid = !string.IsNullOrEmpty(Name) && Name.Length <= 50;
		bool isDescriptionValid = !string.IsNullOrEmpty(Description) && Description.Length <= 100;
		bool isValidPrice = Price > 0;

		return isNameValid && isDescriptionValid && isValidPrice;
	}
}
