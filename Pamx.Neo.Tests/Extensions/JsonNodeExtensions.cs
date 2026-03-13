using System.Text.Json.Nodes;
using FluentAssertions.Execution;
using Pamx.Tests.Assertions;

namespace Pamx.Tests.Extensions;

public static class JsonNodeExtensions
{
    public static JsonNodeAssertions Should(this JsonNode? instance)
    {
        return new JsonNodeAssertions(instance, AssertionChain.GetOrCreate());
    }
}