using ADYFreightDepartment.Models;
using ADYFreightDepartment.Services;
using Microsoft.AspNetCore.Components;

namespace ADYFreightDepartment.Pages
{
    public partial class LoadComponent
    {
        private ILoadService _loadService;

        public LoadComponent(ILoadService loadService)
        {
            _loadService = loadService;
        }

        public IEnumerable<Load> GetAll()
        {
            return _loadService.GetAll();
        }
    }
}
