[RegisterService(ServiceLifetime.Scoped)]
public sealed class OpenAIAPIClient
{
    [InjectService]
    public HttpClient HttpClient { get; private set; }

    private const string BaseUrl = "https://api.openai.com/v1/";

    public OpenAIAPIClient(HttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ConversationThread> CreateThreadAsync()
    {
        var response = await HttpClient.PostAsync($"{BaseUrl}threads", null);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var thread = JsonConvert.DeserializeObject<ConversationThread>(content);

        return thread;
    }

    public async Task AddMessageToThreadAsync(string threadId, string message)
    {
        var payload = new
        {
            thread_id = threadId,
            text = message
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync($"{BaseUrl}threads/{threadId}/messages", content);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// ---------------------------  QUERY PARAMETERS --------------------------- All parameters are
    /// passed as a dictionary of key-value pairs.
    // limit integer (Optional) Defaults to 20
    // - A limit on the number of objects to be returned.Limit can range between 1 and 100, and the
    // default is 20.

    // order string (Optional) Defaults to desc
    // - Sort order by the created_at timestamp of the objects. asc for ascending order and desc for
    // descending order.
    //
    // after string (Optional)
    // - A cursor for use in pagination.after is an object ID that defines your place in the list.
    // For instance, if you make a list request and receive 100 objects, ending with obj_foo, your
    // subsequent call can include after = obj_foo in order to fetch the next page of the list.
    //
    // before string (Optional)
    // - A cursor for use in pagination.before is an object ID that defines your place in the list.
    // For instance, if you make a list request and receive 100 objects, ending with obj_foo, your
    // subsequent call can include before = obj_foo in order to fetch the previous page of the list.
    //
    // run_id string (Optional)
    // - Filter messages by the run ID that generated them.
    /// --------------------------- END QUERY PARAMETERS ---------------------------
    public async Task<List<ChatMessage>> GetMessages(Dictionary<string, string> keyValuePairs)
    {
        HttpHelpers helpers = new HttpHelpers(BaseUrl);
        Uri uri = new Uri(helpers.BuildHttpGetUrl("threads", keyValuePairs));

        var response = await HttpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var messages = JsonConvert.DeserializeObject<List<ChatMessage>>(content);

        return messages;
    }

    public async Task<ConversationThread> GetThreadAsync(string threadId)
    {
        var response = await HttpClient.GetAsync($"{BaseUrl}threads/{threadId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var thread = JsonConvert.DeserializeObject<ConversationThread>(content);

        return thread;
    }

    /// <summary>
    /// A Patch to update the metadata of a thread.
    /// </summary>
    /// <param name="threadId"> </param>
    /// <param name="metadata"> </param>
    /// <returns> </returns>
    public async Task ModifyThreadAsync(string threadId, Dictionary<string, string> metadata)
    {
        // Validate metadata constraints
        foreach (var item in metadata)
        {
            if (item.Key.Length > 64)
            {
                throw new ArgumentException($"Metadata key '{item.Key}' exceeds the maximum length of 64 characters.");
            }

            if (item.Value.Length > 512)
            {
                throw new ArgumentException($"Metadata value for key '{item.Key}' exceeds the maximum length of 512 characters.");
            }
        }

        // Ensure the metadata is nested within a "metadata" key
        var payload = new { metadata };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        // Use PATCH method for partial update
        var response = await HttpClient.PatchAsync($"{BaseUrl}threads/{threadId}", content);
        response.EnsureSuccessStatusCode();

        // Optionally, handle the response to parse and return the modified thread object
    }

    public async Task DeleteThreadAsync(string threadId)
    {
        var response = await HttpClient.DeleteAsync($"{BaseUrl}threads/{threadId}");
        response.EnsureSuccessStatusCode();
    }
}