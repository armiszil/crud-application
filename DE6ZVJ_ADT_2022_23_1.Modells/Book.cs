using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Modells
{
    [Table("books")]
    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public string Genre { get; set; }
        [NotMapped]

        public virtual Author Author { get; set; }
        public Book()
        {
            this.Reviews = new HashSet<Review>();
        }


        [ForeignKey(nameof(Author))]
        public int? AuthorId { get; set; }

        [NotMapped]
        public virtual ICollection<Review> Reviews { get; set; }

        public override string ToString()
        {
            return $"{Id},{Title},pages:{Pages},genre:{Genre}";
        }
    }

}
