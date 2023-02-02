using System.ComponentModel.DataAnnotations;
using SimpleNet.Application.Users.Commands.Register;

namespace SimpleNet.ViewModels.Auth;
public class RegisterVm
{
    [Required]
    public string FullName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    public static explicit operator RegisterAppRequest(RegisterVm vm)
    {
        return new RegisterAppRequest(vm.FullName, vm.Email,vm.Password);
    }
}
