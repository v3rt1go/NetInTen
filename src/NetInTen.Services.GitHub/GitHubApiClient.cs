using System;
using NetInTen_Console.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NetInTen.Services.GitHub.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetInTen.Services.GitHub
{
  public class GitHubApiClient
  {
    private readonly HttpClient _client;
    private const string ApiBase = "https://api.github.com/";

    // Public api client
    public GitHubApiClient()
    {
      _client = new HttpClient();
      _client.BaseAddress = new Uri(ApiBase);
      _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
      _client.DefaultRequestHeaders.Add("User-Agent", "GitHubDummyApi");
    }

    // Authenticated api client
    public GitHubApiClient(string token) : this()
    {
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
      IsAuthenticated = true;
    }

    public bool IsAuthenticated { get; private set; }

    /// <summary>
    /// Searches for GitHub users by given query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<List<User>> SearchUsers(string query)
    {
      var userResults = new List<User>();
      var searchUsersCall = await GetRequestAsync($"search/users?q={query}");

      JObject usersSearch = JObject.Parse(await searchUsersCall.Content.ReadAsStringAsync());
      IList<JToken> searchResults = usersSearch["items"].Children().ToList();

      userResults.AddRange(searchResults.Select(result => JsonConvert.DeserializeObject<User>(result.ToString())));
      return userResults;
    }

    /// <summary>
    /// Lists a github user details
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<User> ListUserDetails(string username)
    {
      var userCall = await GetRequestAsync($"users/{username}");
      var user = JsonConvert.DeserializeObject<User>(await userCall.Content.ReadAsStringAsync());
      return user;
    }

    /// <summary>
    /// Lists a github user's public repos
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<List<Repo>> ListUserRepos(string username)
    {
      var reposCall = await GetRequestAsync($"users/{username}/repos");
      var reposList = JsonConvert.DeserializeObject<List<Repo>>(await reposCall.Content.ReadAsStringAsync());
      return reposList;
    }

    /// <summary>
    /// Lists a public repository details for a github user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="repo"></param>
    /// <returns></returns>
    public async Task<Repo> ListRepoDetails(string username, string repo)
    {
      var repoDetailsCall = await GetRequestAsync($"repos/{username}/{repo}");
      var repoDetails = JsonConvert.DeserializeObject<Repo>(await repoDetailsCall.Content.ReadAsStringAsync());
      return repoDetails;
    }

    /// <summary>
    /// Lists all issues for a public github repo
    /// </summary>
    /// <param name="username"></param>
    /// <param name="repo"></param>
    /// <returns></returns>
    public async Task<List<Issue>> ListRepoIssues(string username, string repo)
    {
      var repoIssuesCall = await GetRequestAsync($"repos/{username}/{repo}/issues");
      var repoIssues = JsonConvert.DeserializeObject<List<Issue>>(await repoIssuesCall.Content.ReadAsStringAsync());
      return repoIssues;
    }

    /// <summary>
    /// Lists issue details for a given github public repo
    /// </summary>
    /// <param name="username"></param>
    /// <param name="repo"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public async Task<Issue> ListIssueDetails(string username, string repo, string number)
    {
      var issueDetailsCall = await GetRequestAsync($"repos/{username}/{repo}/issues/{number}");
      var issueDetails = JsonConvert.DeserializeObject<Issue>(await issueDetailsCall.Content.ReadAsStringAsync());
      return issueDetails;
    }

    /// <summary>
    /// Lists all pull requests for a public github repo
    /// </summary>
    /// <param name="username"></param>
    /// <param name="repo"></param>
    /// <returns></returns>
    public async Task<List<PullRequest>> ListRepoPulls(string username, string repo)
    {
      var repoPullsCall = await GetRequestAsync($"repos/{username}/{repo}/pulls");
      var repoPulls = JsonConvert.DeserializeObject<List<PullRequest>>(await repoPullsCall.Content.ReadAsStringAsync());
      return repoPulls;
    }

    /// <summary>
    /// List a pull request's details for a public github repo
    /// </summary>
    /// <param name="username"></param>
    /// <param name="repo"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public async Task<PullRequest> ListPullRequestDetails(string username, string repo, string number)
    {
      var pullDetailsCall = await GetRequestAsync($"repos/{username}/{repo}/pulls/{number}");
      var pullRequestDetails = JsonConvert.DeserializeObject<PullRequest>(await pullDetailsCall.Content.ReadAsStringAsync());
      return pullRequestDetails;
    }

    /// <summary>
    /// Creates a new github public repository with the given details
    /// </summary>
    /// <param name="repo">The Repo object</param>
    /// <returns></returns>
    public async Task<Repo> CreateNewRepo(Repo repo)
    {
      var settings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None,
        DefaultValueHandling = DefaultValueHandling.Ignore
      };

      var content = new StringContent(JsonConvert.SerializeObject(repo, settings), Encoding.UTF8, "application/json");
      var newRepoCall = await _client.PostAsync("user/repos", content);
      if (!newRepoCall.IsSuccessStatusCode)
        return null;

      var newRepo = JsonConvert.DeserializeObject<Repo>(await newRepoCall.Content.ReadAsStringAsync());
      return newRepo;
    }

    public void Dispose()
    {
      _client?.Dispose();
    }

    private async Task<HttpResponseMessage> GetRequestAsync(string url)
    {
      var resp = await _client.GetAsync(url);
      resp.EnsureSuccessStatusCode();

      return resp;
    }
  }
}
