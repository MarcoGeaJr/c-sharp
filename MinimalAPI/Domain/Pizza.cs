namespace MinimalAPI.Domain
{
    public class Pizza
    {
        public Pizza()
        {
            
        }
        public Pizza(
            string? name,
            string? description)
        {
            Name = name;
            Description = description;
        }

        public Pizza(
            int id,
            string? name,
            string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
