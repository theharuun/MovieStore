using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Director
    {
        /*
            İsim
            Soyisim
            Yönettiği filmler
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //birden fazla filmi olabilir
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
