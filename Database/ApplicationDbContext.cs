using HRMS.Enums;
using HRMS.Model;
using HRMS.Model.HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Database
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
       
        public DbSet<Attendance>Attendances { get; set; }
        public DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public DbSet<Leave>Leaves{ get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Message>Messages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }


      //  protected override void OnModelCreating(ModelBuilder modelBuilder)
      //  {
      //      modelBuilder.Entity<EmployeeInbox>().HasData(
                
      //          new EmployeeInbox { Id=1,ContactNumber="9045227812",JobRole="Developer",Name="Farhan Tyagi",WorkType="Full Time",AttendancePercentage= 97, Status="Active"}
                
                
      //          );
      //      modelBuilder.Entity<Attendance>().Property(a => a.Status).HasConversion<string>();
      //      modelBuilder.Entity<Attendance>().HasData(

      //          new Attendance { AttendanceId=01,EmployeeId = 03, AttendanceDate = DateTime.UtcNow,PunchInTime=DateTime.UtcNow,PunchOutTime=DateTime.UtcNow,TotalWorkingHours=10,Status=AttendanceStatus.Present,LastSyncTime=DateTime.UtcNow }
      //          ) ;
      //      modelBuilder
      // .Entity<Leave>()
      // .Property(l => l.LeaveType)
      // .HasConversion<string>();
      //      modelBuilder
      //.Entity<Leave>()
      //.Property(l => l.Status)
      //.HasConversion<string>();
      //  }



    }
   
}
