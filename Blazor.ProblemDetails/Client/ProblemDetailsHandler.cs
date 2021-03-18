using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.ProblemDetails.Client
{
  public class ProblemDetailsHandler : DelegatingHandler
  {
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var response = await base.SendAsync(request, cancellationToken);

      if (response.IsSuccessStatusCode)
      {
        return response;
      }
      else
      {
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: cancellationToken);
        throw new ProblemDetailsException(problemDetails);
      }
    }
  }
}
