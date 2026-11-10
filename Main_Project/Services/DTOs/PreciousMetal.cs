namespace Main_Project.Services.DTOs
{
    public class PreciousMetal
    {
        public string Name { get; set; } = default!; // Selling Price
        public string Symbol { get; set; } = default!; // Selling Price
        public string ask { get; set; } = default!; // Selling Price
        public string bid { get; set; } = default!; // Buying Price
        public string mid { get; set; } = default!; // Mid-market Price
        public string change { get; set; } = default!; // price change
        public string changePercentage { get; set; } = default!; // Percentage change
        public string high { get; set; } = default!; // Highest price
        public string low { get; set; } = default!; // Lowest price
        public string unit { get; set; } = default!; // Price unit
    }
}