using Labb_2_MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace Labb_2_MVC.Services
{
	public class BaseService : IBaseService
	{
		public ResponseDTO responseModel { get; set; }

		public IHttpClientFactory _httpClient { get;set; }

		public BaseService(IHttpClientFactory httpClient)
		{
			this._httpClient = httpClient;
			this.responseModel = new ResponseDTO();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(true);
		}

		public async Task<T> SendAsync<T>(ApiRequest apiRequest)
		{
			try
			{
				var client = _httpClient.CreateClient("BookAPI");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiRequest.Url);

				if (apiRequest.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
				}
				HttpResponseMessage apiResp = null;
				switch (apiRequest.ApiType)
				{
					case StaticDetails.ApiType.GET:
						message.Method = HttpMethod.Get;
						break;
					case StaticDetails.ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case StaticDetails.ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case StaticDetails.ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
				}
				apiResp = await client.SendAsync(message);
				var apiContent = await apiResp.Content.ReadAsStringAsync();
				var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);
				return apiResponseDTO;
			}
			catch(Exception ex)
			{
				var dto = new ResponseDTO
				{
					Displaymessage = "Error",
					Error = new List<string> { Convert.ToString(ex.Message) },
					Success = false
				};
				var res = JsonConvert.SerializeObject(dto);
				var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);
				return apiResponseDTO;
			}

		}
	}
}
