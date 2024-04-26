using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public interface IAuthorLogic
    {
        Author GetAuthor(int id);
        IEnumerable<Author> GetAllAuthors();
        void UpdateAuthorName(Author clas);
        public Author AddNewAuthor(Author clas);
        public void DeleteAuthor(int id);

        public string FirstName(string name);
        public string SecondName(string name);
        public string AllCaps(string name);
    }
}
