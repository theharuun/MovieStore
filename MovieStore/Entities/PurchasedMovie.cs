using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class PurchasedMovie
    {
        [Key]
        [Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MovieID { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
