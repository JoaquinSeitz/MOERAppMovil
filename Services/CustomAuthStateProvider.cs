using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MOERAppMovil.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await SecureStorage.Default.GetAsync("jwt_token");
            var nombre = await SecureStorage.Default.GetAsync("user_name");
            var rol = await SecureStorage.Default.GetAsync("user_role");
            var id = await SecureStorage.Default.GetAsync("user_id");
            var email = await SecureStorage.Default.GetAsync("user_email");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nombre ?? "Usuario"),
                    new Claim(ClaimTypes.Role, rol ?? "Cliente"),
                    new Claim(ClaimTypes.NameIdentifier, id ?? "1"),
                    new Claim(ClaimTypes.Email, email ?? "")
                }, "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void MarcarComoAutenticado(string nombre, string rol, string id = "", string email = "")
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.Role, rol),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email, email)
            }, "jwt");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarcarComoDeslogueado()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}