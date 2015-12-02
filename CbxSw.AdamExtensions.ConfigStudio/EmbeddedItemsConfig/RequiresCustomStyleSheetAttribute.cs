using Adam.Web.Extensions.UI;
using Adam.Web.Extensions.UI.Controls;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	/// <summary>
	/// When added to a <see cref="System.Web.UI.Control"/>, ADAM will make sure this embedded CSS is available on the page.
	/// </summary>
	public class RequiresCustomStyleSheetAttribute : RequiresStyleSheetAttribute
	{
		private readonly Css _styleSheet;

		/// <param name="styleSheet">The CSS style sheet that is required.</param>
		public RequiresCustomStyleSheetAttribute(Css styleSheet)
		{
			_styleSheet = styleSheet;
		}

		/// <summary>
		/// Gets the <see cref="T:Adam.Web.Extensions.UI.Controls.StyleSheet"/> required by the attribute.
		/// </summary>
		/// <returns>
		/// The <see cref="T:Adam.Web.Extensions.UI.Controls.StyleSheet"/> object required.
		/// </returns>
		protected override StyleSheet GetStyleSheet()
		{
			var styleSheet = StyleSheets.Get(_styleSheet);
			return styleSheet;
		}
	}
}