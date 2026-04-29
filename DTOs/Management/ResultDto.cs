namespace ProjetoPokeShop.DTOs
{
    public class ResultDto<T>
    {
        public string Message { get; set; }

        public DateTime At { get; set; } = DateTime.UtcNow;
        
        public T TargetEntity { get; set; }
    }
}