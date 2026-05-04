namespace PokeShop.Application.DTOs.Management
{
    public record class PokemonManagementResponseDto(int Id, string Name,
        string Nature, IEnumerable<Element> Elements,
        int RarityId, int? OwnerId);
}