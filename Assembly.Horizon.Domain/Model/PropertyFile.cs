using Assembly.Horizon.Domain.Interface;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assembly.Horizon.Domain.Model;

public class PropertyFile : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public string FileName { get; set; }
    [NotMapped]
    [JsonIgnore]
    public IFormFile Upload { get; set; }
    private PropertyFile()
    {
        Id = Guid.NewGuid();
    }
    public PropertyFile(Guid propertyId, string fileName)
    {
        PropertyId = propertyId;
        FileName = fileName;
    }
}
