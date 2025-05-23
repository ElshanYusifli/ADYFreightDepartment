using ADYFreightDepartment.Models;

namespace ADYFreightDepartment.Services
{
    public interface ILoadService
    {
        IEnumerable<Load> Loads { get; set; }
        IEnumerable<Load> GetAll();
        Load GetByTrackingId(string trackingId);
        void UpdateProductStatus(string trackingId, int productId, ProductStatus status, string description, IFormFile formFile);
    }

    public class LoadService : ILoadService
    {
        public IEnumerable<Load> Loads { get; set; } = new List<Load> 
        {
            new Load
            {
                TrackingId = "000000001",
                Status = LoadLocationStatus.BakiDenizLimani,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Name = "MRT",
                        Status = ProductStatus.TeyinEdilmeyib,
                        Description = string.Empty,
                        ImagePath = string.Empty
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "TMM",
                        Status = ProductStatus.TeyinEdilmeyib,
                        Description = string.Empty,
                        ImagePath = string.Empty
                    }
                }
            },
            new Load
            {
                TrackingId = "000000002",
                Status = LoadLocationStatus.Bileceri,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 3,
                        Name = "XYZ",
                        Status = ProductStatus.TeyinEdilmeyib,
                        Description = string.Empty,
                        ImagePath = string.Empty
                    },
                    new Product
                    {
                        Id = 4,
                        Name = "M1",
                        Status = ProductStatus.TeyinEdilmeyib,
                        Description = string.Empty,
                        ImagePath = string.Empty
                    }
                }
            },
            new Load
            {
                TrackingId = "000000003",
                Status = LoadLocationStatus.Elet,
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 5,
                        Name = "PQR",
                        Status = ProductStatus.TeyinEdilmeyib,
                        Description = string.Empty,
                        ImagePath = string.Empty
                    }
                }
            }
        };

        public const string FilePath = "Loads/";

        public IEnumerable<Load> GetAll() => Loads;

        public Load GetByTrackingId(string trackingId) => Loads.FirstOrDefault(i => i.TrackingId == trackingId);

        public void UpdateProductStatus(string trackingId, int productId, ProductStatus status, string description, IFormFile formFile)
        {
            var load = Loads.FirstOrDefault(l => l.TrackingId == trackingId);

            if (load != null)
            {
                var product = load.Products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    product.Status = status;
                    product.Description = description;
                    product.ImagePath = GetImagePath(trackingId, productId, formFile);
                    return;
                }
            }

            return;
        }

        private string GetImagePath(string trackingId, int productId, IFormFile formFile)
        {
            string currentPath = $"{FilePath}/{trackingId}";

            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{formFile.FileName}";

            var filePath = Path.Combine(currentPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return filePath;
        }
    }
}
