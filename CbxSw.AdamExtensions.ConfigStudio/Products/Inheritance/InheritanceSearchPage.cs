using System;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;
using Adam.Core;
using Adam.Core.Fields;
using Adam.Web.ConfigStudio.Pages;
using Adam.Web.Localization;
using Adam.Web.Studio;
using Adam.Web.Studio.Routing;
using Adam.Web.Studio.UI.Controls;

namespace CbxSw.AdamExtensions.ConfigStudio.Products.Inheritance
{
	public class InheritanceSearchPage : ConfigStudioSearchPage<FieldInheritanceDefinitionInfo>, IRequiresSessionState, IHttpHandler
	{
		private Translator _fieldsTranslator;

		public InheritanceSearchPage()
		{
			AppRelativeVirtualPath = "~/UI/FieldInheritance/Search.aspx"; // making sure no aspx file is required (I know it looks strange but it works)
		}

		protected override Type DataProviderType => typeof(FieldInheritanceDefinitionProvider);
		protected override RoleType? RequiredCreateRole => RoleType.ChangeSystemSettings;
		protected override Route SearchRoute => Routes.For<InheritanceSearchPage>();
		protected override Route CreateRoute => Routes.For<Adam.Web.ConfigStudio.UI.FieldDefinitions.Create>(); // No create page, so taken the route to create FieldDefinition
		protected override Route ViewRoute => Routes.For<InheritanceViewPage>();
		private Translator FieldsTranslator => _fieldsTranslator ?? (_fieldsTranslator = Translator.GetGlobalTranslator("FieldDefinitions"));

		protected override RouteValueDictionary CreateRouteValues(FieldInheritanceDefinitionInfo item)
		{
			return CreateRouteValues(item.FieldId, item.Name);
		}

		protected override bool CanCreateItems()
		{
			// No create page
			return false;
		}

		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			BuildControlTree(this);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			View.Columns.AddTextColumn("Name", "Name", FieldsTranslator.Translate("ColumnName.HeaderText"));
			View.Columns.AddTextColumn("Label", "Label", FieldsTranslator.Translate("ColumnLabel.HeaderText"));
			View.Columns.AddEnumColumn<Scope>("Scope", "Scope", FieldsTranslator.Translate("ColumnScope.HeaderText"));
			View.Columns.AddEnumColumn<DataType>("DataType", "DataType", FieldsTranslator.Translate("ColumnDataType.HeaderText"));
			View.Columns.Add(InitializeIsActiveColumn());
			View.SortExpression = "name";
		}

		private Column InitializeIsActiveColumn()
		{
			var studioTranslator = Translator.GetStudioTranslator(StudioHelper.ResolveStudio(), "Globals");
			var column = new BooleanColumn
			{
				Name = "IsActive",
				InitialVisible = true,
				Header = Translator.Translate("ColumnIsActive.HeaderText"),
				Path = "IsActive",
				CssClass = "column-isactive",
				TrueText = Translator.Translate("Yes.Text"),
				FalseText = Translator.Translate("No.Text")
			};
			return column;
		}

		private void BuildControlTree(InheritanceSearchPage page)
		{
			page.MasterPageFile = StudioHelper.GetMasterPageFile();
			InitializeCulture();
			AddContentTemplate("HeadContent", new CompiledTemplateBuilder(x =>
			{
				// does nothing intentionally
			}));
		}
	}
}