using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetInTen_Console.ViewModels
{
  public class UserViewModel
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Url { get; set; }

    public void ListToConsole()
    {
      Console.WriteLine($"Login: {Login} | Id: {Id}");
      Console.WriteLine($"Profile URL: {Url}\n");
    }
  }
}
