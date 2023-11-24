using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakonczenie_tygodnia.Pages
{
    internal class AddUserPage
    {
        private IPage _page;
        private readonly ILocator _userName;
        private readonly ILocator _userSurname;
        private readonly ILocator _userEmail;
        private readonly ILocator _userPhone;
        private readonly ILocator _chceckCompanyWorker;
        private readonly ILocator _saveBtn;
        private readonly ILocator _modifyBtn;

        public AddUserPage(IPage page)

        {
            _page = page;
            _userName = _page.GetByLabel("Imię");
            _userSurname = _page.GetByLabel("Nazwisko");
            _userEmail = _page.GetByLabel("Adres e-mail");
            _userPhone = _page.GetByLabel("Numer telefonu (9 cyfr)");
            _chceckCompanyWorker = _page.GetByLabel("Pracownik firmy", new() { Exact = true });
            _saveBtn = _page.GetByRole(AriaRole.Button, new() { Name = "Zapisz" });
            _modifyBtn = _page.Locator(selector: "text='Modyfikuj'");

        }

        public async Task FillUser(User User)
        {
            await _userName.FillAsync(User.Name);
            await _userSurname.FillAsync(User.Surname);
            await  _userEmail.FillAsync(User.Email+"abc");
            await _userPhone.FillAsync(User.PhoneNumber);
        }

        public async Task ClickChceckBox() => await _chceckCompanyWorker.ClickAsync();

        public async Task SaveBtnClick()
        {
            await _saveBtn.ClickAsync();
            Thread.Sleep(1000);

        }
   
        public async Task<bool> isModifiedExist()

        {
            bool modifyBtnVidible;
            modifyBtnVidible = await _modifyBtn.IsVisibleAsync();
            return modifyBtnVidible;
            //Console.WriteLine("Jestem w sprawdzaniu czy widoczny jest przycisk Modyfikuj: " + modifyBtnVidible);

        }

        public async Task <ILocator> isModified() 
        {
            return  _saveBtn;
        }
    }
}
