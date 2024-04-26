using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

