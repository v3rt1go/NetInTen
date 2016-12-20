using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetInTen.Services.GitHub;
using NetInTen.Services.GitHub.Models;
using NetInTen_Console.Models;
using NetInTen_Console.ViewModels;

namespace NetInTen_Console
{
  public class GitHubService
  {
    private readonly GitHubApiClient _client;
    private readonly IMapper _mapper;

    public GitHubService(string token = "")
    {
      _client = string.IsNullOrEmpty(token)
        ? new GitHubApiClient()
        : new GitHubApiClient(token);

      var config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<User, UserViewModel>();
      });
      _mapper = config.CreateMapper();
    }

    public bool IsAuthenticated => _client.IsAuthenticated;

    /// <summary>
    /// Performs the search by username and lists the first 3 results to the Console
    /// </summary>
    /// <returns></returns>
    public async Task SearchUser()
    {
      Console.WriteLine("Please provide the search query:");
      var query = Console.ReadLine();

      var results = await _client.SearchUsers(query);
      var totalResults = results.Count();
      var listings = results.Take(3).Select(user => _mapper.Map<UserViewModel>(user));

      Console.WriteLine($"Found {totalResults} results.");
      Console.WriteLine("Listing first 3 results:\n");

      foreach (var userViewModel in listings)
      {
        userViewModel.ListToConsole();
      }
    }

    public async Task CreateRepo()
    {
      Console.WriteLine("Please provide the repository name:");
      var repoName = Console.ReadLine();
      Console.WriteLine("Please provide the repository description");
      var repoDesc = Console.ReadLine();

      var repo = new Repo()
      {
        Name = repoName,
        Description = repoDesc,
        Private = false,
        HasIssues = true,
        HasDownloads = true,
        HasWiki = false
      };

      var newRepo = await _client.CreateNewRepo(repo);
      if (newRepo != null)
      {
        Console.WriteLine("Repository created successfuly!\n");
        Console.WriteLine($"Name: {newRepo.Name} \n Url: {newRepo.Url} \n Owner: {newRepo.Owner.Login}");
      }
    }

    public void Dispose()
    {
      _client?.Dispose();
    }
  }
}
