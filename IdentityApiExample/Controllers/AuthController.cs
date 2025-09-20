using IdentityApiExample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApiExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Geçersiz istek." : e.ErrorMessage)
                .ToArray();
            return BadRequest(new { Errors = errors });
        }

        var user = new IdentityUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description).ToArray() });
        }

        return Ok(new { Message = "Kayıt başarılı." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Geçersiz istek." : e.ErrorMessage)
                .ToArray();
            return BadRequest(new { Errors = errors });
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized(new { Errors = new[] { "E‑posta veya parola hatalı." } });
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            return Ok(new { Message = "Giriş başarılı." });
        }
        if (result.IsLockedOut)
        {
            return Unauthorized(new { Errors = new[] { "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin." } });
        }

        return Unauthorized(new { Errors = new[] { "E‑posta veya parola hatalı." } });
    }

    [HttpPost("role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Geçersiz istek." : e.ErrorMessage)
                .ToArray();
            return BadRequest(new { Errors = errors });
        }

        var exists = await _roleManager.RoleExistsAsync(request.Name);
        if (exists)
        {
            return BadRequest(new { Errors = new[] { "Rol zaten mevcut." } });
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(request.Name));
        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description).ToArray() });
        }

        return Ok(new { Message = "Rol oluşturuldu." });
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Geçersiz istek." : e.ErrorMessage)
                .ToArray();
            return BadRequest(new { Errors = errors });
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound(new { Errors = new[] { "Kullanıcı bulunamadı." } });
        }

        var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
        if (!roleExists)
        {
            return NotFound(new { Errors = new[] { "Rol bulunamadı." } });
        }

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);
        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description).ToArray() });
        }

        return Ok(new { Message = "Rol kullanıcıya atandı." });
    }
}


