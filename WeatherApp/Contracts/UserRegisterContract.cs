using System.ComponentModel.DataAnnotations;

namespace WeatherApp.API.Contracts
{
    public record UserRegisterContract([Required]string login, [Required]string password, [Required] string repeatPassword)
    { 
    }
}
