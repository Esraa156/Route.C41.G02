using Route.C41.G02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Route.C41.G02.PL.ViewModels
{
    public class DepartmentViewModel:ModelBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is reuqired here !!")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Date of creation")]
        public DateTime DateOfCreation { get; set; }
        //Navigational property
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
