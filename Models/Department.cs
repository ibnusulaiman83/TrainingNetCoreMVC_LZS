
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTraining2.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Department Name")]
        public string Name { get; set; }
    }
}
