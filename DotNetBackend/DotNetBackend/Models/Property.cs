using System.ComponentModel.DataAnnotations;

namespace DotNetBackend.Models
{
    public class Property
    {
        [Key]
        public int Pid { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public decimal Rate { get; set; }
        public string? PropertyType { get; set; }

        // MongoDB references (stored as strings)
        public string? ExecutiveId { get; set; }
        public string? CustomerId { get; set; }
    }
}
