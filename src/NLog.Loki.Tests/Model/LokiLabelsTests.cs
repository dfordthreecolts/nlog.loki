using System;
using System.Collections.Generic;
using NLog.Loki.Model;
using NUnit.Framework;

namespace NLog.Loki.Tests.Model;

[TestFixture]
internal class LokiLabelsTests
{
    [Test]
    [TestCaseSource(nameof(GetEqualsTestCases))]
    public bool Equals(LokiLabels left, LokiLabels right)
    {
        return left.Equals(right);
    }

    public static IEnumerable<TestCaseData> GetEqualsTestCases()
    {
        yield return
            new TestCaseData(
                new LokiLabels(Array.Empty<LokiLabel>()),
                new LokiLabels(Array.Empty<LokiLabel>())).
                Returns(true);

        yield return
            new TestCaseData(
                new LokiLabels(new[] { new LokiLabel("env", "dev") }),
                new LokiLabels(Array.Empty<LokiLabel>())).
                Returns(false);

        yield return
            new TestCaseData(
                new LokiLabels(new[] { new LokiLabel("env", "dev") }),
                new LokiLabels(new[] { new LokiLabel("env", "dev") })).
                Returns(true);

        yield return
            new TestCaseData(
                    new LokiLabels(new List<LokiLabel> {
                        new LokiLabel("env", "dev"), new LokiLabel("level", "fatal") }),
                    new LokiLabels(new List<LokiLabel> {
                        new LokiLabel("level", "fatal"), new LokiLabel("env", "dev") })).
                Returns(true);

        yield return
            new TestCaseData(
                    new LokiLabels(new List<LokiLabel> {
                        new LokiLabel("env", "dev"), new LokiLabel("level", "fatal") }),
                    new LokiLabels(new List<LokiLabel> {
                        new LokiLabel("level", "fatal"), new LokiLabel("env", "dev"), new LokiLabel("severity", "debug") })).
                Returns(false);
    }
}
