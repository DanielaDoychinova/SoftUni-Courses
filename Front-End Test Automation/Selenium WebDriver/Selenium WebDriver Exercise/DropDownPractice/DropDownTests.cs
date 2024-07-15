using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DropDownPractice
{
    public class Tests
{
    WebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }


    [Test]
    public void ExtractInformationBasedOnDropDownOption()
    {
        string path = Directory.GetCurrentDirectory() + "/manufacturers.txt";


        if (File.Exists(path))
        {
            File.Delete(path);
        }

        SelectElement dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name=\"manufacturers\"]//select")));

        IList<IWebElement> options = dropdown.Options;

        List<string> optionsAsString = new List<string>();

        foreach (IWebElement option in options)
        {
            optionsAsString.Add(option.Text);
        }

        optionsAsString.RemoveAt(0);

        foreach (string option in optionsAsString)
        {
            dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));
            dropdown.SelectByText(option);
            if (driver.PageSource.Contains("There are no products available" +
                "in this category."))
            {
                File.AppendAllText(path, $"The manufacturer {option} has no products.");
            }
            else
            {
                IWebElement productsTable = driver.FindElement(By.ClassName("productListingHeader"));

                File.AppendAllText(path, $"\n\n The manufacturer {option} products are listed below -- \n");

                IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody/tr"));

                foreach (IWebElement row in tableRows)
                {
                    File.AppendAllText(path, row.Text + "\n");

                }
            }
        }
    }
}
}
