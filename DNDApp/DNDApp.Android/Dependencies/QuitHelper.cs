using DNDApp.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(DNDApp.Droid.Dependencies.QuitHelper))]
namespace DNDApp.Droid.Dependencies
{
    class QuitHelper : IQuitHelper
    {
        public void Quit() => Java.Lang.Runtime.GetRuntime().Exit(-1);
    }
}