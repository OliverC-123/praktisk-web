namespace ScubaAPI.Models
{
    public class Tur
    {
        public int Id { get; set; }

        public int StedID { get; set; }
        public string? Navn { get; set; }
        public string?  Dato { get; set; }
        public string? Tid { get; set; }
        public string? Beskrivelse { get; set; }
        public int Pladser { get; set; }
        public int Tilmeldte { get; set; }
        public int Pris { get; set; }
        public int Rabat { get; set; }
    }
}
