using System;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;
using Adam.Core.Fields;
using Adam.Pims.Core.Configuration;
using Adam.Web.ConfigStudio;
using Adam.Web.ConfigStudio.Pages;
using Adam.Web.ConfigStudio.UI.FieldDefinitions;
using Adam.Web.Studio;
using Adam.Web.Studio.Routing;
using Adam.Web.Studio.UI.Controls;
using Adam.Web.Tools.Fluent;
using Adam.Web.UI.DataSources;

namespace CbxSw.AdamExtensions.Products.Inheritance
{
	/// <summary>
	/// The page that will display one <see cref="FieldInheritanceDefinition"/> in readonly mode.
	/// This is where headers, actions, ... are added.
	/// </summary>
	public class InheritanceViewPage : ConfigStudioViewPage<FieldInheritanceDefinition>, IRequiresSessionState, IHttpHandler
	{
		private ConfigStudioFormView _formView;
		private AdamDataSource _dataSource;
		private FieldDefinition _fieldDefinition;

		public InheritanceViewPage()
		{
			AppRelativeVirtualPath = "~/UI/FieldInheritance/View.aspx"; // making sure no aspx file is required (I know it looks strange but it works)
		}

		protected override StudioFormView FormView => _formView;
		protected override AdamDataSource DataSource => _dataSource;
		protected override Route SearchRoute => Routes.For<Search>(); // TODO: make this our field inheritance search page
		protected override int VisibleActionGroups => 0;
		protected override Type SummaryUserControl => typeof(InheritanceSummary);

		/// <summary>
		/// Initializes the control tree during page generation based on the declarative nature of the page. 
		/// </summary>
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			BuildControlTree(this);
		}

		private void BuildControlTree(InheritanceViewPage container)
		{
			container.MasterPageFile = StudioHelper.GetMasterPageFile();
			container.EnableEventValidation = false;
			InitializeCulture();
			AddContentTemplate("HeadContent", new CompiledTemplateBuilder(InitializeHeaderIn));
			AddContentTemplate("PageContent", new CompiledTemplateBuilder(InitializeContentIn));
		}

		private void InitializeHeaderIn(Control container)
		{
			// intentionally left blank
		}

		private void InitializeContentIn(Control container)
		{
			_formView = container.AddControl(new ConfigStudioFormView(), fvw =>
			{
				fvw.TemplateControl = this;
				fvw.TemplateControl = this;
				fvw.ApplyStyleSheetSkin(this);
				fvw.ID = "sfvFieldInheritance";
				fvw.IdentifierDataField = "Name";
			});
			_dataSource = container.AddControl(new AdamDataSource(), ds =>
			{
				ds.TemplateControl = this;
				ds.ID = "dsFieldInheritance";
				ds.DataProviderType = "CbxSw.AdamExtensions.Products.Inheritance.FieldInheritanceDefinitionProvider, CbxSw.AdamExtensions";
			});
		}

		private FieldDefinition FieldDefinition(FieldInheritanceDefinition dataItem)
		{
			if (_fieldDefinition == null)
			{
				_fieldDefinition = new FieldDefinitionHelper(dataItem.App).GetFieldDefinition(dataItem.FieldId);
			}
			return _fieldDefinition;
		}

		protected override RouteValueDictionary GetDataItemRouteValues(FieldInheritanceDefinition dataItem)
		{
			var fieldName = FieldDefinition(dataItem)?.Name;
			return this.CreateRouteValues(dataItem.Id, fieldName);
		}

		protected override string GetDataItemTitle(FieldInheritanceDefinition dataItem)
		{
			return FieldDefinition(dataItem).Label;
		}
		protected override void ConfigureToolbar(StudioActionToolbar toolbar)
		{
		}
	}
}