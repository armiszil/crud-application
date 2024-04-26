using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public class AuthorLogic : IAuthorLogic
    {
        protected IAuthorRepository _authorrepo;
        //private ReviewRepository @object;
        public virtual ICollection<Book> Books { get; set; }
        public AuthorLogic(IAuthorRepository authorrepo)
        {
            this._authorrepo = authorrepo;
        }

        //public AuthorLogic(ReviewRepository @object)
        //{
        //    this.@object = @object;
        //}

        public Author AddNewAuthor(Author aut)
        {
            if (aut.Name == null)
            {
                throw new ArgumentException("Please enter the author's name");
            }
            else
            {
                this._authorrepo.Add(aut);
                return aut;
            }
        }

        public void DeleteAuthor(int id)
        {
            Author AuthorDeletion = this._authorrepo.GetOne(id);
            if (AuthorDeletion != null)
            {
                this._authorrepo.Delete(AuthorDeletion);
            }
            else
            {
                throw new Exception("The ID is not in the Authors database.");
            }
        }
        
        public IEnumerable<Author> GetAllAuthors()
        {
            return this._authorrepo.GetAll();
        }

        public Author GetAuthor(int id)
        {
            Author AuthorReturn = this._authorrepo.GetOne(id);
            if (AuthorReturn != null)
            {
                return AuthorReturn;
            }
            else
            {
                throw new Exception("The ID is not in the authors database.");
            }
        }

        public void UpdateAuthorName(Author aut)
        {
            this._authorrepo.UpdateName(aut.Id, aut.Name);
        }
        public string FirstName(string name)
        {
           return name.Split(' ')[0];
        }
        public string SecondName(string name)
        {
            return name.Split(' ')[1];
        }
        public string AllCaps(string name)
        {
            return name.ToUpper();
        }
    }
}
