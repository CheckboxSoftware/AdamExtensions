using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using Adam.Core;
using Adam.Core.Tools;
using Adam.Web.Administration.Providers;
using Adam.Web.Tools.Fluent;

namespace CbxSw.RegisteredAssemblyVersion
{
	[DisplayName("Assembly versions")]
	public class RegisteredAssembliesVersion : AdministrationProvider
	{
		public RegisteredAssembliesVersion(Application app)
			: base(app)
		{
		}

		protected override string Description => "Shows the version of the registered assemblies.";
		protected override string Title => "Assembly versions";
		protected override string VendorName => "Checkbox software";
		protected override string VendorUrl => "http://www.checkboxsoftware.net/";

		protected override MasterResetItem[] GetMasterResetItems()
		{
			return new MasterResetItem[0];
		}

		protected override void CreateControlHierarchy()
		{
			this.AddGeneric("table", tbl =>
			{
				var noAssembliesRegistered = true;
				tbl.AddGeneric("tr", tr =>
				{
					AddHeaderCell(tr, "Assembly");
					AddHeaderCell(tr, "Version in ADAM");
					AddHeaderCell(tr, "Version on file");
				});

				var providerHelper = new ProviderHelper(App);
				var registeredAssemblies = providerHelper.GetRegisteredAssemblyNames();
				foreach (var registeredAssemblyName in registeredAssemblies)
				{
					var adamGacVersion = GetAdamGacVersion(providerHelper, registeredAssemblyName);
					var binFileVersion = GetBinFileVersion(registeredAssemblyName);

					AddAssembly(tbl, registeredAssemblyName, adamGacVersion, binFileVersion);
					noAssembliesRegistered = false;
				}
				if (noAssembliesRegistered)
				{
					NoRegisteredAssemblies(tbl);
				}
			});

		}

		private static void NoRegisteredAssemblies(Control tbl)
		{
			tbl.AddGeneric("tr", tr =>
			{
				tr.AddGeneric("td", noDataCell =>
				{
					noDataCell.Attributes["colspan"] = "3";
					noDataCell.AddLiteral("No assemblies registered in ADAM");
				});
			});
		}

		private void AddAssembly(Control table, string assemblyName, Version adamGacVersion, string binFileVersion)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddGeneric("td", td => td.AddLiteral(assemblyName));
				row.AddGeneric("td", td => td.AddLiteral(adamGacVersion?.ToString() ?? string.Empty));
				row.AddGeneric("td", td => td.AddLiteral(binFileVersion));
			});
		}

		private string GetBinFileVersion(string registeredAssemblyName)
		{
			var fileName = $"{registeredAssemblyName}.dll";
			var binFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), fileName);
			if (!File.Exists(binFilePath))
				return "not in bin folder";

			return FileVersionInfo.GetVersionInfo(binFilePath).FileVersion;
		}

		private Version GetAdamGacVersion(ProviderHelper providerHelper, string registeredAssemblyName)
		{
			var assembly = providerHelper.GetRegisteredAssembly(registeredAssemblyName);
			return assembly.GetName().Version;
		}

		private void AddHeaderCell(Control row, string headerText)
		{
			row.AddGeneric("th", th =>
			{
				th.AddLiteral(headerText);
			});
		}
	}
}
