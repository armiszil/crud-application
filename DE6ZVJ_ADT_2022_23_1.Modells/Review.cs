using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Modells
{
    [Table("reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Rating { get; set; }
        [NotMapped]
        public virtual Book Book { get; set; }
        [NotMapped]
        public int BookId { get; set; }

        public override string ToString()
        {
            return $"{Id},{Name},{Description},{Rating}";
        }
    }


}
