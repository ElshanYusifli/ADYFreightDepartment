namespace ADYFreightDepartment.Models
{
    public class Load
    {
        public string? TrackingId { get; set; }
        public LoadLocationStatus Status { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductStatus Status { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

    public enum LoadLocationStatus
    {
        BakiDenizLimani = 1,
        Bileceri = 2,
        Elet = 3,
        Chatdirildi = 4
    }

    public enum ProductStatus
    {
        TeyinEdilmeyib = 0,
        Zedelenmeyib = 1,
        Zedelenmish = 2
    }
}
