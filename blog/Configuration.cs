namespace blog;

public static class Configuration
{
    // Json Web Token - tem esse nome pois após o código ser descriptografado, será retornado um JSON com o código gerado.
    // Essa chave ficará no nosso servidor, quem tiver essa chave pode descriptografar o nosso token e editar ele.
    public static string JwtKey { get; set; } = new Guid().GetHashCode().ToString();
    
    
        
}
