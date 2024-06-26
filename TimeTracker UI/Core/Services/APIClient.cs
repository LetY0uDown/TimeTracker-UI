﻿using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;
using System.Windows;

namespace TimeTracker.UI.Core.Services;

public sealed class APIClient (IConfiguration config)
{
    private readonly JsonSerializerOptions _jsonOptions = new() {
        PropertyNameCaseInsensitive = true
    };

    private readonly IConfiguration _config = config;

    public async Task<T?> GetAsync<T> (string url) where T : class
    {
        T? entity = default;

        using (RestClient client = GetClient()) {
            RestRequest request = new(url, Method.Get);

            try {
                var response = await client.GetAsync(request);

                if (!response.IsSuccessStatusCode) {
                    MessageBox.Show(response.Content, response.StatusCode.ToString());

                    return default;
                }

                entity = JsonSerializer.Deserialize<T>(response.Content!, _jsonOptions);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        return entity;
    }

    public async Task<bool> DeleteAsync (string url)
    {
        bool isSuccess = false;

        using (RestClient client = GetClient()) {
            RestRequest request = new(url, Method.Delete);

            try {
                var response = await client.DeleteAsync(request);

                isSuccess = response.IsSuccessful;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        return isSuccess;
    }

    public async Task PostAsync<T> (T value, string url) where T : class
    {
        using (RestClient client = GetClient()) {
            RestRequest request = new(url, Method.Post);

            if (value is not null) {
                request.AddJsonBody(value);
            }

            try {
                var response = await client.PostAsync(request);

                if (!response.IsSuccessStatusCode) {
                    MessageBox.Show(response.Content, response.StatusCode.ToString());

                    return;
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        return;
    }

    public async Task PutAsync (string url)
    {
        using (RestClient client = GetClient()) {
            RestRequest request = new RestRequest(url, Method.Put);

            try {
                var response = await client.PutAsync(request);

                if (!response.IsSuccessStatusCode) {
                    return;
                }

                if (string.IsNullOrWhiteSpace(response.Content))
                    return;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }
    }

    private RestClient GetClient ()
    {
        var options = new RestClientOptions {
            BaseUrl = new(_config["HostURL"]!)
        };

        return new(options);
    }
}