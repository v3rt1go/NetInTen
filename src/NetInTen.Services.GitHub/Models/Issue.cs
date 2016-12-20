using System;
using System.Collections.Generic;
using NetInTen_Console.Models;
using Newtonsoft.Json;

namespace NetInTen.Services.GitHub.Models
{
  public class Issue
  {
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("repository_url")]
    public string RepositoryUrl { get; set; }

    [JsonProperty("labels_url")]
    public string LabelsUrl { get; set; }

    [JsonProperty("comments_url")]
    public string CommentsUrl { get; set; }

    [JsonProperty("events_url")]
    public string EventsUrl { get; set; }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("number")]
    public int Number { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }

    [JsonProperty("labels")]
    public IList<Label> Labels { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("locked")]
    public bool Locked { get; set; }

    [JsonProperty("assignees")]
    public IList<object> Assignees { get; set; }

    [JsonProperty("milestone")]
    public Milestone Milestone { get; set; }

    [JsonProperty("comments")]
    public int Comments { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("pull_request")]
    public PullRequest PullRequest { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }
  }
}
