using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RecursosHumanosAng.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WApiController: ControllerBase
    {
        HttpClient clientApi = new HttpClient();

        string url = "http://apiservicios.ecuasolmovsa.com:3009";

        [HttpGet("Login")]
        public async Task<string> loginAsync(string nombreUsuario, string passwordUsuario, string codigoEmisor)
        {

            string parameters = "?usuario=" + nombreUsuario + "&password=" + passwordUsuario;
            HttpResponseMessage response = clientApi.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + "/api/Usuarios" + parameters)
            }).Result;

            string content = await response.Content.ReadAsStringAsync();
            dynamic resp = JsonConvert.DeserializeObject(content);

            if (resp == "error")
            {
                return "Error al encontrar el usuario";
            }

            if (resp.Emisor != codigoEmisor)
            {
                return "No se pudo iniciar sesion";
            }

            return resp;
        }


    }
}