using System.ComponentModel;

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
        [Description("Bakı Dəniz Limanı")]
        BakiDenizLimani = 1,

        [Description("Biləcəri")]
        Bileceri = 2,

        [Description("Ələt")]
        Elet = 3,

        [Description("Çatdırıldı")]
        Chatdirildi = 4
    }

    public enum ProductStatus
    {
        [Description("Təyin edilməyib")]
        TeyinEdilmeyib = 0,

        [Description("Zədələnməyib")]
        Zedelenmeyib = 1,

        [Description("Zədələnmiş")]
        Zedelenmish = 2
    }
}
