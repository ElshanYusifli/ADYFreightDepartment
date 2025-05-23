using ADYFreightDepartment.Models;
using ADYFreightDepartment.Services;
using Microsoft.AspNetCore.Components;

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

        public bool IsDataFound { get; set; } = true;

        public bool EditPopupVisible { get; set; }
        public bool StatusDetailsPopupVisible { get; set; }

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

        #region ProductGridActions

        protected async Task ShowEditPopup(int productId)
        {
            EditPopupVisible = true;

            await InvokeAsync(StateHasChanged);
        }

        protected async Task ShowStatusDetailsPopup(int productId)
        {
            StatusDetailsPopupVisible = true;

            await InvokeAsync(StateHasChanged);
        }

        #endregion

        #region EditPopupActions

        protected async Task ChangeStatus()
        {
            EditPopupVisible = false;

            await InvokeAsync(StateHasChanged);
        }

        protected async Task CloseEditPopup()
        {
            EditPopupVisible = false;

            await InvokeAsync(StateHasChanged);
        }

        #endregion

        #region StatusDetailsPopupActions

        protected async Task CloseStatusDetailsPopup()
        {
            StatusDetailsPopupVisible = false;

            await InvokeAsync(StateHasChanged);
        }

        #endregion
    }
}
