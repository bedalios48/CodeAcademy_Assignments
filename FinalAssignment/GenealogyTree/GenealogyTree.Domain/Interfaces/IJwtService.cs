﻿namespace GenealogyTree.Domain.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(int userId, string role);
    }
}