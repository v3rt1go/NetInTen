using System;
using System.Threading.Tasks;

namespace NetInTen_Console
{
  public class Program
  {
    private static GitHubService _service;
    public static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the .Net Core GitHub API Client");
      Init();
      ShowMenu().Wait();
      Console.ReadLine();
      _service.Dispose();
    }

    private static void Init()
    {
      Console.WriteLine("Choose what API you want to use:\n");
      Console.WriteLine("1. GitHub public API");
      Console.WriteLine("2. GitHub authenticated API (you will need to provide an OAuth access token\n");
      var choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
          _service = new GitHubService();
          Console.WriteLine("GitHub Public API client initialized\n");
          break;
        case "2":
          Console.WriteLine("Please provide public access OAuth token:");
          var token = Console.ReadLine();
          _service = new GitHubService(token);
          Console.WriteLine("GitHub Authenticated API client initialized\n");
          break;
        default:
          Console.WriteLine("Not a valid choice. Initializing GitHub public API\n");
          _service = new GitHubService();
          break;
      }
    }

    private static async Task DelegateToAction(string menuOption)
    {
      switch (menuOption)
      {
        case "1":
          await _service.SearchUser();
          break;
        case "2":

        case "9":
          await _service.CreateRepo();
          break;
        default:
          Console.WriteLine("Wrong menu action!");
          Init();
          break;
      }

      await ShowMenu();
    }

    private static async Task ShowMenu()
    {
      Console.WriteLine("Please select an action to proceed:");
      Console.WriteLine("1. Search for a GitHub user");
      Console.WriteLine("2. List a user details");
      Console.WriteLine("3. List a user's repositories");
      Console.WriteLine("4. List repository details");
      Console.WriteLine("5. List repository issues");
      Console.WriteLine("6. List repository pull requests");
      Console.WriteLine("7. List issue details");
      Console.WriteLine("8. List pull request details");

      if (_service.IsAuthenticated)
        Console.WriteLine("9. Create a new GitHub repo");

      Console.WriteLine("\n");
      var menuOption = Console.ReadLine();
      await DelegateToAction(menuOption);
    }
  }
}
