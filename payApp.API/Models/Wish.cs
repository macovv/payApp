namespace payApp.API.Models
{
    public class Wish
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public string UrlToShop { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}