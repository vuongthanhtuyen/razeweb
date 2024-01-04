using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VTT.models{
    public class AppUser : IdentityUser{
        [Column(TypeName ="nvarchar")]
        [StringLength(400)]
        public string? HomeAdress {get; set;}

        [DataType(DataType.Date)]
        public DateTime? BirthDate {get; set;}
    }
}