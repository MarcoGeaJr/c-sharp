namespace MinimalAPI.API.Requests;

public class PutPizzaRequest
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public bool IsGlutenFree { get; set; }

	public bool IsValid()
	{
		bool isValidId = Id > 0;
		bool isNameValid = !string.IsNullOrEmpty(Name) && Name.Length <= 50;
		bool isDescriptionValid = !string.IsNullOrEmpty(Description) && Description.Length <= 100;
		bool isValidPrice = Price > 0;

		return isValidId && isNameValid && isDescriptionValid && isValidPrice;
	}
}
