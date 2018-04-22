
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        //FK properties
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        //navigation properties
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}