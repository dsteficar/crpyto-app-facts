using Microsoft.AspNetCore.Components;
using System.Net;
using WebAdminUI.Models.ParallelChannels;
using WebAdminUI.Services.Tokens;

namespace WebAdminUI.Services.ParallelChannels
{

    public class ParallelChannelsClientService : BaseClientServiceOLD, IParallelChannelsClientService
    {
        private const string BaseUrl = "api/parallelchannel";

        public ParallelChannelsClientService(
            HttpClient httpClient,
            ITokenClientService tokenClientService,
            NavigationManager navigationManager
            ) : base(httpClient, tokenClientService, navigationManager)
        {
        }

        public async Task<List<ParallelChannelDataGridAdminModel>> GetAllAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/all");

                var response = await SendRequestAsync<List<ParallelChannelDataGridAdminModel>>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null!;
            }

            return new List<ParallelChannelDataGridAdminModel>();
        }

        public async Task<ParallelChannelDataGridAdminModel> AddAsync(ParallelChannelDataGridAdminModel requestDto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/add")
                {
                    Content = JsonContent.Create(requestDto)
                };

                var response = await SendRequestAsync<ParallelChannelDataGridAdminModel>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null!;
            }

            return null!;
        }

        public async Task<ParallelChannelDataGridAdminModel> UpdateAsync(ParallelChannelDataGridAdminModel requestDto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/update")
                {
                    Content = JsonContent.Create(requestDto)
                };

                var response = await SendRequestAsync<ParallelChannelDataGridAdminModel>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null!;
            }

            return null!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/delete/{id}");

                var response = await SendRequestAsync<bool>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Data;
                    return content;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

            return false;
        }
    }
}

