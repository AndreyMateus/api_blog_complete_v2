using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace blog.Services;

public class TokenService
{
    // Precisaremos de dois pacotes para gerar o token.
    // Microsoft.AspNetCore.Authentication
    // e
    // Microsoft.AspNetCore.Authentication.JwtBearer

    // pasando o User que terá as Roles dentro dele.
    public string GenerateToken(User user)
    {
        // Manipulador de Token que nos permitirá criar um token.
        var tokenHandler =  new JwtSecurityTokenHandler();
        
        // Transformando a chave em bytes, pois é isso que o método do handler espera.
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

        // criando uma especificação/configuração para o token ser criado em cima/ baseando-se nela.
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            // Tempo que o token irá durar, pois ele não pode viver pra sempre, nesse caso será de 8 horas.
            // ou seja: estamos passando a data de quando ele irá expirar, ou seja daqui a 8 horas ele irá expirar e o usuário precisará fazer login novamente.
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                ),
        };

        // criando o token baseando na Configuração/Descrição.
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Retornando o token em string.
        return tokenHandler.WriteToken(token);
    }
    
    
}
