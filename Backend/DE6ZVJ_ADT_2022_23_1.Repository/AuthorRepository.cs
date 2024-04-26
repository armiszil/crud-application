using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookDbContext ctx) : base(ctx) { }
        public override Author GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(author => author.Id == id);
        }

        public void UpdateName(int id, string name)
        {
            var author = this.GetOne(id);
            if (author == null)
            {
                throw new Exception("This author doesn't exist in the database");
            }
            else
            {
                author.Name = name;
                this.context.SaveChanges();
            }
        }
    }
}
