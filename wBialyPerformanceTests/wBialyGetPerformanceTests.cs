using NBomber.CSharp;
using NBomber.Http.CSharp;
using Xunit.Abstractions;
using Xunit;

namespace wBialyPerformanceTests
{
    public class wBialyGetPerformanceTests
    {
        private readonly ITestOutputHelper _outputHelper;
        [Fact]
        public void get_lfposts_should_handle_at_least_100_requests_per_second()
        {
            var httpClient = new HttpClient();
            const string url = "http://wbialyamogus-001-site1.atempurl.com/api/post/lfposts/?pageSize=5&pageNumber=1&LfFlag=lost";
            const int rate = 100;
            const int duration = 120;
            var scenario = Scenario.Create("get_lf_posts", async context =>
            {
                var request = Http.CreateRequest("GET", url);
                var response = await Http.Send(httpClient, request);
                return response;

            }).WithLoadSimulations(
                    Simulation.Inject(
                        rate: rate,
                        interval: TimeSpan.FromSeconds(1),
                        during: TimeSpan.FromSeconds(duration)));
            var stats = NBomberRunner.RegisterScenarios(scenario)
                .Run();
            var scenarioStats = stats.ScenarioStats[0];
            _outputHelper.WriteLine($"OK {stats.AllOkCount}, FAILED: {stats.AllFailCount}");

            Assert.True(stats.AllOkCount >= duration * rate, "Too slow");
        }
        public wBialyGetPerformanceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
    }
}
