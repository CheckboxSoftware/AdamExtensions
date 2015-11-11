using System.Collections.Generic;
using System.Web.UI;
using Adam.Web.Extensions.UI;
using Adam.Web.Extensions.UI.Controls;
using CbxSw.AdamExtensions.ConfigStudio;

[assembly: WebResource(EmbeddedCss.Inheritance, "text/css", PerformSubstitution = true)]

namespace CbxSw.AdamExtensions.ConfigStudio
{
	public static class EmbeddedCss
	{
		private const string _rootNamespace = "CbxSw.AdamExtensions.ConfigStudio.";

		public const string Inheritance = _rootNamespace + "Products.Inheritance.Inheritance.css";
	}

	public enum Css
	{
		Inheritance
	}

	public static class StyleSheets
	{
		private static readonly Dictionary<Css, EmbeddedResourceStyleSheet> _styleSheets;

		static StyleSheets()
		{
			var type = typeof(StyleSheets);
			_styleSheets = new Dictionary<Css, EmbeddedResourceStyleSheet>
			{
				{ Css.Inheritance, new EmbeddedResourceStyleSheet(type, EmbeddedCss.Inheritance) },
			};
		}

		public static StyleSheet Get(Css styleSheet)
		{
			return _styleSheets[styleSheet];
		}

		public static string UrlFor(Control control, Css styleSheet)
		{
			var sheet = _styleSheets[styleSheet];
			return control.Page.ClientScript.GetWebResourceUrl(sheet.ResourceType, sheet.ResourceName);
		}
	}

	public class RequiresCustomStyleSheetAttribute : RequiresStyleSheetAttribute
	{
		private readonly Css _styleSheet;

		public RequiresCustomStyleSheetAttribute(Css styleSheet)
		{
			_styleSheet = styleSheet;
		}

		protected override StyleSheet GetStyleSheet()
		{
			var styleSheet = StyleSheets.Get(_styleSheet);
			return styleSheet;
		}
	}
}