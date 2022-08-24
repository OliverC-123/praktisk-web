namespace ScubaAPI.Models
{
    public class Sted
    {
        public int Id { get; set; }
        public int TypeID { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public string? Navn { get; set; }
        public string? Dybde { get; set; }
        public string? Content { get; set; }
    }
}
