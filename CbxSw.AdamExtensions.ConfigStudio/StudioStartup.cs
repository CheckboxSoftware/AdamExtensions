using Adam.Web.Studio;
using Adam.Web.Studio.Extensibility;
using Adam.Web.Studio.Routing;
using CbxSw.AdamExtensions.ConfigStudio.Products.Inheritance;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	/// <summary>
	/// A class that we will configure in the Web.Config of ConfigStudio, telling ADAM to call <see cref="Configure"/> when the studio starts.
	/// </summary>
	/// <remarks>
	/// To get the extension to work, open the web.config and find the section: /configuration/adam.web.studio.
	/// Then add the following code:
	/// <code>
	///   &lt;studioExtensions&gt;
	///	    &lt;providers&gt;
	///	      &lt;add type = "CbxSw.AdamExtensions.ConfigStudio.Startup, CbxSw.AdamExtensions" /&gt;
	///     &lt;/providers&gt;
	///   &lt;/studioExtensions&gt;
	/// </code>
	/// </remarks>
	public class Startup : StudioExtension
	{
		private static readonly object _lock = new object();
		private static bool _hasStarted = false;

		public override void Configure(HttpStudioApplication application)
		{
			RunsOnceAtStudioStartup();
		}

		private void RunsOnceAtStudioStartup()
		{
			lock (_lock) // making sure only one thread can run the startup code at a time
			{
				if (_hasStarted) // if startup code has ran before: no need to run it again
				{
					return;
				}
				_hasStarted = true;
				RegisterRoutes();
			}
		}

		protected virtual void RegisterRoutes()
		{
			RegisterInheritanceRoutes();
		}

		private static void RegisterInheritanceRoutes()
		{
			// HINT: comment the code below if you don't have a license for Products
			const string routePrefix = "FieldInheritance";
			// TODO: add edit page
			Routes.AddPage<InheritanceSearchPage>(routePrefix);
			Routes.AddPage<InheritanceViewPage>(routePrefix + "/{id}");
			//Routes.AddPage<Products.Inheritance.InheritanceEditPage>(routePrefix + "/{id}/Edit");
		}
	}
}