using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;
using System;
using System.IO;

namespace RestAPISampleAutoTestsSpecFlow.Hooks
{
    [Binding]
    public class ReportHooks
    {
        private static ExtentReports ExtentReport;

        private static ExtentTest Feature { get; set; }
        private ExtentTest Scenario { get; set; }
        private ExtentTest Step { get; set; }

        [BeforeTestRun]
        public static void GenerateExtentReport()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", $"Report_{timestamp}.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

            Console.WriteLine("Report Path: " + reportPath);

            var reporter = new ExtentSparkReporter(reportPath);
            ExtentReport = new ExtentReports();
            ExtentReport.AttachReporter(reporter);
        }

        [BeforeFeature]
        public static void AttachFeature(FeatureContext featureContext)
        {
            Feature = ExtentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void AttachScenario(ScenarioContext scenarioContext)
        {
            Scenario = Feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void AttachStep(ScenarioContext scenarioContext)
        {
            Step = Scenario.CreateNode(new GherkinKeyword(scenarioContext.CurrentScenarioBlock.ToString()), scenarioContext.StepContext.StepInfo.Text);
        }

        [AfterStep]
        public void AddStepResult(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                Step.Fail(scenarioContext.TestError);
            }
        }

        [AfterScenario]
        public void AttachScenarioResult(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                Scenario.Fail();
            }
        }

        [AfterFeature]
        public static void AttachFeatureResult(FeatureContext featureContext)
        {
            if (featureContext.TestError != null)
            {
                Feature.Fail();
            }
        }

        [AfterTestRun]
        public static void CloseExtentReport()
        {
            ExtentReport.Flush();
        }
    }
}