using Adam.Web.Extensions.UI.Controls;
using Adam.Web.UI.Controls;

namespace CbxSw.UserFullName.ControlBuilder
{
	/// <summary>
	/// This <see cref="FieldControlBuilder"/> allows users to search on the field <see cref="UserFullNameFieldName"/>. 
	/// </summary>
	public class CustomUserListFieldControlBuilder : ExtendedUserListFieldControlBuilder
	{
		public const string UserFullNameFieldName = "UserFullName";

		protected override ListEditorBase CreateListEditor(FieldControlBuildInfo buildInfo)
		{
			var editor = (SearchListEditor)base.CreateListEditor(buildInfo);
			editor.SearchTemplate = $"fieldname({UserFullNameFieldName})=? or name=?";
			return editor;
		}
	}
}