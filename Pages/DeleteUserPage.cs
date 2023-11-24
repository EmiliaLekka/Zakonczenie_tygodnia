using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zakonczenie_tygodnia.Pages
{
    internal class DeleteUserPage
    {
        private IPage _page;
        private readonly ILocator _deleteBtn;
        private readonly ILocator _popUp;
        private readonly ILocator _yesBtnOnPopUp;


        public DeleteUserPage(IPage page)
        {
            _page = page;
            _deleteBtn = _page.GetByRole(AriaRole.Button, new() { Name = "Usuń" });
            _popUp = _page.Locator("div").Filter(new() { HasTextRegex = new Regex("^Czy chcesz trwale usunąć konto użytkownika z Aplikacji RiskRadar\\?$") }).First;
            _yesBtnOnPopUp = _page.GetByRole(AriaRole.Button, new() {Name = "Tak"});
        }


       
        public async Task DeleteUser () 
        
        {
            
            await _deleteBtn.IsVisibleAsync();
            Thread.Sleep(1000);
            await _deleteBtn.ClickAsync();
            Thread.Sleep(1000);
            await _popUp.IsVisibleAsync();
            Thread.Sleep(1000);
             await _yesBtnOnPopUp.ClickAsync();
            Thread.Sleep(3000);



        }
    }
}
