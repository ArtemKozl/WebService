using System.ComponentModel.DataAnnotations;

namespace WebApp.Contracts
{
    public record MessageInputRequest(
        [Required] int SerialNumber,
        [Required] string Message);
    
}
