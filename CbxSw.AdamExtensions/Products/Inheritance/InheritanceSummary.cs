using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adam.Pims.Core.Configuration;
using Adam.Web.ConfigStudio.Controls;
using Adam.Web.Localization;
using Adam.Web.Studio.UI;
using Adam.Web.Studio.UI.Controls;
using Adam.Web.Tools.Fluent;
using Adam.Web.UI;
using Adam.Web.UI.Controls;

namespace CbxSw.AdamExtensions.ConfigStudio.Products.Inheritance
{
	/// <summary>
	/// Control that displays the <see cref="FieldInheritanceDefinition"/> of one <see cref="Adam.Core.Fields.FieldDefinition"/>.
	/// This control only displays the info of a <see cref="FieldInheritanceDefinition"/>, it doesn't show anything but the <see cref="FieldInheritanceDefinition"/>.
	/// </summary>
	public class InheritanceSummary : BindableUserControl<FieldInheritanceDefinitionInfo>, ISkipControlBinding
	{
		private Translator _fieldsTranslator;

		private Translator FieldsTranslator => _fieldsTranslator ?? (_fieldsTranslator = Translator.GetGlobalTranslator("FieldDefinitions"));

		private Control RightsContainer { get; set; }

		public InheritanceSummary()
		{
			AppRelativeVirtualPath = "~/UI/FieldInheritance/Summary.ascx"; // making sure no ascx file is required (I know it looks strange but it works)
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			InitializeContentIn(this);
		}

		private void InitializeContentIn(InheritanceSummary container)
		{
			container.AddGeneric("div", ctr =>
			{
				ctr.AddClass("summary-form");
				ctr.AddControl(new Expandable(), InitalizeGeneralSection);
				ctr.AddControl(new Expandable(), InitalizeRightsSection);
			});
		}

		public override void DataBind()
		{
			base.DataBind();
			AddRights();
		}

		private void AddRights()
		{
			RightsContainer.Controls.Clear();
			foreach (var fieldInheritanceRight in DataItem.Rights)
			{
				AddRightTo(RightsContainer, fieldInheritanceRight);
			}
		}

		private void InitalizeGeneralSection(Expandable generalSection)
		{
			generalSection.Title = FieldsTranslator.Translate("GeneralSection.Title");
			generalSection.Expanded = true;
			generalSection.AddGeneric("table", table =>
			{
				table.AddClass("summary-grid");
				InitializeNameIn(table);
				InitializeDataTypeIn(table);
				InitializeScopeIn(table);
				InitializePropagationModeIn(table);
				InitializeIsActiveIn(table);
			});
		}

		private void InitializeIsActiveIn(Control table)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddGeneric("td", td =>
				{
					td.AddClass("control");
					td.Attributes["colspan"] = "2";
					td.AddGeneric("div", div =>
					{
						div.AddControl(new ReadOnlyCheckBox(), cbx =>
						{
							cbx.ID = "cbxIsActive";
							cbx.Text = "Is active";
							cbx.EnableViewState = false;
						});
						div.AddPropertyBinder("cbxIsActive", "IsActive");
					});
				});
			});
		}

		private void InitializeNameIn(Control table)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddLabelCell(FieldsTranslator.Translate("Name.Text"), "lblFieldName");
				row.AddControlCell(cell =>
				{
					cell.AddControl(new Label(), lblId =>
					{
						lblId.ID = "lblFieldName";
						lblId.EnableViewState = false;
					});
					cell.AddPropertyBinder("lblFieldName", "Name");
				});
			});
		}

		private void InitializeDataTypeIn(Control table)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddLabelCell(FieldsTranslator.Translate("DataType.Text"), "lblDataType");
				row.AddControlCell(cell =>
				{
					cell.AddControl(new Label(), lbl =>
					{
						lbl.ID = "lblDataType";
						lbl.EnableViewState = false;
					});
					cell.AddPropertyBinder("lblDataType", "DataType", bndr => bndr.DisplayConversions.Add(new EnumerationTranslation()));
				});
			});
		}

		private void InitializeScopeIn(Control table)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddLabelCell(FieldsTranslator.Translate("Scope.Text"), "lblScope");
				row.AddControlCell(container =>
				{
					container.AddControl(new Label(), lblId =>
					{
						lblId.ID = "lblScope";
						lblId.EnableViewState = false;
					});
					container.AddPropertyBinder("lblScope", "Scope", bndr => bndr.DisplayConversions.Add(new EnumerationTranslation()));
				});
			});
		}

		private void InitializePropagationModeIn(Control table)
		{
			table.AddGeneric("tr", row =>
			{
				row.AddLabelCell("Propagation mode", "lblPropagationMode"); // TODO: translate
				row.AddControlCell(cell =>
				{
					cell.AddControl(new Label(), lblId =>
						{
							lblId.ID = "lblPropagationMode";
							lblId.EnableViewState = false;
						});
					cell.AddPropertyBinder("lblPropagationMode", "PropagationMode");
				});
			});
		}

		private void AddRightTo(Control container, FieldInheritanceRight right)
		{
			container.AddGeneric("div", itm =>
			{
				itm.AddClass("item");
				itm.Style.Add("border-top", "1px solid #E7E9EB;"); // TODO: move this to css
				itm.AddGeneric("table", itmTable =>
				{
					itmTable.AddClass("summary-grid");
					itmTable.AddGeneric("tr", row =>
					{
						row.AddLabelCell("Classification"); // TODO translate
						row.AddControlCell(cell =>
						{
							cell.AddLiteral(DisplayValueFor(right.Classification));
						});
					});
					itmTable.AddGeneric("tr", row =>
					{
						row.AddLabelCell("User group"); // TODO translate
						row.AddControlCell(cell =>
						{
							cell.AddLiteral(DisplayValueFor(right.UserGroup)); // TODO translate
						});
					});
					itmTable.AddGeneric("tr", row =>
					{
						row.AddLabelCell("Permission"); // TODO translate
						row.AddControlCell(cell =>
						{
							cell.AddLiteral(right.Permission.ToString());
						});
					});
				});
			});
		}

		private string DisplayValueFor(Guid? id)
		{
			if (id == null)
			{
				return "Any";// TODO translate
			}
			return id.ToString();
		}

		private void InitalizeRightsSection(Expandable rightsSection)
		{
			rightsSection.Title = "Rights"; // TODO: translate
			rightsSection.Expanded = true;
			RightsContainer = rightsSection.AddGeneric("div", div =>
			{
				div.AddClass("rights");
			});
		}
	}
}