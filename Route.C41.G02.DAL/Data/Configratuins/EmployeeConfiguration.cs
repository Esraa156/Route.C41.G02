using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Data.Configratuins
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        //Fluent Apis for Embloyee
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E=>E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.gender)
                .HasConversion(

                (gender) => gender.ToString(),//Store in DataBase
                (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)//Retrieve from DataBase
                );

        }
    }
}
