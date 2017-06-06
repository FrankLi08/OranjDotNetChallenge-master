using OranjDotNetChallenge_master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace OranjDotNetChallenge_master.Controllers
{
    [RoutePrefix("api/Oranj")]
    public class SpotifyController : ApiController
    {
        // GET: Spotify
        HttpClient _Client = new HttpClient();

       

        // POST: Spotify/Create
        [HttpPost]
        public async Task<OranjAlbumDTO> Create([FromBody] OranjAlbum album)
        {
            OranjAlbumDTO oranjAlbum = new OranjAlbumDTO();

            try
            {
                _Client.BaseAddress = new Uri("https://kkuwbyropc.execute-api.us-east-1.amazonaws.com/internexam");
                _Client.DefaultRequestHeaders.Accept.Clear();
                _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _Client.PostAsJsonAsync("https://kkuwbyropc.execute-api.us-east-1.amazonaws.com/internexam", album);

                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK )
                {
                    var getResponse = await _Client.GetAsync("https://kkuwbyropc.execute-api.us-east-1.amazonaws.com/internexam");
                    if (getResponse.IsSuccessStatusCode)
                    {
                        oranjAlbum = await getResponse.Content.ReadAsAsync<OranjAlbumDTO>();
                    }
                }
                else
                {
                    throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
                }
                
            }
            catch
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return oranjAlbum;
        }

               
    }
}
