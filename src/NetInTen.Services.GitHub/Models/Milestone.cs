using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetInTen_Console.Models;
using Newtonsoft.Json;

namespace NetInTen.Services.GitHub.Models
{
  public class Milestone
  {
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; }

    [JsonProperty("labels_url")]
    public string LabelsUrl { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("number")]
    public int Number { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("creator")]
    public User Creator { get; set; }

    [JsonProperty("open_issues")]
    public int OpenIssues { get; set; }

    [JsonProperty("closed_issues")]
    public int ClosedIssues { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("due_on")]
    public object DueOn { get; set; }

    [JsonProperty("closed_at")]
    public object ClosedAt { get; set; }
  }
}
