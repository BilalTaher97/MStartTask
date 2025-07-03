namespace KafanaTask.Server.DTOs
{
    public class ProductCreateDto
    {
        public string NameEn { get; set; } = null!;
        public string? NameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string StatusEn { get; set; } = null!;
        public string StatusAr { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
    }
}
