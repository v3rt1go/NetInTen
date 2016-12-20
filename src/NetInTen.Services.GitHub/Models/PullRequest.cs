using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetInTen.Services.GitHub.Models
{
  public class PullRequest
  {
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; }

    [JsonProperty("diff_url")]
    public string DiffUrl { get; set; }

    [JsonProperty("patch_url")]
    public string PatchUrl { get; set; }
  }
}
