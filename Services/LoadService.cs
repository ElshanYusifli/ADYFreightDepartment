using ADYFreightDepartment.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ADYFreightDepartment.Services
{
    public interface ILoadService
    {
        IEnumerable<Load> Loads { get; set; }
        IEnumerable<Load> GetAll();
        Load GetByTrackingId(string trackingId);
        Task UpdateProductStatus(UpdateProductDetailsModel model);
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

        public const string FilePath = "Loads";

        public IEnumerable<Load> GetAll() => Loads;

        public Load GetByTrackingId(string trackingId) => Loads.FirstOrDefault(i => i.TrackingId == trackingId);

        public async Task UpdateProductStatus(UpdateProductDetailsModel model)
        {
            var load = Loads.FirstOrDefault(l => l.TrackingId == model.TrackingId);

            if (load != null)
            {
                var product = load.Products.FirstOrDefault(p => p.Id == model.ProductId);

                if (product != null)
                {
                    product.Status = model.Status;
                    product.Description = model.Description;
                    if (model.File != null)
                    {
                        product.ImagePath = await GetImagePath(model.TrackingId, model.ProductId, model.File);
                    }
                    return;
                }
            }

            return;
        }

        private async Task<string> GetImagePath(string trackingId, int productId, IBrowserFile browserFile)
        {
            //string currentPath = $"{FilePath}\\{trackingId}";

            var currentPath = Path.Combine(AppContext.BaseDirectory, FilePath, trackingId);

            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{browserFile.Name}";

            var filePath = Path.Combine(currentPath, uniqueFileName);

            using var stream = browserFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await stream.CopyToAsync(fileStream);

            return filePath;
        }
    }
}
