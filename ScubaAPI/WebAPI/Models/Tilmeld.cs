namespace ScubaAPI.Models
{
    public class Tilmeld
    {
        public int Id { get; set; }
        public int TurID { get; set; }
        public string? Navn { get; set; }
        public string? Mail { get; set; }
        public string? Tlfnr { get; set; }
        public int Hojde { get; set; }
        public int Vaegt { get; set; }

    }
}
