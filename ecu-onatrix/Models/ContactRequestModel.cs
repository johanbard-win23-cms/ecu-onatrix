namespace ecu_onatrix.Models;

public class ContactRequestModel
{
    public string? Name { get; set; }
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Category { get; set; }
    public string? Question { get; set; }
}
