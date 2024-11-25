namespace ComputerAPI.Models
{
   public record CreateOsDto(string? Name);
   public record CreateCompDto(string? brand, string? type, double display, int memory, Guid osid);
   public record UpdateOsDto(string? Name);
}
