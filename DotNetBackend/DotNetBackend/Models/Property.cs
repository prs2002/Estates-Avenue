using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Property
{
    [Key]
    [Column("id")]
    public int Pid { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("location")]
    public string? Location { get; set; }

    [Column("rate")]
    public decimal Rate { get; set; }

    [Column("propertyType")]
    public string? PropertyType { get; set; }

    [Column("description")]
    public string? Desc { get; set; }

    [Column("executive_id")]
    public string? ExecutiveId { get; set; }

    [Column("customer_id")]
    public string? CustomerId { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }
}