using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreCodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.Data
{
    public class MyBlogDbContext : DbContext
    {
        //NE ZABRAVQI DA SUZDADESH KLAS DbConfig s connection stringa vutre !!!

        public MyBlogDbContext()
        {

        }

        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Post> Posts { get; set; }

    }




}
