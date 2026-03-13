using System.Text.Json.Nodes;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Pamx.Tests.Assertions;

public sealed class JsonNodeAssertions(JsonNode? instance, AssertionChain assertionChain)
    : ReferenceTypeAssertions<JsonNode?, JsonNodeAssertions>(instance, assertionChain)
{
    protected override string Identifier => "jsonNode";

    public AndConstraint<JsonNodeAssertions> BeIdenticalTo(JsonNode? expected, string path = "$", int maxFailures = 50)
    {
        var failureCount = 0;

        using (new AssertionScope())
        {
            CompareRecursive(Subject, expected, path, ref failureCount, maxFailures);
        }

        return new AndConstraint<JsonNodeAssertions>(this);
    }

    private static void CompareRecursive(JsonNode? actual,
        JsonNode? expected,
        string path,
        ref int failureCount,
        int maxFailures)
    {
        if (failureCount >= maxFailures) return;

        if (actual == null && expected == null)
            return;

        if (actual == null || expected == null)
        {
            RecordFailure(path,
                ref failureCount,
                "Expected {context:jsonNode} at {0} to be {1}, but found {2}.",
                path,
                (object?)expected ?? "null",
                (object?)actual ?? "null");

            return;
        }

        switch (actual)
        {
            case JsonObject actObj when expected is JsonObject expObj:
            {
                var keys = expObj.Select(x => x.Key).Union(actObj.Select(x => x.Key)).Distinct();
                foreach (var key in keys)
                {
                    CompareRecursive(actObj[key], expObj[key], $"{path}.{key}", ref failureCount, maxFailures);
                    if (failureCount >= maxFailures) return;
                }

                return;
            }
            case JsonArray actArr when expected is JsonArray expArr:
            {
                if (actArr.Count != expArr.Count)
                {
                    RecordFailure(path,
                        ref failureCount,
                        "Expected array length at {0} to be {1}, but found {2}.",
                        path,
                        expArr.Count,
                        actArr.Count);
                }

                var minCount = Math.Min(actArr.Count, expArr.Count);
                for (var i = 0; i < minCount; i++)
                {
                    CompareRecursive(actArr[i], expArr[i], $"{path}[{i}]", ref failureCount, maxFailures);
                    if (failureCount >= maxFailures) return;
                }

                return;
            }
        }

        if (!JsonNode.DeepEquals(actual, expected))
        {
            RecordFailure(path,
                ref failureCount,
                "Expected {context:jsonNode} at {0} to be {1}, but found {2}.",
                path,
                expected,
                actual);
        }
    }

    private static void RecordFailure(string path, ref int failureCount, string failureMessage, params object[] args)
    {
        failureCount++;

        AssertionChain.GetOrCreate()
            .BecauseOf(path)
            .FailWith(failureMessage, args);
    }
}