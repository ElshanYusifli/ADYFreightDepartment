using ADYFreightDepartment.Models;
using ADYFreightDepartment.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ADYFreightDepartment.Pages
{
    public partial class LoadDetailsComponent : ComponentBase
    {
        public Load CurrentLoad { get; set; }
        [Parameter]
        public string TrackingId { get; set; }
        [Inject]
        public ILoadService LoadService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Product CurrentProduct { get; set; }
        public UpdateProductDetailsModel CurrentPopupModel { get; set; }
        public bool IsDataFound { get; set; } = true;
        public bool IsReadOnly { get; set; }
        public bool PopupVisible { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(TrackingId))
            {
                CurrentLoad = LoadService.GetByTrackingId(TrackingId);
            }

            if (CurrentLoad == null)
            {
                IsDataFound = false;
            }

            await InvokeAsync(StateHasChanged);
        }

        protected async Task RedirectToLoads()
        {
            NavigationManager.NavigateTo($"loads");

            await InvokeAsync(StateHasChanged);
        }

        #region ProductGridActions

        protected async Task ShowEditPopup(int productId)
        {
            CurrentProduct = CurrentLoad.Products.FirstOrDefault(i => i.Id == productId);

            CurrentPopupModel = new UpdateProductDetailsModel
            {
                TrackingId = CurrentLoad.TrackingId,
                ProductId = productId,
                Status = CurrentProduct.Status,
                Description = CurrentProduct.Description
            };

            PopupVisible = true;

            IsReadOnly = false;

            await InvokeAsync(StateHasChanged);
        }

        protected async Task ShowStatusDetailsPopup(int productId)
        {
            CurrentProduct = CurrentLoad.Products.FirstOrDefault(i => i.Id == productId);

            CurrentPopupModel = new UpdateProductDetailsModel
            {
                TrackingId = CurrentLoad.TrackingId,
                ProductId = productId,
                Status = CurrentProduct.Status,
                Description = CurrentProduct.Description
            };

            PopupVisible = true;

            IsReadOnly = true;

            await InvokeAsync(StateHasChanged);
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            CurrentPopupModel.File = e.File;

            await InvokeAsync(StateHasChanged);
        }

        #endregion

        #region EditPopupActions

        protected async Task ChangeStatus()
        {
            await LoadService.UpdateProductStatus(CurrentPopupModel);

            PopupVisible = false;

            await InvokeAsync(StateHasChanged);
        }

        #endregion


        #region HelperMethods

        public string ConvertImagePathToBase64(string relativePathFromDebug)
        {
            string debugFolder = AppContext.BaseDirectory;
            string imagePath = Path.Combine(debugFolder, relativePathFromDebug);

            if (!File.Exists(imagePath))
                return null;

            byte[] imageBytes = File.ReadAllBytes(imagePath);

            string extension = Path.GetExtension(imagePath).ToLowerInvariant();
            string mimeType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            return $"data:{mimeType};base64,{Convert.ToBase64String(imageBytes)}";
        }

        #endregion
    }
}
