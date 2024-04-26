using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Modells
{
    [Table("authors")]
    public class Author
    {

        public Author()
        {
            this.Books = new HashSet<Book>();

        }

        [Required]

        public string Name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public virtual ICollection<Book> Books { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name},{Id}";



        }
    }
}
