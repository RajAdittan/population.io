using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Population.IO {
    [Table("Estimates")]
    public class Estimate {
        [Key]
        [Column(Order=1)]
        public int State{ get; set; }
        [Key]
        [Column(Order=2)]
        public int Districts { get; set; }
        [Column(Order=3)]
        public int EstimatesPopulation { get; set; }
        [Column(Order=4)]
        public int EstimateHoseholds { get; set; }


        public override string ToString()
        {
            return
                $"Estimate : {{ \n\t\"State\": {State}, \n\t\"Disticts\": {Districts}, \n\t\"EstimatesPopulation\":{EstimatesPopulation}, \n\t\"EstimateHoseholds\":{EstimateHoseholds}\n}}";
        }
    }
}