using System;
using System.Collections.Generic;
using System.Linq;
using Adam.Core.Fields;
using Adam.Core.Search;
using Adam.Pims.Core;
using Adam.Pims.Core.Configuration;
using Adam.Web.UI.DataSources;
using Adam.Web.UI.DataSources.DataProviders;

namespace CbxSw.AdamExtensions.ConfigStudio.Products.Inheritance
{
	/// <summary>
	/// A <see cref="DataProvider"/> that is responsible for loading <see cref="FieldInheritanceDefinition"/> instances via a <see cref="AdamDataSource"/>.
	/// </summary>
	public class FieldInheritanceDefinitionProvider : ItemBaseProvider
	{
		public override SearchExpression SelectAllExpression => new SearchExpression("name = *");

		public override Type DataType => typeof(FieldInheritanceDefinition);

		public override string DefaultSortExpression => "Label";

		public override bool CanDelete => false; // TODO maybe this can be true?

		public override bool CanInsert => false;

		public override bool CanPage => true;

		public override bool CanRetrieveTotalRowCount => true;

		public override bool CanSort => true;

		public override bool CanUpdate => true;

		public override SelectResult ExecuteSelect(AdamDataSourceSelectingEventArgs arguments)
		{
			var selectResult = new SelectResult<FieldInheritanceDefinition>();
			var definitionCollection = new FieldDefinitionCollection(App);
			var isLastPage = LoadFieldsPage(definitionCollection, arguments);
			var inheritanceDefinitions = LoadDefinitionsFor(definitionCollection).ToList();
			selectResult.SetItems(arguments, inheritanceDefinitions, isLastPage);
			return selectResult;
		}

		public override int QueryTotalRowCount(AdamDataSourceSelectingEventArgs arguments)
		{
			var amountOfFieldDefinitions = new FieldDefinitionHelper(App).GetMatches(arguments.SelectExpression, false);
			if (arguments.MaximumRows != -1)
			{
				if (amountOfFieldDefinitions > arguments.MaximumRows)
				{
					return arguments.MaximumRows;
				}
			}
			return amountOfFieldDefinitions;
		}

		private bool LoadFieldsPage(FieldDefinitionCollection definitionCollection, AdamDataSourceSelectingEventArgs arguments)
		{
			var pageNumber = arguments.PageNumber + 1;
			var loadOptions = arguments.LoadOptions as FieldDefinitionLoadOptions;
			var maxRows = arguments.MaximumRows;
			var pageSize = arguments.PageSize;
			var sortExpression = arguments.SortExpression;
			var searchExpression = arguments.SelectExpression;
			bool isLastPage;
			do
			{
				definitionCollection.Load(searchExpression, sortExpression, pageNumber, pageSize, out isLastPage, maxRows, loadOptions);
				--pageNumber;
			}
			while (definitionCollection.Count == 0 && pageNumber > 0);
			arguments.PageNumber = pageNumber;
			return isLastPage;
		}

		private IEnumerable<FieldInheritanceDefinitionInfo> LoadDefinitionsFor(FieldDefinitionCollection fieldDefinitionCollection)
		{
			var inheritances = new FieldInheritanceDefinitionCollection(App);
			inheritances.Load(fieldDefinitionCollection.CopyIdsToArray());
			foreach (FieldDefinition fieldDefinition in fieldDefinitionCollection)
			{
				var fieldId = fieldDefinition.Id;
				var foundInheritance = inheritances.FirstOrDefault(x => x.FieldId == fieldId);
				if (foundInheritance != null)
				{
					yield return CreateInfoInstance(fieldDefinition, foundInheritance);
				}
				else
				{
					yield return CreateInfoInstance(fieldDefinition);
				}
			}
		}

		private FieldInheritanceDefinitionInfo CreateInfoInstance(FieldDefinition field, FieldInheritanceDefinition inheritance = null)
		{
			return new FieldInheritanceDefinitionInfo
			{
				DataType = field.DataType,
				FieldId = field.Id,
				IsActive = inheritance?.IsActive ?? false,
				Name = field.Name,
				PropagationMode = inheritance?.PropagationMode ?? Execution.Delayed,
				Rights = inheritance?.Rights ?? new FieldInheritanceRight[0],
				Scope = field.Scope,
				Label = field.Label,
				Labels = field.Labels,
			};
		}
	}
}