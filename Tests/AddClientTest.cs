using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Zakonczenie_tygodnia.Pages;

namespace Zakonczenie_tygodnia.Tests
{

    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class ClientTest : PageTest
    {
        public Faker _faker;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _faker = new Faker("pl");
        }

        [SetUp]

        public async Task SetUp()
        {
            await Page.GotoAsync(url: "https://test.riskradar.pl");

        }


        [Test]

        public async Task AddClient()
        {
            //wywołanie metody logowania
            var loginPage = new LoginPage(Page);
            await loginPage.Login(userName: "e.lekka@test.pl", password: "Wiosna.123456");
            var isExist = await loginPage.IsMenuExist();
            Assert.IsTrue(isExist);

            // wywołanie metody klikającej w "Dodaj klienta"
            var clients = new ClientsPage(Page);
            var addClientPg = await clients.AddClientButton();



            //wywołanie metody uzupełnij  NIP
            AddClientPage addclient = new AddClientPage(addClientPg);
            var customer = CustomerData();
            await addclient.FillClient(customer);
           

            var isModifiedExist = await addclient.isModifiedExist();
            Assert.IsTrue(isExist);
        }

        private Client CustomerData()
        {
            return new Client()
            {
                TaxId = "7614692304",
                ClientName = _faker.Company.CompanyName(),
                Description1 = "Opis1",
                Description2 = "Opis2",
                Street = _faker.Address.StreetName(),
                FlatNb = _faker.Address.BuildingNumber(),
                HouseNb = _faker.Address.BuildingNumber(),
                ZipCode = _faker.Address.ZipCode(),
                City = _faker.Address.City()
            };
            
    }


}
    }
