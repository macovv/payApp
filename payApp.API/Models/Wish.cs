using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payApp.API.Models
{
    public class Wish
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Cost { get; set; }
        public string UrlToShop { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}