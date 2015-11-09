using Adam.Web.Studio;
using Adam.Web.Studio.Extensibility;
using Adam.Web.Studio.Routing;

namespace CbxSw.AdamExtensions
{
	public class StudioStartup : StudioExtension
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
			// TODO: add pages for searching, adding and editing inheritance
			//Routes.AddPage<Products.Inheritance.InheritanceSearchPage>("FieldInheritances");
			//Routes.AddPage<Products.Inheritance.InheritanceCreatePage>("FieldInheritances/+");
			Routes.AddPage<Products.Inheritance.InheritanceViewPage>("FieldInheritances/{id}");
			//Routes.AddPage<Products.Inheritance.InheritanceEditPage>("FieldInheritances/{id}/Edit");
		}
	}
}