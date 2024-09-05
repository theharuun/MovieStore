using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieID { get; set; }  // Property olarak tanımlandı

        public string? MovieName { get; set; }
        public DateTime MovieDate { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        public int DirectorID { get; set; }
        public Director Director { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

        public int Price { get; set; }

        // Aktif/Pasif durumu
        public bool IsActive { get; set; } = true;



    }
}
