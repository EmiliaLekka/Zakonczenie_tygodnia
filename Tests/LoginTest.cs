using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Zakonczenie_tygodnia.Pages;

namespace Zakonczenie_tygodnia.Tests
{

    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class Tests : PageTest
    {
        [SetUp]

        public async Task SetUp()
        {
            await Page.GotoAsync(url: "http://test.riskradar.pl");

        }

        [Test]
        public async Task MyTest()
        {

            LoginPage  loginPage = new LoginPage(Page);
            await loginPage.Login(userName:"e.lekka@test.pl", password:"Wiosna.123456");

            var isExist = await loginPage.IsMenuExist();
            Assert.IsTrue(isExist);

         }

      

    }
}
