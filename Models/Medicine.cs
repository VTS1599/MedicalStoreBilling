namespace MedBilling.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? MedName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
