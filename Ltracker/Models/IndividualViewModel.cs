using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ltracker.Models
{
    public class IndividualViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Nombre")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [DisplayName("Correo electrónico")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage ="La entrada no tiene el formato de correo electronico")]
        //[Remote("CheckEmail", "Individual", HttpMethod ="GET", ErrorMessage ="Correo electronico ocupado (Remote)")]
        public string Email { get; set; }

        public string EmailAnterior { get; set; }
    }
}