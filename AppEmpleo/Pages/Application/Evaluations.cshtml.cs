using AppEmpleo.Class.Utilities.Normalization;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppEmpleo.Pages.Application
{
    [Authorize(Roles = RoleConstants.Recruiter)]
    public class EvaluationsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IJobApplicationService _applicationService;

        public UserAccount CurrentUser { get; private set; } = null!;

        public List<JobApplication> AllApplications { get; set; } = [];

        public EvaluationsModel(IUserService userService, IJobApplicationService applicationService)
        {
            _userService = userService;
            _applicationService = applicationService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            LoadCurrentUser();
            await LoadApplicationsAsync();

            return Page();
        }

        private async Task LoadApplicationsAsync()
            => AllApplications = await _applicationService.GetApplicationsByRecruiterAsync(CurrentUser.UserId);

        public async Task<IActionResult> OnGetDownloadResumeAsync(int resumeId)
        {
            var resume = await _applicationService.GetResumeByIdAsync(resumeId);

            if (resume == null)
            {
                return NotFound();
            }

            var filePath = _applicationService.GetResumeFilePath(resume);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return await _applicationService.DownloadResumeAsync(resume, filePath);
        }

        private void LoadCurrentUser()
        {
            CurrentUser = _userService.GetCurrentUserFromClaims();
        }
    }
}
