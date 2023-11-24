using Bogus;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakonczenie_tygodnia.Pages
{
    internal class AddClientPage
    {

        private IPage _page;

        private readonly ILocator _txtNip;
        private readonly ILocator _txtClientName;
        private readonly ILocator _txtDescription1;
        private readonly ILocator _txtDescription2;
        private readonly ILocator _txtStreet;
        private readonly ILocator _txtHouseNumber;
        private readonly ILocator _txtFaltNumber;
        private readonly ILocator _txtCity;
        private readonly ILocator _txtZipCode;
        private readonly ILocator _bttSave;
        private readonly ILocator _bttModified;



        public AddClientPage(IPage page)
        {
            _page = page;
            _txtNip = _page.GetByLabel("NIP");
            _txtClientName = _page.GetByLabel("Nazwa klienta");
            _txtDescription1 = _page.GetByLabel("Opis 1");
            _txtDescription2 = _page.GetByLabel("Opis 2");
            _txtStreet = _page.GetByLabel("Ulica");
            _txtFaltNumber = _page.GetByLabel("Nr lokalu");
            _txtHouseNumber = _page.GetByLabel("Nr domu");
            _txtZipCode = _page.GetByLabel("Kod pocztowy");
            _txtCity = _page.GetByLabel("Miejscowość");
            _bttSave = _page.GetByRole(AriaRole.Button, new() { Name = "Zapisz" });
            _bttModified = _page.GetByRole(AriaRole.Button, new() { Name = "Modyfikuj" });

        }

     

        public async Task FillClient(Client customer)
            {
            await _txtNip.FillAsync(customer.TaxId);
            await _txtClientName.FillAsync(customer.ClientName);
            await _txtDescription1.FillAsync(customer.Description1);
            await _txtDescription2.FillAsync(customer.Description2);
            await _txtStreet.FillAsync(customer.Street);
            await _txtFaltNumber.FillAsync(customer.FlatNb);
            await _txtHouseNumber.FillAsync(customer.HouseNb);   
            await _txtZipCode.FillAsync(customer.ZipCode);   
            await _txtCity.FillAsync(customer.City);
            await _bttSave.ClickAsync();
            }

        public async Task <bool> isModifiedExist() => await _bttModified.IsVisibleAsync();
        
    }

}
