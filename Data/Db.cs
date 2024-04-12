using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AppraisalManagentSystem.Data
{
    public class Db:DbContext
    {
        internal object _loginDetails;

        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

       public DbSet<Employees> Employees { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employees>()
        //        // .HasOne(e => e.Manager)
        //        .WithMany(e
        //        .HasForeignKey(e => e._managerId)
        //        .IsRequired(false); // Manager is optional
        //}
        public DbSet<Appraisal> Appraisals {  get; set; }

        public DbSet<AppraisalStatus> AppraisalStatus { get; set; } 
        public DbSet<Competencies> Competencies { get; set; }   
        public DbSet<LoginDetails> LoginDetails { get; set; } 


        public DbSet<AppraisalWithCopm> AppraisalWithCopms { get; set; }    

    }
}
