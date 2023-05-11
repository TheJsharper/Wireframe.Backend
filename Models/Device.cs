using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wireframe.Backend.Models
{
    public class Device
    {
        
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string DeviceTypeId { get; set; } = "";
        [Required]
        public bool Failsafe { get; set; }
        [Required]
        public long TempMin { get; set; }
        [Required]
        public long TempMax { get; set; }
        [Required]
        public string InstallationPosition { get; set; } = "";
        [Required]
        public bool InsertInto19InchCabinet { get; set; }
        [Required]
        public bool MotionEnable { get; set; }
        [Required]
        public bool SiplusCatalog { get; set; }
        [Required]
        public bool SimaticCatalog { get; set; }
        [Required]
        public long RotationAxisNumber { get; set; }
        [Required]
        public long PositionAxisNumber { get; set; }
        
        public bool? AdvancedEnvironmentalConditions { get; set; }
        public bool? TerminalElement { get; set; }

        [ForeignKey("WireframeId")]
        public virtual WireframeModel? WireframeModel { get; set; }

        public override string ToString()
        {
            return $" Id: {Id}\nName: ${Name}\n DeviceType:{DeviceTypeId}\n ";
        }
    }
}
