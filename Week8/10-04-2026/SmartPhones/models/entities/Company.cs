namespace SmartPhones.models.entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        // Removed erroneous user-defined conversion operator that converted Company to Company.
        // C# does not allow defining an operator that converts a type to itself (CS0555).
    }
}
