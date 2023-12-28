using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VTT.models{
    public class AppUser : IdentityUser{
        [Column(TypeName ="nvarchar")]
        [StringLength(400)]
        public sbyte HomeAdress {get; set;}
    }
}