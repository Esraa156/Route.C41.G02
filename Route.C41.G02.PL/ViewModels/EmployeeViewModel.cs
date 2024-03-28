﻿using Route.C41.G02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace Route.C41.G02.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public enum Gender
        {
            [EnumMember(Value = "Male")]
            Male = 1,
            [EnumMember(Value = "Female")]

            Female = 2
        }
        public enum EmpType
        {
            Fulltime = 1,
            Parttime = 2
        }

      
            public int Id { get; set; }
            [Required(ErrorMessage = "Name is Required!")]
            [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
            [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
            public string EmpName { get; set; }
            [Range(22, 30)]
            public int? Age { get; set; }
            [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address must be like 123-Street-City-Country")]

            public string Address { get; set; }


            [DataType(DataType.Currency)]
            public decimal Salary { get; set; }
            [Display(Name = "Is Active")]
            public bool IsActive { get; set; }
            [EmailAddress]

            //[DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Display(Name = "Phone Number")]
            [Phone]
            //[DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Display(Name = "Hiring Date")]
            public DateTime HiringDate { get; set; }

            public DateTime CreationDate { get; set; } = DateTime.Now;
            public bool IsDeleted { get; set; } = false;

            public Gender gender { get; set; }

            public EmpType Emptype { get; set; }


            //[ForeignKey]
            public int? DepartmentId { get; set; }

            //[navigation ONE]
            public virtual Department Department { get; set; }

        }
    }