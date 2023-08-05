using City_Vibe.ViewModels.ApiBulletinBoard;
using System.Text;
using System.Text.Json;

namespace City_Vibe.Services.ApiBulletinBoard
{
    public class BulletinBoardService
    {
        private readonly HttpClient httpClient;
        Uri baseAddress = new Uri("https://localhost:7219/api");

        public BulletinBoardService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
        }

        public async Task<T> BulletinBoardDeserialize<T>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
        
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            T messageBoard = JsonSerializer.Deserialize<T>(data, options)!;
            return messageBoard;
        }

        public async Task<List<BulletinBoard>> GetAllBulletinBoard()
        { 
            HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/MessageBoard/GetMessageBoards"); 
            var messageBoard = await BulletinBoardDeserialize<List<BulletinBoard>>(response);                                      
            return messageBoard;
        }

        public async Task<BulletinBoard> EditBulletinBoardGet(string id)
        {
            HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/MessageBoard/GetMessageBoard/" + id);
            var messageBoard = await BulletinBoardDeserialize<BulletinBoard>(response);
            return messageBoard;
        }

        public async Task EditBulletinBoardPost(BulletinBoard messageBoard)
        {
            try
            {
                var data = JsonSerializer.Serialize(messageBoard);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress + "/MessageBoard/Update", content);
                if (response.IsSuccessStatusCode) return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BulletinBoard> DeleteBulletinBoardGet(string id)
        {
            HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + "/MessageBoard/GetMessageBoard/" + id);
            var messageBoard = await BulletinBoardDeserialize<BulletinBoard>(response);
            return messageBoard;
        }

        public async Task DeleteBulletinBoardPost(string Id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(httpClient.BaseAddress + "/MessageBoard/Delete/" + Id);
            if (response.IsSuccessStatusCode) return;
        }


        public async Task CreateBulletinBoard(BulletinBoard messageBoard)
        {
            try
            {
                var data = JsonSerializer.Serialize(messageBoard);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(httpClient.BaseAddress + "/MessageBoard/Create", content);

                if (response.IsSuccessStatusCode) return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
