using ADYFreightDepartment.Models;
using ADYFreightDepartment.Services;
using Microsoft.AspNetCore.Components;

namespace ADYFreightDepartment.Pages
{
    public partial class LoadComponent
    {
        public IEnumerable<Load> Loads { get; set; }

        [Inject]
        public ILoadService LoadService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            Loads = LoadService.GetAll();
        }

        protected async Task RedirectToLoadDetails(string trackingId)
        {
            NavigationManager.NavigateTo($"load-details/{trackingId}");

            await InvokeAsync(StateHasChanged);
        }
    }
}
