using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Models
{


    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male =1,
        [EnumMember(Value = "Female")]

        Female = 2
    }
   public enum EmpType
    {
        Fulltime =1,
        Parttime=2
    }

    public class Employee:ModelBase
    {

        public int Id { get; set; }
        
        public string Name { get; set; }
        public int? Age { get; set; }
     

        public string Address { get; set; }


        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }

        public DateTime CreationDate { get; set; }=DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public Gender gender { get; set; }

        public EmpType Emptype { get; set; }


        //[ForeignKey]
        public int? DepartmentId { get; set; }

        //[navigation ONE]
        public virtual Department Department { get; set; }

    }
}
