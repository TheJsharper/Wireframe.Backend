using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wireframe.Backend.Models
{
    public class WireframeModel
    {

       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ICollection<Device> Devices { get; set; } = new List<Device>();

        public override string ToString()
        {
            StringBuilder sb = new();
            return Devices.Select((d) => sb.AppendLine(d.ToString())).ToString() ?? "";
        }
    }
}
