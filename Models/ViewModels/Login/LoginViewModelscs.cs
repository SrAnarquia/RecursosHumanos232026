using System.ComponentModel.DataAnnotations;


namespace RecursosHumanos.Models.ViewModels.Login
{
    public class LoginViewModelscs
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;


        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }


    }
}
