using Adam.Web.Extensions.UI;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	/// <summary>
	/// Serves as the base class for attributes that specify which scripts are required for a
	///             control to properly run on the client.
	/// 
	/// </summary>
	public class RequiresEtexScriptAttribute : RequiresScriptAttribute
	{
		private readonly JavaScript _scriptResource;

		public RequiresEtexScriptAttribute(JavaScript scriptResource)
		{
			_scriptResource = scriptResource;
		}

		protected override ScriptInfo GetScriptInfo()
		{
			return ScriptInfos.Get(_scriptResource);
		}
	}
}