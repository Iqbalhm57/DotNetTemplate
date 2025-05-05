//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using iFormTem5.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace iFormTem5.Pages.Admin
//{
//    [Authorize(Roles = "Admin")]
//    public class UserManagementModel : PageModel
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        public UserManagementModel(
//            UserManager<ApplicationUser> userManager,
//            RoleManager<IdentityRole> roleManager,
//            SignInManager<ApplicationUser> signInManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _signInManager = signInManager;
//        }

//        public List<UserDto> Users { get; set; } = new();

//        [BindProperty]
//        public List<string> SelectedUserIds { get; set; } = new();

//        [BindProperty]
//        public string ActionType { get; set; }

//        public async Task OnGetAsync()
//        {
//            var allUsers = _userManager.Users.ToList();

//            foreach (var user in allUsers)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                Users.Add(new UserDto
//                {
//                    Id = user.Id,
//                    Email = user.Email,
//                    Role = roles.Contains("Admin") ? "Admin" : "User",
//                    IsBlocked = user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.Now,
//                    LastLoginTime = user.LastLoginTime
//                });
//            }
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            var currentUserId = _userManager.GetUserId(User);

//            foreach (var userId in SelectedUserIds)
//            {
//                var user = await _userManager.FindByIdAsync(userId);
//                if (user == null) continue;

//                switch (ActionType)
//                {
//                    case "Promote":
//                        if (!await _userManager.IsInRoleAsync(user, "Admin"))
//                            await _userManager.AddToRoleAsync(user, "Admin");
//                        break;

//                    case "Demote":
//                        if (await _userManager.IsInRoleAsync(user, "Admin"))
//                        {
//                            await _userManager.RemoveFromRoleAsync(user, "Admin");
//                            if (user.Id == currentUserId)
//                            {
//                                await _signInManager.SignOutAsync();
//                                return RedirectToPage("/Index");
//                            }
//                        }
//                        break;

//                    case "Block":
//                        user.LockoutEnabled = true;
//                        user.LockoutEnd = DateTimeOffset.MaxValue;
//                        await _userManager.UpdateAsync(user);
//                        break;

//                    case "Unblock":
//                        user.LockoutEnd = null;
//                        await _userManager.UpdateAsync(user);
//                        break;

//                    case "Delete":
//                        if (user.Id == currentUserId) continue; // prevent self-delete
//                        await _userManager.DeleteAsync(user);
//                        break;
//                }
//            }

//            return RedirectToPage();
//        }

//        public class UserDto
//        {
//            public string Id { get; set; }
//            public string Email { get; set; }
//            public string Role { get; set; }
//            public bool IsBlocked { get; set; }
//            public DateTime? LastLoginTime { get; set; }
//        }
//    }
//}
