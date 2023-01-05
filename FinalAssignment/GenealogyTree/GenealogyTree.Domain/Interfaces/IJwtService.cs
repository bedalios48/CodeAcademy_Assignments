namespace GenealogyTree.Domain.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(int id, string role);
    }
}