namespace IDsas.Server
{
    public class Document
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[]? FileData { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Signed { get; set; }
        public string? Signer { get; set; }
        public DateTime? SigningDate { get; set; }
    }
}