using System.Collections.Generic;

namespace StartupProject.AdminUI.Models
{
    public class DashboardViewModel
    {
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public string SystemStatus { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}