using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvidencePojisteni.Models
{
    public class AssignedInsurance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int AssignedInsuranceId { get; set; }

        [ForeignKey("Insurance")]
        [Display(Name = "Pojištění")]
        public int InsuranceId { get; set; }

        [ForeignKey("Insured")]
        public int InsuredId { get; set; }

        [Required(ErrorMessage = "Vyplňte částku")]
        [Range(0, int.MaxValue, ErrorMessage = "Částka nesmí být záporná")]
        [Display(Name = "Částka")]
        public int Value { get; set; }

        [Required(ErrorMessage = "Vyplňte předmět pojištění")]
        [StringLength(20, ErrorMessage = "Předmět pojištění vyplňte v rozsahu 3 až 20 znaků", MinimumLength = 3)]
        [Display(Name = "Předmět pojištění")]
        public string Issue { get; set; }

        [Required(ErrorMessage = "Zadejte datum platnosti od")]
        [DataType(DataType.Date, ErrorMessage = "Zadané datum není validní")]
        [Display(Name = "Platnost od")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Zadejte datum platnosti do")]
        [DataType(DataType.Date, ErrorMessage = "Zadané datum není validní")]
        [Display(Name = "Platnost do")]
        public DateTime ValidTo { get; set; }

        public virtual Insurance Insurance { get; set; }

        public virtual Insured Insured { get; set; }
    }
}
