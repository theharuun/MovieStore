using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Actor
    {
        /*
            İsim
            Soyisim
            Oynadığı filmler
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //birden fazla filmi olabilir
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
