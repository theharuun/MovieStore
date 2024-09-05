using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Entities
{
    public class MovieActor
    {
        [Key]
        [Column(Order = 0)]
        public int MovieID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ActorID { get; set; }

        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
