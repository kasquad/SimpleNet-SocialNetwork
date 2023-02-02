using System.ComponentModel.DataAnnotations;
using SimpleNet.Application.Users.Commands.Login;

namespace SimpleNet.ViewModels.Auth;

public class LoginVm
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public static explicit operator LoginAppRequest(LoginVm vm)
    {
        return new LoginAppRequest(vm.Email, vm.Password);
    }
}
