namespace MinimalAPI.API.Requests
{
    public class PostPutPizzaRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public bool IsValid()
        {
            bool isNameValid = !string.IsNullOrEmpty(Name) && Name.Length <= 50;
            bool isDescriptionValid = !string.IsNullOrEmpty(Description) && Description.Length <= 50;

            return isNameValid && isDescriptionValid;
        }
    }
}
