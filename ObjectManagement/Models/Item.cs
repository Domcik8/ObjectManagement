using System;
using System.ComponentModel.DataAnnotations;

namespace ObjectManagement.Models
{
    public class Item
    {
        public int ItemID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime Due { get; set; }

        public bool Completed { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public int PrincipalID { get; set; }
        public virtual Principal Principal { get; set; }
    }
}