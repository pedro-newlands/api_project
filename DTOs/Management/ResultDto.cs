namespace ProjetoPokeShop.DTOs
{
    public class ResultDto<T>
    {
        public string Message { get; set; }

        public DateTime At { get; set; } = DateTime.Now;
        
        public T TargetEntity { get; set; }
    }
}