using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectManagement.Models
{
    public class Principal
    {
        public int PrincipalID { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\d]*$")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<Object> Objects { get; set; }
    }
}