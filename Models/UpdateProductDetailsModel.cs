using Microsoft.AspNetCore.Components.Forms;

namespace ADYFreightDepartment.Models
{
    public class UpdateProductDetailsModel
    {
        public string TrackingId { get; set; }
        public int ProductId { get; set; }
        public ProductStatus Status { get; set; }
        public string Description { get; set; }
        public IBrowserFile File { get; set; }
    }
}
