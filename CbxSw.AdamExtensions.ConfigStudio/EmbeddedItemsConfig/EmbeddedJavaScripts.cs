using System.Collections.Generic;
using System.Web.UI;
using Adam.Web.Extensions.UI;
using CbxSw.AdamExtensions.ConfigStudio;

// build your javascript as embedded resource and add it here
[assembly: WebResource(ScriptInfos.Namespaces.LoggedOnUsersWidget, "text/javascript", PerformSubstitution = true)]

namespace CbxSw.AdamExtensions.ConfigStudio
{
	public enum JavaScript
	{
		// add an item here to this enum to make your script available from the ScriptInfos attribute
		LoggedOnUsersWidget = 0,
	}

	public static class ScriptInfos
	{
		private static readonly Dictionary<JavaScript, EmbeddedResourceScriptInfo> _scriptInfos;
		public static class Namespaces
		{
			// here is the list of fully qualified name to script embedded resource
			public const string LoggedOnUsersWidget = "CbxSw.AdamExtensions.ConfigStudio.Widgets.LoggedOnUsers.LoggedOnUsersWidget.js";
		}


		static ScriptInfos()
		{
			var type = typeof(ScriptInfos);
			_scriptInfos = new Dictionary<JavaScript, EmbeddedResourceScriptInfo>
			{
				// add an item here to this enum to make your script available from the ScriptInfos attribute
				{JavaScript.LoggedOnUsersWidget, new EmbeddedResourceScriptInfo(type, Namespaces.LoggedOnUsersWidget, "CbxSw.AdamExtensions")},
			};
		}

		public static ScriptInfo Get(JavaScript script)
		{
			return _scriptInfos[script];
		}

		public static string UrlFor(Control control, JavaScript script)
		{
			var scriptInfo = _scriptInfos[script];
			return control.Page.ClientScript.GetWebResourceUrl(scriptInfo.GetType(), scriptInfo.ResourceName);
		}
	}
}
