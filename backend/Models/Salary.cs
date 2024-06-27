using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace backend.Models
{
    // [Table("Salaries")]
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal? BaseSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal? Bonus { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        // [NotMapped]
        public DateTime? Date { get; set; } = DateTime.Now;
        
        // [JsonIgnore]
        public Employee Employee { get; set; }
    }
}