using System;
using Adam.Core.Fields;
using Adam.Pims.Core.Configuration;
using Adam.Web.Studio.UI;
using Adam.Web.Tools.Fluent;
using Adam.Web.UI;

namespace CbxSw.AdamExtensions.Products.Inheritance
{
	public class InheritanceSummary : BindableUserControl<FieldDefinition>, ISkipControlBinding
	{
		public InheritanceSummary()
		{
			AppRelativeVirtualPath = "~/UI/FieldInheritance/Summary.ascx"; // making sure no aspx file is required (I know it looks strange but it works)
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			// build controls here
			this.AddLiteral("Summary control here");
		}
	}
}