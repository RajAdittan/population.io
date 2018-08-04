using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Population.IO {
    [Table("Actuals")]
    public class Actual {
        [Key]
        [Column(Order=1)]
        public int State {get;set;}
        [Column(Order=2)]
        public double ActualPopulation{get;set;}
        [Column(Order=3)]
        public double ActualHouseholds {get;set;}
        
        public override string ToString()
        {
            return $"Acutual : {{ \n\t\"State\": {State}, \n\t\"ActualPopulation\": {ActualPopulation}, \n\t\"ActualHouseholds\": {ActualHouseholds}\n}}";
        }
    }
}