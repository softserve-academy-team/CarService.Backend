using Microsoft.IdentityModel.Tokens;


namespace CarService.Api.Models
{
    public class AuthOptions
    {
        
        public string Issuer { get; set; } = "CarServer"; 
        public string Audience { get; set; } = "http://localhost:5000/";
        public string Key { get; set; } = "mysupersecret_secretkey!123"; 
        public int Lifetime { get; set; } = 1; 
     
    }
}