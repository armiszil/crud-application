using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public class BookRepository: Repository<Book>, IBookRepository
    {
        public BookRepository(BookDbContext ctx) : base(ctx) { }

        public override Book GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(book => book.Id == id);
        }

        public void UpdateTitle(int id, string title)
        {
            var book = this.GetOne(id);
            if (book == null)
            {
                throw new Exception("This book doesn't exist in the database");
            }
            else
            {
                book.Title = title;
                this.context.SaveChanges();
            }
        }
    }
}
