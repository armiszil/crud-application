using DE6ZVJ_ADT_2022_23_1.Modells;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class BookDbContext : DbContext
{
    

    public BookDbContext()
    {
        this.Database.EnsureCreated();
    }

    public DbSet<Book> Books;
    public DbSet<Author> Authors;
    public DbSet<Review> Reviews;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.
                UseLazyLoadingProxies().
                UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True");
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        Author a1 = new Author { Name = "Stephen King", Id = 1 };
        Author a2 = new Author { Name = "George Martin", Id = 2 };
        Author a3 = new Author { Name = "Ernest Hemingway", Id = 3 };
        Author a4 = new Author { Name = "Leo Tolstoy", Id = 4 };


        Book b1 = new Book {Id=1,Title="It",Genre="horror", Pages=1492, AuthorId=a1.Id};
        Book b2 = new Book {Id=2,Title="Pet Sematary",Genre="horror",Pages=374,AuthorId=a1.Id };
        Book b3 = new Book {Id=3,Title="A Dance with Dragons",Genre="fantasy",Pages=1180, AuthorId=a2.Id };
        Book b4 = new Book {Id=4,Title="A Feast for Crows", Genre="fantasy",Pages=866,AuthorId=a2.Id };
        Book b5 = new Book {Id=5,Title="For Whom the Bell Tolls", Genre="fiction",Pages=629,AuthorId=a3.Id };
        Book b6 = new Book {Id=6,Title="The Sun Also Rises",Genre="romance",Pages=194,AuthorId=a3.Id };
        Book b7 = new Book {Id=7,Title="The Death of Ivan Ilyich",Genre="fiction",Pages=93,AuthorId=a4.Id };
        Book b8 = new Book {Id=8,Title="Childhood",Genre="autobiography",Pages=100,AuthorId=a4.Id };

        Review r1 = new Review { Id=1,Name="John", Description="Very scary", Rating="Good",BookId=b1.Id };
        Review r2 = new Review {Id=2,Name="Jane",Description="Not scary enough",Rating="OKay",BookId=b1.Id };
        Review r3 = new Review {Id=3,Name="Alex",Description="Good reading,good story",Rating="Good",BookId=b3.Id };
        Review r4 = new Review {Id=4,Name="Chad",Description="Not at all scary, too short",Rating="Bad",BookId=b2.Id };
        Review r5 = new Review {Id=5,Name="Dwayne",Description="Good experience, nice fantasy story",Rating="Good",BookId=b4.Id };
        Review r6 = new Review {Id=6,Name="Jim",Description="Interesting autobiography",Rating="Good",BookId=b8.Id };
        Review r7 = new Review {Id=7,Name="Tyler",Description="Very good story,in russian also",Rating="Good",BookId=b7.Id };
        Review r8 = new Review {Id=8,Name="Rebecca",Description="Very romantic,I enjoyed it",Rating="Good",BookId=b6.Id };
        Review r9 = new Review {Id=9,Name="James",Description="Reminds me of the song",Rating="Okay",BookId=b5.Id };
        Review r10 = new Review {Id=10,Name="Tyrion",Description="A nice light reading",Rating="OKay",BookId=b7.Id };
        Review r11 = new Review {Id=11,Name="Tyrique",Description="I would have rather done anything else",Rating="Bad",BookId=b3.Id };
        Review r12 = new Review {Id=12,Name="Lilla",Description="I love me a romantic book any time of the year",Rating="Good",BookId=b6.Id };

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(order => order.Author)
                 .WithMany(clown => clown.Books)
                 .HasForeignKey(order => order.AuthorId)
                 .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(order => order.Book)
                 .WithMany(customer => customer.Reviews)
                 .HasForeignKey(order => order.BookId)
                 .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Author>().HasData(a1, a2, a3, a4);
        modelBuilder.Entity<Book>().HasData(b1, b2, b3, b4, b5, b6, b7, b8);
        modelBuilder.Entity<Review>().HasData(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12);





        





























    }
}



   

