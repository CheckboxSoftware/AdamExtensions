using System.Collections.Generic;
using System.Web.UI;
using Adam.Web;
using Adam.Web.Studio.Providers.Widgets;

namespace CbxSw.AdamExtensions.ConfigStudio.Widgets.LoggedOnUsers
{
	/// <summary>
	/// A widget that will display the currently active users.
	/// </summary>
	public class LoggedOnUsersWidget : ContextSearchWidget
	{
		/// <summary>
		/// The name of the search provider ID.
		/// </summary>
		/// <returns></returns>
		protected override string ResolveSearchProviderID()
		{
			return "ActiveUsers";
		}

		/// <summary>
		/// The targetted studio.
		/// </summary>
		protected override RegisteredStudio TargetStudio => RegisteredStudio.ConfigStudio;

		/// <summary>
		/// Describes the script descriptor.
		/// </summary>
		/// <param name="descriptor">The descriptor.</param>
		protected override void DescribeScriptDescriptor(ScriptControlDescriptor descriptor)
		{
			base.DescribeScriptDescriptor(descriptor);
			descriptor.AddProperty("urlToItem", "Users");
			descriptor.AddProperty("template", "<div class=\"title\">{=username}</div><div class=\"info\"><span>Last access</span>{=lastAccessOn}</div><div class=\"info\"><span>Site</span>{=siteName}</div>");
		}

		protected override string JavaScriptType => "CbxSw.Widgets.LoggedOnUsersWidget";

		/// <summary>
		/// Gets a collection of <see cref="T:System.Web.UI.ScriptReference"/> objects that define script resources that the control requires.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> collection of <see cref="T:System.Web.UI.ScriptReference"/> objects.
		/// </returns>
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			yield return new ScriptReference(ScriptInfos.Namespaces.LoggedOnUsersWidget, GetType().Assembly.FullName);
		}
	}
}