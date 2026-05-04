namespace PokeShop.Application.DTOs.Management
{
    public record class ResultDto<T>(string Message, T ResponseDto)
    {
        public DateTime At { get; init; } = DateTime.Now;
    }
}