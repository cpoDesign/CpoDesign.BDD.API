using CpoDesign.BDD.API.CpoDesign;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    public sealed class HealthcheckStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public HealthcheckStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"A call to the endpoint using template file '(.*)'")]
        public void GivenAnonymousCallToEndpointUsingTemplateFile(string templateFileName)
        {
            var templateFolderExists = TemplateTester.CheckTemplateFolderExists();

            if (!templateFolderExists)
            {
                Assert.Fail("Failed to find template folder in project");
            }

            var templateFilePath = TemplateTester.GetTemplatePathIfExists(templateFileName);

            if (string.IsNullOrWhiteSpace(templateFilePath))
            {
                Assert.Fail("Failed to find template file in project");
            }

            _scenarioContext.Set(templateFilePath, ContextDefinedValues.TemplateFilePath);
        }

        [When(@"make request using the template")]
        public void WhenWhenMakeRequestUsingTheTemplate()
        {
            var path = _scenarioContext.Get<string>(ContextDefinedValues.TemplateFilePath);

            var templateData = TemplateTester.LoadTemplateData(path);

            if (string.IsNullOrWhiteSpace(templateData))
            {
                Assert.Fail("Failed to load template data");
            }

            Postmanv3template template = JsonConvert.DeserializeObject<Postmanv3template>(templateData);
            var templateItem = template.item[0];

            if (templateItem._event != null)
            {
                foreach (Event eventType in templateItem._event)
                {
                    if (eventType.listen == "test")
                    {
                        _scenarioContext.Set(eventType.script, ContextDefinedValues.ResponseTests);
                    }
                }
            }

            var request = templateItem.request;

            var requestMethod = request.method;
            var urlTemplate = request.url.raw;

            var url = ConfigurationParser.GetFullUrlFromTemplate(urlTemplate);
            _scenarioContext.Set(requestMethod, ContextDefinedValues.RequestMethod);

            WebRequestResult testResult = WebRequestProvider.MakeRequest(requestMethod, url, request.header);

            _scenarioContext.Set(JsonConvert.SerializeObject(testResult), ContextDefinedValues.RequestResult);
        }

        [Then(@"result should contain the word '(.*)'")]
        public void ThenResultShouldContainTheWord(string p0)
        {
            string resultString = _scenarioContext.Get<string>(ContextDefinedValues.RequestResult);

            var result = JsonConvert.DeserializeObject<WebRequestResult>(resultString);

            if (!result.Executed)
            {
                Assert.Fail("Failed to execute the request");
            }

            if (!result.Content.Contains(p0))
            {
                Assert.Fail($"Request result does not contain {p0}");
            }
        }

        //[Then(@"a result is recieved as defined in template")]
        //public void ThenThenASuccessfullResultIsRecieved()
        //{
        //    string resultString = _scenarioContext.Get<string>(ContextDefinedValues.RequestResult);

        //    var result = JsonConvert.DeserializeObject<WebRequestResult>(resultString);

        //    if (!result.Executed)
        //    {
        //        Assert.Fail("Failed to execute the request");
        //    }
        //}
    }

    public class ContextDefinedValues
    {
        public static string TemplateFilePath => nameof(TemplateFilePath);
        public static string TemplateData => nameof(TemplateData);
        public static string RequestMethod => nameof(RequestMethod);
        public static string ResponseTests => nameof(ResponseTests);
        public static string RequestResult => nameof(RequestResult);
    }
    public class ConfigurationParser
    {
        internal static string GetFullUrlFromTemplate(string urlTemplate)
        {
            string baseUrlConfigurationName = GetTemplateName(urlTemplate);

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //        .SetBasePath(PathProvider.AssemblyDirectory)
            //        .AddJsonFile("appsettings.json")
            //        .Build();

            //TODO: implement configuration properly
            //var baseConfigurationValue = configuration.GetValue<string>(baseUrlConfigurationName);

            var baseConfigurationValue = "http://pavelsvarc-001-site5.gtempurl.com/";

            /*
             IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath([PATH_WHERE_appsettings.json_RESIDES])
            .AddJsonFile("appsettings.json")
            .Build();
             */

            var stringTemplate = "{{" + baseUrlConfigurationName + "}}";

            return urlTemplate.Replace(stringTemplate, baseConfigurationValue);
        }

        private static string GetTemplateName(string urlTemplate)
        {
            int startFirst = urlTemplate.IndexOf("{") + 1;
            int start = urlTemplate.IndexOf("{", startFirst) + 1;
            int end = urlTemplate.IndexOf("}", start);
            int endTwo = urlTemplate.IndexOf("}", end);

            return urlTemplate.Substring(start, endTwo - start);
        }
    }
}
