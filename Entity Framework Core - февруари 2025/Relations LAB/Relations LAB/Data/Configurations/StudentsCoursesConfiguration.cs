using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relations_LAB.Data.Models;

namespace Relations_LAB.Data.Configurations;

public class StudentsCoursesConfiguration:IEntityTypeConfiguration<StudentsCourses>
{
    public void Configure(EntityTypeBuilder<StudentsCourses> builder)
    {
        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
    }
}