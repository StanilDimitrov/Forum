namespace Forum.Infrastructure.Identity
{
    internal interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
