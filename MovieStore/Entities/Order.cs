using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        // İlişkilendirilmiş müşteri
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        // Satın alınan filmler
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
