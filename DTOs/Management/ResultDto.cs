namespace ProjetoPokeShop.DTOs
{
    public class ResultDto<T>
    {
        public string Message { get; set; }

<<<<<<< HEAD
        public DateTime At { get; set; } = DateTime.Now;
=======
        public DateTime At { get; set; } = DateTime.UtcNow;
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
        
        public T TargetEntity { get; set; }
    }
}