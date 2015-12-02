using Adam.Web.Extensions.Extensibility;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	public class CustomSearchPlugins
	{
		public static void Register()
		{
			RegisterSearchPlugin<Widgets.LoggedOnUsers.LoggedOnUsersPlugin>("ActiveUsers");
		}

		private static void RegisterSearchPlugin<TSearchPluginProvider>(string name)
		{
			if (!SearchPlugin.IsProviderRegistered(name))
			{
				SearchPlugin.RegisterProvider(name, typeof(TSearchPluginProvider));
			}
		}
	}
}