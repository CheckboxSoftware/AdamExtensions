using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adam.Web.Studio.UI.Controls;
using Adam.Web.Tools.Fluent;
using Adam.Web.UI.Controls;

namespace CbxSw.AdamExtensions.ConfigStudio
{
	public static class ControlExtensions
	{
		public static PropertyBinder AddPropertyBinder(this Control container, string targetControlId, string propertyName, Action<PropertyBinder> customizeBinder = null)
		{
			return container.AddControl(new PropertyBinder(), binder =>
			{
				binder.ID = $"bndr{targetControlId}";
				binder.TargetControlID = targetControlId;
				binder.DataField = propertyName;
				customizeBinder?.Invoke(binder);
			});
		}

		public static void AddLabelCell(this Control row, string label, string associatedControlId = null)
		{
			row.AddGeneric("td", cell =>
			{
				cell.AddClass("label");
				cell.AddControl(new Label(), lbl =>
				{
					if (associatedControlId != null)
					{
						lbl.AssociatedControlID = associatedControlId;
					}
					lbl.Text = label;
					lbl.EnableViewState = false;
				});
			});
		}

		public static void AddControlCell(this Control row, Action<Control> addControls)
		{
			row.AddGeneric("td", ctrl =>
			{
				ctrl.AddClass("control");
				ctrl.AddGeneric("div", div =>
				{
					div.AddClass("property-text-ro");
					addControls.Invoke(div);
				});
			});
		}

		public static Column AddTextColumn(this IList<Column> columns, string name, string path, string label)
		{
			var identifier = name.ToLowerInvariant();
			var column = new BinderColumn
			{
				Name = name,
				InitialVisible = true,
				Header = label,
				Path = path,
				CssClass = $"column-{identifier}",
				ColumnFilter = new TextFilter { FilterExpression = $"{identifier}=?" },
				SortOptions = new[]
				{
					new SortOption {SortOrder = SortOrder.Ascending, SortExpression = $"{identifier}"},
					new SortOption {SortOrder = SortOrder.Descending, SortExpression = $"{identifier}name desc"}
				}
			};

			columns.Add(column);
			return column;
		}

		public static Column AddEnumColumn<TEnum>(this IList<Column> columns, string name, string path, string label)
			where TEnum : struct
		{
			var identifier = name.ToLowerInvariant();
			var column = new EnumColumn
			{
				Name = name,
				InitialVisible = true,
				Header = label,
				Path = path,
				CssClass = $"column-{identifier}",
				ColumnFilter = new EnumFilter<TEnum>
				{
					FilterExpression = $"{identifier}=?"
				}
			};
			columns.Add(column);
			return column;
		}
	}
}