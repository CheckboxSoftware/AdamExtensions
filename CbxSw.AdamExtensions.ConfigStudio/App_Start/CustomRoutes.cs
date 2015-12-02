using Adam.Web.Studio.Routing;
using CbxSw.AdamExtensions.ConfigStudio.Products.Inheritance;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	public class CustomRoutes
	{
		public static void Register()
		{
			// HINT: comment the code below if you don't have a license for Products
			RegisterProductInheritanceRoutes();
		}

		private static void RegisterProductInheritanceRoutes()
		{
			const string routePrefix = "FieldInheritance";
			// TODO: add edit page
			Routes.AddPage<InheritanceSearchPage>(routePrefix);
			Routes.AddPage<InheritanceViewPage>(routePrefix + "/{id}");
			//Routes.AddPage<Products.Inheritance.InheritanceEditPage>(routePrefix + "/{id}/Edit");
		}
	}
}