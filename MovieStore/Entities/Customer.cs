using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        // Satın aldığı filmler
        public List<PurchasedMovie> purchasedMovies { get; set; } = new List<PurchasedMovie>();

        // Favori türler
        public List<FavoriteGenre> favoriteGenres { get; set; } = new List<FavoriteGenre>();

        public string Password { get; set; }
        public string? RefreshToken { get; set; }  // Zorunlu olmaktan çıkarıldı
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
