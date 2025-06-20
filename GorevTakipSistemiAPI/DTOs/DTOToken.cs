namespace GorevTakipSistemiAPI.DTOs
{
    public class DTOToken
    {
        public string accessToken { get; set; }
        public DateTime expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
