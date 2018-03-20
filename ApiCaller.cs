using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using DMBuddy.Models;
 
namespace DMBuddy
{
    public class WebRequest
    {
        // The second parameter is a function that returns a Dictionary of string keys to object values.
        // If an API returned an array as its top level collection the parameter type would be "Action>"

        public static async Task GetAllSpellDataAsync(Action<Dictionary<string, object>> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"http://dnd5eapi.co/api/spells/");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                     
                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    // SpellAPIResponse JsonResponse = JsonConvert.DeserializeObject<SpellAPIResponse>(StringResponse);
                    Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);
                    var DeserializedResponse = new SpellAPIResponse();
                    foreach(var info in JsonResponse){
                        
                    }

                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
        // public static async Task GetSpellDataAsync(int SpellId, Action<Dictionary<string, object>> Callback)
        // {
        //     // Create a temporary HttpClient connection.
        //     using (var Client = new HttpClient())
        //     {
        //         try
        //         {
        //             Client.BaseAddress = new Uri($"http://dnd5eapi.co/api/spells/{SpellId}");
        //             HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
        //             Response.EnsureSuccessStatusCode(); // Throw error if not successful.
        //             string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                     
        //             // Then parse the result into JSON and convert to a dictionary that we can use.
        //             // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
        //             Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);
                     
        //             // Finally, execute our callback, passing it the response we got.
        //             Callback(JsonResponse);
        //         }
        //         catch (HttpRequestException e)
        //         {
        //             // If something went wrong, display the error.
        //             Console.WriteLine($"Request exception: {e.Message}");
        //         }
        //     }
        // }
    }
}