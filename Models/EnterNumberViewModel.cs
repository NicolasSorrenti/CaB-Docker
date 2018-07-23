using System.ComponentModel.DataAnnotations;

namespace CowsAndBulls.Models
{
    public class EnterNumberViewModel
    {
        [Required]
        [Range(1234,9999 ,ErrorMessage ="Veso e vliuben v men!")]
        public int Number { get; set; }
    }
}
